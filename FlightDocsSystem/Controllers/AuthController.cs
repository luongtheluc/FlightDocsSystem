using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightDocsSystem.DataAccess.Responsitory.IResponsitory;
using FlightDocsSystem.Helper;
using FlightDocsSystem.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace FlightDocsSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {

        private readonly IAuthResponsitory _authRepo;
        private readonly IConfiguration _configuration;
        private readonly IRoleResponsitory _roleRepo;

        public AuthController(IAuthResponsitory authRepo, IConfiguration configuration, IRoleResponsitory roleResponsitory)
        {
            this._roleRepo = roleResponsitory;
            this._configuration = configuration;
            _authRepo = authRepo;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDTO userDTO)
        {
            try
            {
                var randomToken = Jwt.CreateRandomToken();
                var addUser = await _authRepo.AddUserAsync(userDTO, randomToken);
                if (addUser == -1)
                {
                    return BadRequest(new ApiResponse
                    {
                        Data = null,
                        Success = false,
                        Message = "Email Already in Use"
                    });
                }
                var user = await _authRepo.GetUserByIdAsync(addUser);
                if (user != null)
                {
                    // var userrole = new UserRoleDTO
                    // {
                    //     UserId = user.UserId,
                    //     RoleId = await _roleRepo.GetUserRoleAsync()
                    // };
                    // await _roleRepo.AddUserRole(userrole);
                    return Ok(new ApiResponse
                    {
                        Data = user,
                        Success = true,
                        Message = "Register success"
                    });
                }
                return NotFound(new ApiResponse
                {
                    Success = false,
                    Message = "Register fail",
                    Data = null
                });
            }
            catch (System.Exception e)
            {

                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = e.Message,
                    Data = null
                });
            }
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(string username, string password)
        {
            try
            {
                if (username != null && password != null)
                {
                    if (await _authRepo.CheckUserNameAsync(username))
                    {
                        var user = await _authRepo.CheckLoginAsync(username, password);

                        if (user != null)
                        {
                            if (user.VerifyAt == null)
                            {
                                return BadRequest(new ApiResponse
                                {
                                    Data = null,
                                    Message = "Email is not verify",
                                    Success = true
                                });
                            }
                            var secretKey = _configuration.GetValue<string>("AppSettings:Token");

                            string token = Jwt.CreateToken(user, secretKey!);
                            var refreshToken = Jwt.GenerateRefreshToken();
                            var cookieOptions = new CookieOptions
                            {
                                HttpOnly = true,
                                Expires = refreshToken.Expired,
                            };
                            Response.Cookies.Append("refreshToken", refreshToken.Token!, cookieOptions);
                            user.RefreshToken = refreshToken.Token;
                            user.RefreshTokenCreated = refreshToken.Created;
                            user.RefreshTokenExpries = refreshToken.Expired;
                            await _authRepo.UpdateUserAsync(user);
                            return Ok(new ApiResponse
                            {
                                Data = token,
                                Success = true,
                                Message = "Login success"
                            });
                        }
                        return BadRequest(new ApiResponse
                        {
                            Data = null,
                            Message = "username or password wrong!",
                            Success = false
                        });
                    }
                    else
                    {
                        return NotFound(new ApiResponse
                        {
                            Success = false,
                            Message = "user not found",
                            Data = null
                        });
                    }
                }
                return BadRequest(new ApiResponse
                {
                    Data = null,
                    Message = "field is null",
                    Success = true
                });
            }
            catch (System.Exception e)
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = e.Message,
                    Data = null
                });

            }
        }

        [HttpPost("verify")]
        public async Task<IActionResult> Verify(string token)
        {
            var user = await _authRepo.CheckVerifyTokenAsync(token);
            if (user == null)
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Invalid token",
                    Data = null
                });
            }
            user.VerifyAt = DateTime.Now;
            await _authRepo.UpdateUserAsync(user);
            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Email is verifed",
                Data = null
            });
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            var user = await _authRepo.CheckEmailAsync(email);
            if (user == null)
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "User not found",
                    Data = null
                });
            }
            user.PasswordResetToken = Jwt.CreateRandomToken();
            user.ResetTokenExpries = DateTime.Now.AddDays(1);

            await _authRepo.UpdateUserAsync(user);
            return Ok(new ApiResponse
            {
                Success = true,
                Message = "You may now reset your password",
                Data = null
            });
        }
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(string token, string password)
        {
            try
            {
                var user = await _authRepo.CheckPasswordResetTokenAsync(token);
                if (user == null)
                {
                    return BadRequest(new ApiResponse
                    {
                        Success = false,
                        Message = "User not found",
                        Data = null
                    });

                }
                if (user.ResetTokenExpries < DateTime.Now)
                {
                    return BadRequest(new ApiResponse
                    {
                        Success = false,
                        Message = "token expired",
                        Data = null
                    });
                }
                string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
                user.Password = passwordHash;
                user.PasswordResetToken = "";
                user.ResetTokenExpries = null;

                await _authRepo.UpdateUserAsync(user);
                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "You may now reset your password",
                    Data = null
                });
            }
            catch (System.Exception e)
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = e.Message,
                    Data = null
                });
            }
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<string>> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            if (refreshToken is not null)
            {
                var check = await _authRepo.CheckRefreshToken(refreshToken!);
                if (check == 1)
                {
                    var user = await _authRepo.GetUserByRefreshToken(refreshToken);
                    var secretKey = _configuration.GetValue<string>("AppSettings:Token");
                    string token = Jwt.CreateToken(user, secretKey!);
                    var newRefreshToken = Jwt.GenerateRefreshToken();
                    var cookieOptions = new CookieOptions
                    {
                        HttpOnly = true,
                        Expires = newRefreshToken.Expired,
                    };
                    Response.Cookies.Append("refreshToken", newRefreshToken.Token!, cookieOptions);
                    user.RefreshToken = newRefreshToken.Token;
                    user.RefreshTokenCreated = newRefreshToken.Created;
                    user.RefreshTokenExpries = newRefreshToken.Expired;
                    await _authRepo.UpdateUserAsync(user);
                    return Ok(new ApiResponse
                    {
                        Data = token,
                        Message = "success",
                        Success = true,
                    });
                }
                else
                {
                    if (check == -1)
                    {
                        return Unauthorized(new ApiResponse
                        {
                            Data = null,
                            Success = false,
                            Message = "Invalid refresh token"
                        });
                    }
                    else
                    {
                        return Unauthorized(new ApiResponse
                        {
                            Data = null,
                            Success = false,
                            Message = "Token expires"
                        });
                    }
                }
            }
            else
            {
                return Unauthorized(new ApiResponse
                {
                    Data = null,
                    Success = false,
                    Message = "Invalid refresh token"
                });
            }
        }
    }
}