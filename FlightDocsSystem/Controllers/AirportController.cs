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
    public class AirportController : ControllerBase
    {
        private readonly IAirportResponsitory _airportRepo;

        public AirportController(IAirportResponsitory AirportTypeRepo)
        {
            _airportRepo = AirportTypeRepo;
        }

        const string NAMECONTROLLER = "Airport"; //hien thi ten cua thong bao


        [HttpGet]
        public async Task<IActionResult> GetAllAirportType()
        {
            try
            {
                var Airports = await _airportRepo.GetAllAirportAsync();
                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Get all " + NAMECONTROLLER + " success",
                    Data = Airports
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
        public async Task<IActionResult> GetAirportTypeById(int id)
        {
            try
            {
                var Airport = await _airportRepo.GetAirportByIdAsync(id);
                if (Airport != null)
                {
                    return Ok(new ApiResponse
                    {
                        Success = true,
                        Message = "Get " + NAMECONTROLLER + " success",
                        Data = Airport
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
        public async Task<IActionResult> AddAirportType(AirportDTO model)
        {
            try
            {
                var newAirportId = await _airportRepo.AddAirportAsync(model);
                var Airport = await _airportRepo.GetAirportByIdAsync(newAirportId);
                if (Airport != null)
                {
                    return Ok(new ApiResponse
                    {
                        Success = true,
                        Message = "Get " + NAMECONTROLLER + " success",
                        Data = Airport
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
        public async Task<IActionResult> UpdateAirportType(int id, AirportDTO model)
        {
            try
            {
                if (id != model.AirportId)
                {

                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "Update " + NAMECONTROLLER + " fail",
                        Data = null
                    });
                }
                await _airportRepo.UpdateAirportAsync(id, model);
                var Airport = await _airportRepo.GetAirportByIdAsync(id);

                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Update " + NAMECONTROLLER + " success",
                    Data = Airport
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
        public async Task<IActionResult> DeleteAirportType([FromRoute] int id)
        {
            try
            {
                var Airport = await _airportRepo.GetAirportByIdAsync(id);
                if (Airport == null)
                {
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "Delete " + NAMECONTROLLER + " fail",
                        Data = null
                    });
                }

                await _airportRepo.DeleteAirportAsync(id);
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