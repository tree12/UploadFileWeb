using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UploadFileWeb.Shared.Models;

namespace UploadFileWeb.Shared.Interfaces
{
    public interface IFileLogService: IService
    {
        public Task<List<FileLogDto>> GetFileLogDtosAsync();
        public Task SaveFileLogDtoAsynce(FileLogDto fileLog);
    }
}
