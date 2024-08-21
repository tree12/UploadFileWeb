using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic.FileIO;
using UploadFileWeb.API.Controllers.Base;
using UploadFileWeb.Entities.Data.Entities;
using UploadFileWeb.Shared.Constants;
using UploadFileWeb.Shared.Interfaces;
using UploadFileWeb.Shared.Models;

namespace UploadFileWeb.API.Controllers
{
    public class TransactionController : BaseController<ITransactionService>
    {
        IFileLogService fileLogService { get; set; }
        public TransactionController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            fileLogService = serviceProvider.GetService<IFileLogService>();
        }
        [HttpPost("UploadFile")]
        [RequestSizeLimit(1_000_000)]
        public async Task<ActionResult> UploadFileAsync(IFormFile fileDetails)
        {
            //Save Log file every time. IF I have more time I will separate this function
            if (fileDetails == null)
            {
                return BadRequest("File is empty");
            }

            try
            {
                string contentType = fileDetails.ContentType; 
                if (FileType.permittedContentTypes.Contains(contentType.ToLower()))
                {

                    ReturnResult returnResult = contentType.ToLower() == "text/csv" ? await service.UploadFileCSVAsync(fileDetails) : await service.UploadFileXMLAsync(fileDetails);
                    if (returnResult.Success)
                    {
                        await fileLogService.SaveFileLogDtoAsynce(new FileLogDto()
                        {
                            FileType = contentType,
                            FilePath = fileDetails.FileName,
                            Success = true
                        });
                        return Ok();
                    }
                    else
                    {
                        await fileLogService.SaveFileLogDtoAsynce(new FileLogDto()
                        {
                            FileType = contentType,
                            FilePath = fileDetails.FileName,
                            Success = false,
                            ErrorMessage = returnResult.Message

                        });
                        return BadRequest(returnResult.Message);
                    }

                }
                else {
                    await fileLogService.SaveFileLogDtoAsynce(new FileLogDto()
                    {
                        FileType = contentType,
                        FilePath = fileDetails.FileName,
                        Success = false,
                        ErrorMessage = "Unknown format"

                    });
                    return BadRequest("Unknown format");
                }
                   

            }
            catch (Exception e)
            {
                await fileLogService.SaveFileLogDtoAsynce(new FileLogDto()
                {
                    FileType = fileDetails.ContentType,
                    FilePath = fileDetails.FileName,
                    Success = false,
                    ErrorMessage = e.Message

                });
                return BadRequest(e.Message);
            }
        }
        [HttpGet("GetTransactionsByCurrency")]
        public async Task<List<TransactionDto>> GetTransactionsByCurrencyAsync(string currency)
        {

            return await service.GetTransactionsByCurrencyAsync(currency);
        }
        [HttpGet("GetTransactionsByDate")]
        public async Task<List<TransactionDto>> GetTransactionsByDateAsync(DateTime beginDate, DateTime endDate)
        {
            return await service.GetTransactionsByDateAsync(beginDate, endDate);
        }
        [HttpGet("GetTransactionsByStatus")]
        public async Task<List<TransactionDto>> GetTransactionsByStatusAsync(string cstatus)
        {
            string status = TransactionCSVStatus.GetMemberValue(cstatus);
            if (string.IsNullOrEmpty(status))
                status = TransactionXMLStatus.GetMemberValue(cstatus);
            return await service.GetTransactionsByStatusAsync(status);
        }
    }
}
