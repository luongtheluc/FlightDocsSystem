using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightDocsSystem.DataAccess.Repository.IRepository;
using FlightDocsSystem.Helper;
using FlightDocsSystem.Model.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightDocsSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class GroupController : ControllerBase
    {
        private readonly IGroupPermissionRepository _groupRepo;

        public GroupController(IGroupPermissionRepository groupPermissionResponsitory)
        {
            _groupRepo = groupPermissionResponsitory;
        }

        const string NAMECONTROLLER = "Group Permission"; //hien thi ten cua thong bao


        [HttpGet]
        public async Task<IActionResult> GetAllGroupPermissionType()
        {
            try
            {
                var GroupPermissions = await _groupRepo.GetAllGroupPermissionAsync();
                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Get all " + NAMECONTROLLER + " success",
                    Data = GroupPermissions
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
        public async Task<IActionResult> GetGroupPermissionTypeById(int id)
        {
            try
            {
                var GroupPermission = await _groupRepo.GetGroupPermissionByIdAsync(id);
                if (GroupPermission != null)
                {
                    return Ok(new ApiResponse
                    {
                        Success = true,
                        Message = "Get " + NAMECONTROLLER + " success",
                        Data = GroupPermission
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
        public async Task<IActionResult> AddGroupPermissionType(GroupPermissionDTO model)
        {
            try
            {
                var newGroupPermissionId = await _groupRepo.AddGroupPermissionAsync(model);
                var GroupPermission = await _groupRepo.GetGroupPermissionByIdAsync(newGroupPermissionId);
                if (GroupPermission != null)
                {
                    return Ok(new ApiResponse
                    {
                        Success = true,
                        Message = "Get " + NAMECONTROLLER + " success",
                        Data = GroupPermission
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
        public async Task<IActionResult> UpdateGroupPermissionType(int id, GroupPermissionDTO model)
        {
            try
            {
                if (id != model.GroupId)
                {

                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "Update " + NAMECONTROLLER + " fail",
                        Data = null
                    });
                }
                await _groupRepo.UpdateGroupPermissionAsync(id, model);
                var GroupPermission = await _groupRepo.GetGroupPermissionByIdAsync(id);

                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Update " + NAMECONTROLLER + " success",
                    Data = GroupPermission
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
        public async Task<IActionResult> DeleteGroupPermissionType([FromRoute] int id)
        {
            try
            {
                var GroupPermission = await _groupRepo.GetGroupPermissionByIdAsync(id);
                if (GroupPermission == null)
                {
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "Delete " + NAMECONTROLLER + " fail",
                        Data = null
                    });
                }

                await _groupRepo.DeleteGroupPermissionAsync(id);
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