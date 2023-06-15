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
    public class DocumentTypeController : ControllerBase
    {
        private readonly IFlightDocumentTypeResponsitory _flightDocumentTypeRepo;
        const string NAMECONTROLLER = "FlightDocumentType"; //hien thi ten cua thong bao
        public DocumentTypeController(IFlightDocumentTypeResponsitory flightDocumentTypeRepo)
        {
            _flightDocumentTypeRepo = flightDocumentTypeRepo;
        }




        [HttpGet]
        public async Task<IActionResult> GetAllDocumentType()
        {
            try
            {
                var DocumentTypes = await _flightDocumentTypeRepo.GetAllFlightDocumentTypeAsync();
                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Get all " + NAMECONTROLLER + " success",
                    Data = DocumentTypes
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
        public async Task<IActionResult> GetDocumentTypeById(int id)
        {
            try
            {
                var DocumentType = await _flightDocumentTypeRepo.GetFlightDocumentTypeByIdAsync(id);
                if (DocumentType != null)
                {
                    return Ok(new ApiResponse
                    {
                        Success = true,
                        Message = "Get " + NAMECONTROLLER + " success",
                        Data = DocumentType
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
        public async Task<IActionResult> AddDocumentType(FlightDocumentTypeDTO model)
        {
            try
            {
                var newDocumentTypeId = await _flightDocumentTypeRepo.AddFlightDocumentTypeAsync(model);
                var DocumentType = await _flightDocumentTypeRepo.GetFlightDocumentTypeByIdAsync(newDocumentTypeId);
                if (DocumentType != null)
                {
                    return Ok(new ApiResponse
                    {
                        Success = true,
                        Message = "Get " + NAMECONTROLLER + " success",
                        Data = DocumentType
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
        public async Task<IActionResult> UpdateDocumentType(int id, FlightDocumentTypeDTO model)
        {
            try
            {
                if (id != model.DocumentTypeId)
                {

                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "Update " + NAMECONTROLLER + " fail",
                        Data = null
                    });
                }
                await _flightDocumentTypeRepo.UpdateFlightDocumentTypeAsync(id, model);
                var DocumentType = await _flightDocumentTypeRepo.GetFlightDocumentTypeByIdAsync(id);

                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Update " + NAMECONTROLLER + " success",
                    Data = DocumentType
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
        public async Task<IActionResult> DeleteDocumentType([FromRoute] int id)
        {
            try
            {
                var DocumentType = await _flightDocumentTypeRepo.GetFlightDocumentTypeByIdAsync(id);
                if (DocumentType == null)
                {
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "Delete " + NAMECONTROLLER + " fail",
                        Data = null
                    });
                }

                await _flightDocumentTypeRepo.DeleteFlightDocumentTypeAsync(id);
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