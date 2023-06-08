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
                var hashPassword = HashPassword.HashPasswordBycript(userDTO.Password!);
                var addUser = await _authRepo.AddUserAsync(userDTO);
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
                            var secretKey = _configuration.GetValue<string>("AppSettings:Token");

                            string token = Jwt.CreateToken(user, secretKey!);
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
                            Message = "Login fail",
                            Success = true
                        });
                    }
                    else
                    {
                        return NotFound(new ApiResponse
                        {
                            Success = false,
                            Message = "Login fail",
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
    }
}