using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightDocsSystem.DataAccess.Repository.IRepository;
using FlightDocsSystem.Helper;
using FlightDocsSystem.Model.DTOs;
using FlightDocsSystem.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightDocsSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "User")]
    public class AircraftTypeController : ControllerBase
    {
        private readonly IAircraftTypeResponsitory _aircraftTypeRepo;
        const string NAMECONTROLLER = "Aircraft Type"; //hien thi ten cua thong bao
        public AircraftTypeController(IAircraftTypeResponsitory repo)
        {
            this._aircraftTypeRepo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAircraftType()
        {
            try
            {
                var aircrafts = await _aircraftTypeRepo.GetAllAircraftTypeAsync();
                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Get all " + NAMECONTROLLER + " success",
                    Data = aircrafts
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
        public async Task<IActionResult> GetAircraftTypeById(int id)
        {
            try
            {
                var aircraft = await _aircraftTypeRepo.GetAircraftTypeByIdAsync(id);
                if (aircraft != null)
                {
                    return Ok(new ApiResponse
                    {
                        Success = true,
                        Message = "Get " + NAMECONTROLLER + " success",
                        Data = aircraft
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
        public async Task<IActionResult> AddAircraftType(AircraftTypeDTO model)
        {
            try
            {
                var newAircraftId = await _aircraftTypeRepo.AddAircraftTypeAsync(model);
                var aircraft = await _aircraftTypeRepo.GetAircraftTypeByIdAsync(newAircraftId);
                if (aircraft != null)
                {
                    return Ok(new ApiResponse
                    {
                        Success = true,
                        Message = "Get " + NAMECONTROLLER + " success",
                        Data = aircraft
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
        public async Task<IActionResult> UpdateAircraftType(int id, AircraftTypeDTO model)
        {
            try
            {
                if (id != model.AircraftTypeId)
                {

                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "Update " + NAMECONTROLLER + " fail",
                        Data = null
                    });
                }
                await _aircraftTypeRepo.UpdateAircraftTypeAsync(id, model);
                var aircraft = await _aircraftTypeRepo.GetAircraftTypeByIdAsync(id);

                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Update " + NAMECONTROLLER + " success",
                    Data = aircraft
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
        public async Task<IActionResult> DeleteAircraftType([FromRoute] int id)
        {
            try
            {
                var aircraft = await _aircraftTypeRepo.GetAircraftTypeByIdAsync(id);
                if (aircraft == null)
                {
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "Delete " + NAMECONTROLLER + " fail",
                        Data = null
                    });
                }

                await _aircraftTypeRepo.DeleteAircraftTypeAsync(id);
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