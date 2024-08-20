using Microsoft.AspNetCore.Mvc;
using UploadFileWeb.API.Controllers.Base;
using UploadFileWeb.Shared.Interfaces;

namespace UploadFileWeb.API.Controllers
{
    public class FileLogController : BaseController<IFileLogService>
    {
        public FileLogController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
}
