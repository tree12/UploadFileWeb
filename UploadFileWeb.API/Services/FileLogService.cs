using UploadFileWeb.API.Services.Base;
using UploadFileWeb.Shared.Interfaces;
using UploadFileWeb.Shared.Models;

namespace UploadFileWeb.API.Services
{
    public class FileLogService : BaseService, IFileLogService
    {
        public FileLogService(IServiceProvider services) : base(services)
        {
        }

        public async Task<List<FileLogDto>> GetFileLogDtosAsync()
        {
            throw new NotImplementedException();
        }
    }
}
