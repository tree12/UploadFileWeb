using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic.FileIO;
using UploadFileWeb.API.Controllers.Base;
using UploadFileWeb.Shared.Constants;
using UploadFileWeb.Shared.Interfaces;
using UploadFileWeb.Shared.Models;

namespace UploadFileWeb.API.Controllers
{
    public class TransactionController : BaseController<ITransactionService>
    {
        public TransactionController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
        [HttpPost("UploadFile")]
        [RequestSizeLimit(1_000_000)]
        public async Task<ActionResult> UploadFileAsync(IFormFile fileDetails)
        {
            if (fileDetails == null)
            {
                return BadRequest("File is empty");
            }

            try
            {
                string contentType = fileDetails.ContentType; //Path.GetExtension(fileDetails.FileName);
                if (FileType.permittedContentTypes.Contains(contentType.ToLower())) {

                    ReturnResult returnResult = contentType.ToLower() == "text/csv" ? await service.UploadFileCSVAsync(fileDetails): await service.UploadFileXMLAsync(fileDetails);
                    if(returnResult.Success)
                        return Ok();
                    else
                        return BadRequest(returnResult.Message);
                } 
                else
                    return BadRequest("Unknown format");
                
                
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("GetTransactionsByCurrency")]
        public async Task<List<TransactionDto>> GetTransactionsByCurrencyAsync(string currency) {

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
            if(string.IsNullOrEmpty(status))
                status = TransactionXMLStatus.GetMemberValue(cstatus);
            return await service.GetTransactionsByStatusAsync(status);
        }
    }
}
