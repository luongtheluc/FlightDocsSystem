using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightDocsSystem.DataAccess.Repository.IRepository;
using FlightDocsSystem.Helper;
using FlightDocsSystem.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightDocsSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RoleController : ControllerBase
    {
        private readonly IRoleRepository _roleRepo;

        public RoleController(IRoleRepository RoleRepo)
        {
            _roleRepo = RoleRepo;
        }

        const string NAMECONTROLLER = "Role"; //hien thi ten cua thong bao


        [HttpGet]
        public async Task<IActionResult> GetAllRole()
        {
            try
            {
                var Roles = await _roleRepo.GetAllRoleAsync();
                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Get all " + NAMECONTROLLER + " success",
                    Data = Roles
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleById(int id)
        {
            try
            {
                var Role = await _roleRepo.GetRoleByIdAsync(id);
                if (Role != null)
                {
                    return Ok(new ApiResponse
                    {
                        Success = true,
                        Message = "Get " + NAMECONTROLLER + " success",
                        Data = Role
                    });
                }
                return NotFound(new ApiResponse
                {
                    Success = false,
                    Message = "Get " + NAMECONTROLLER + " fail",
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

        [HttpPost]
        public async Task<IActionResult> AddRole(RoleDTO model)
        {
            try
            {
                var newRoleId = await _roleRepo.AddRoleAsync(model);
                var Role = await _roleRepo.GetRoleByIdAsync(newRoleId);
                if (Role != null)
                {
                    return Ok(new ApiResponse
                    {
                        Success = true,
                        Message = "Get " + NAMECONTROLLER + " success",
                        Data = Role
                    });
                }
                return NotFound(new ApiResponse
                {
                    Success = false,
                    Message = "Get " + NAMECONTROLLER + " fail",
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

        [HttpPut]
        public async Task<IActionResult> UpdateRole(int id, RoleDTO model)
        {
            try
            {
                if (id != model.RoleId)
                {

                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "Update " + NAMECONTROLLER + " fail",
                        Data = null
                    });
                }
                await _roleRepo.UpdateRoleAsync(id, model);
                var Role = await _roleRepo.GetRoleByIdAsync(id);

                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Update " + NAMECONTROLLER + " success",
                    Data = Role
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


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole([FromRoute] int id)
        {
            try
            {
                var Role = await _roleRepo.GetRoleByIdAsync(id);
                if (Role == null)
                {
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "Delete " + NAMECONTROLLER + " fail",
                        Data = null
                    });
                }

                await _roleRepo.DeleteRoleAsync(id);
                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Delete " + NAMECONTROLLER + " success",
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
    }
}