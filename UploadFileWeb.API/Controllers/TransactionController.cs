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
                string extension = Path.GetExtension(fileDetails.FileName);
                if (FileType.permittedExtensions.Contains(extension.ToUpper())) {

                    ReturnResult returnResult = extension.ToUpper() == ".CSV"? await service.UploadFileCSVAsync(fileDetails): await service.UploadFileXMLAsync(fileDetails);
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

    }
}
