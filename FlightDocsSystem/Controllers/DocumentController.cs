using FlightDocsSystem.DataAccess.Repository.IRepository;
using FlightDocsSystem.Helper;
using FlightDocsSystem.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace FlightDocsSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentResponsitory _documentRepo;
        private readonly IFirebaseStorageService _firebaseStorageService;
        private readonly IFlightResponsitory _flightRepo;

        public DocumentController(IDocumentResponsitory documentRepo, IFirebaseStorageService firebaseStorageService, IFlightResponsitory flightRepo)
        {
            this._flightRepo = flightRepo;
            this._documentRepo = documentRepo;
            this._firebaseStorageService = firebaseStorageService;
        }

        const string NAMECONTROLLER = "Document"; //hien thi ten cua thong bao


        [HttpGet]
        public async Task<IActionResult> GetAllDocument(string? searchKeyword)
        {
            try
            {
                var currentPage = 1;
                var pageSize = 5;
                var Documents = await _documentRepo.GetAllDocumentAsync(currentPage, pageSize, searchKeyword);
                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Get all " + NAMECONTROLLER + " success",
                    Data = Documents
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
        public async Task<IActionResult> GetDocumentById(int id)
        {
            try
            {
                var Document = await _documentRepo.GetDocumentByIdAsync(id);
                if (Document != null)
                {
                    return Ok(new ApiResponse
                    {
                        Success = true,
                        Message = "Get " + NAMECONTROLLER + " success",
                        Data = Document
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
        public async Task<IActionResult> AddDocument(IFormFile docFile, IFormFile coverFile, int flightId, DateTime expirationDate, int documentTypeId, int usedId)
        {
            try
            {
                if (docFile == null || docFile.Length == 0 || coverFile == null || coverFile.Length == 0)
                {
                    return BadRequest("No file selected.");
                }
                var fileName = Path.GetFileName(docFile.FileName);
                var docUrl = await _firebaseStorageService.UploadFile(docFile, "0.1");
                var coverUrl = await _firebaseStorageService.UploadFile(coverFile, null);
                var model = new DocumentDTO
                {
                    DocumentName = fileName,
                    DocumentVersion = "0.1",
                    DocumentPath = docUrl,
                    CoverPath = coverUrl,
                    FlightId = flightId,
                    ExpirationDate = expirationDate,
                    DocumentTypeId = documentTypeId,
                    UserId = usedId
                };

                var newDocumentId = await _documentRepo.AddDocumentAsync(model);
                var Document = await _documentRepo.GetDocumentByIdAsync(newDocumentId);
                if (Document != null)
                {
                    return Ok(new ApiResponse
                    {
                        Success = true,
                        Message = "Get " + NAMECONTROLLER + " success",
                        Data = Document
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
        public async Task<IActionResult> UpdateDocument(IFormFile docFile, int documentId, int flightId, DateTime expirationDate, int documentTypeId, int usedId)
        {
            try
            {
                if (docFile == null || docFile.Length == 0)
                {
                    return BadRequest("No file selected.");
                }
                var document = _documentRepo.GetDocumentByIdAsync(documentId);
                if (document != null)
                {
                    var flight = await _flightRepo.GetFlightByIdAsync(flightId);
                    if (flight.DepartureTime != null)
                    {
                        return BadRequest(new ApiResponse
                        {
                            Success = false,
                            Message = "Flight was end",
                            Data = null
                        });
                    }
                    var fileName = Path.GetFileName(docFile.FileName);
                    var docUrl = await _firebaseStorageService.UploadFile(docFile, "0.1");
                    var model = new DocumentDTO
                    {
                        DocumentName = fileName,
                        DocumentVersion = "0.1",
                        DocumentPath = docUrl,
                        FlightId = flightId,
                        ExpirationDate = expirationDate,
                        DocumentTypeId = documentTypeId,
                        UserId = usedId
                    };
                    await _documentRepo.UpdateDocumentAsync(document.Id, model);

                    return Ok(new ApiResponse
                    {
                        Success = true,
                        Message = "Get " + NAMECONTROLLER + " success",
                        Data = document
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocument([FromRoute] int id)
        {
            try
            {
                var Document = await _documentRepo.GetDocumentByIdAsync(id);
                if (Document == null)
                {
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "Delete " + NAMECONTROLLER + " fail",
                        Data = null
                    });
                }

                await _documentRepo.DeleteDocumentAsync(id);
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