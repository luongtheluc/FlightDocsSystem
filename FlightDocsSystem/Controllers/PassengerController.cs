using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightDocsSystem.DataAccess.Responsitory.IResponsitory;
using FlightDocsSystem.Helper;
using FlightDocsSystem.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightDocsSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PassengerController : ControllerBase
    {
        private readonly IPassengerResponsitory _passengerRepo;

        public PassengerController(IPassengerResponsitory PassengerTypeRepo)
        {
            _passengerRepo = PassengerTypeRepo;
        }

        const string NAMECONTROLLER = "Passenger"; //hien thi ten cua thong bao


        [HttpGet]
        public async Task<IActionResult> GetAllPassengerType()
        {
            try
            {
                var Passengers = await _passengerRepo.GetAllPassengerAsync();
                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Get all " + NAMECONTROLLER + " success",
                    Data = Passengers
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
        public async Task<IActionResult> GetPassengerTypeById(int id)
        {
            try
            {
                var Passenger = await _passengerRepo.GetPassengerByIdAsync(id);
                if (Passenger != null)
                {
                    return Ok(new ApiResponse
                    {
                        Success = true,
                        Message = "Get " + NAMECONTROLLER + " success",
                        Data = Passenger
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
        public async Task<IActionResult> AddPassengerType(PassengerDTO model)
        {
            try
            {
                var newPassengerId = await _passengerRepo.AddPassengerAsync(model);
                var Passenger = await _passengerRepo.GetPassengerByIdAsync(newPassengerId);
                if (Passenger != null)
                {
                    return Ok(new ApiResponse
                    {
                        Success = true,
                        Message = "Get " + NAMECONTROLLER + " success",
                        Data = Passenger
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
        public async Task<IActionResult> UpdatePassengerType(int id, PassengerDTO model)
        {
            try
            {
                if (id != model.PassengerId)
                {

                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "Update " + NAMECONTROLLER + " fail",
                        Data = null
                    });
                }
                await _passengerRepo.UpdatePassengerAsync(id, model);
                var Passenger = await _passengerRepo.GetPassengerByIdAsync(id);

                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Update " + NAMECONTROLLER + " success",
                    Data = Passenger
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
        public async Task<IActionResult> DeletePassengerType([FromRoute] int id)
        {
            try
            {
                var Passenger = await _passengerRepo.GetPassengerByIdAsync(id);
                if (Passenger == null)
                {
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "Delete " + NAMECONTROLLER + " fail",
                        Data = null
                    });
                }

                await _passengerRepo.DeletePassengerAsync(id);
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