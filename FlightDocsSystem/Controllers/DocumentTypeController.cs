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

        public DocumentTypeController(IFlightDocumentTypeResponsitory flightDocumentTypeRepo)
        {
            _flightDocumentTypeRepo = flightDocumentTypeRepo;
        }

        const string NAMECONTROLLER = "FlightDocumentType"; //hien thi ten cua thong bao


        [HttpGet]
        public async Task<IActionResult> GetAllDocumentTypeType()
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
        public async Task<IActionResult> GetDocumentTypeTypeById(int id)
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
        public async Task<IActionResult> AddDocumentTypeType(FlightDocumentTypeDTO model)
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
        public async Task<IActionResult> UpdateDocumentTypeType(int id, FlightDocumentTypeDTO model)
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
        public async Task<IActionResult> DeleteDocumentTypeType([FromRoute] int id)
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