using Microsoft.AspNetCore.Mvc;
using UploadFileWeb.API.Controllers.Base;
using UploadFileWeb.Shared.Interfaces;
using UploadFileWeb.Shared.Models;

namespace UploadFileWeb.API.Controllers
{
    public class FileLogController : BaseController<IFileLogService>
    {
        public FileLogController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
        [HttpGet("GetFileLogs")]
        public async Task<List<FileLogDto>> GetFileLogDtosAsync() { 
            return await service.GetFileLogDtosAsync();
        }
    }
}
