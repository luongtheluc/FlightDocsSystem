using AutoMapper;
using FlightDocsSystem.DataAccess.Data;
using FlightDocsSystem.Model.Models;
using FlightDocsSystem.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using FlightDocsSystem.DataAccess.Repository.IRepository;
using System.Security.Cryptography;

namespace FlightDocsSystem.DataAccess.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly FlightDocsSystemContext _context;
        private readonly IMapper _mapper;
        private readonly ISendMailRepository _sendMailRepo;
        public AuthRepository(FlightDocsSystemContext context, IMapper mapper, ISendMailRepository sendMailRepo)
        {
            this._sendMailRepo = sendMailRepo;
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> AddUserAsync(UserDTO user, string randomToken)
        {
            if (await _context.Users!.CountAsync(x => x.Email == user.Email) > 0)
            {
                return -1;
            }
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(user.Password); //mã hoá password bằng Bycript
            user.Password = passwordHash;

            var addnew = _mapper.Map<User>(user);
            addnew.VerificationToken = randomToken;
            Console.WriteLine("\n \n id cua user: " + addnew.Username + "\n \n");
            _context.Users!.Add(addnew);
            await _context.SaveChangesAsync();
            return addnew.UserId;
        }

        public async Task<User> CheckEmailAsync(string email)
        {
            var user = await _context.Users!.FirstOrDefaultAsync(u => u.Email == email);
            return user!;
        }

        public async Task<User> CheckLoginAsync(string username, string password)
        {
            if (await CheckUserNameAsync(username))
            {
                var user = await _context.Users!.Where(x => x.Username == username).FirstOrDefaultAsync();
                var verify = BCrypt.Net.BCrypt.Verify(password, user!.Password);
                if (verify)
                {
                    return user;
                }
                else
                {
                    return null!;
                }
            }
            return null!;
        }

        public async Task<User> CheckPasswordResetTokenAsync(string token)
        {
            var user = await _context.Users!.FirstOrDefaultAsync(u => u.PasswordResetToken == token);
            return user!;
        }

        public async Task<bool> CheckUserNameAsync(string username)
        {
            var result = await _context.Users!.CountAsync(x => x.Username == username) > 0;
            return result;
        }

        public async Task<User> CheckVerifyTokenAsync(string token)
        {
            var user = await _context.Users!.FirstOrDefaultAsync(u => u.VerificationToken == token);
            return user!;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            var user = await _context.Users!.FindAsync(id);
            return user!;
        }


        public async Task UpdateUserAsync(User user)
        {
            _context.Users!.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetUserByRefreshToken(string token)
        {
            var user = await _context.Users!.Where(u => u.RefreshToken == token).FirstOrDefaultAsync();
            return user!;
        }

        public async Task<int> CheckRefreshToken(string token)
        {
            if (await _context.Users!.CountAsync(u => u.RefreshToken == token) > 0)
            {
                var user = await _context.Users!.Where(p => p.RefreshToken == token).FirstOrDefaultAsync();
                if (user!.RefreshTokenExpries > DateTime.Now)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return -1;
            }
        }
        public async Task<int> ForgotPassword(string email)
        {
            var user = await _context.Users!.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return -1;
            }

            user.PasswordResetToken = Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
            user.ResetTokenExpries = DateTime.Now.AddDays(1);
            await _context.SaveChangesAsync();

            var sendEmail = new EmailDTO
            {
                To = email,
                Subject = "Reset password link",
                Body = "<a target=" + "_blank" + " href=" + "http://localhost:5017/reset-password/" + user.PasswordResetToken + ">CLICK HERE</a>"

            };
            await _sendMailRepo.SendEmailAsync(sendEmail);
            return 1;
        }
        public async Task<User> GetUserByAccessToken(string token, string secretKey)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                ValidateIssuer = false,
                ValidateAudience = false
            };

            ClaimsPrincipal claimsPrincipal;
            try
            {
                claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out _);
            }
            catch
            {
                return null!;
            }

            var name = claimsPrincipal.FindFirst(ClaimTypes.Name)?.Value;
            var role = claimsPrincipal.FindFirst(ClaimTypes.Role)?.Value;
            var email = claimsPrincipal.FindFirst(ClaimTypes.Email)?.Value;

            var user = await _context.Users!.FirstOrDefaultAsync(u => u.Email == email);
            return user!;

        }
    }


}