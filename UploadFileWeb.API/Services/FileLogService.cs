using UploadFileWeb.API.Services.Base;
using UploadFileWeb.Entities.Data.Entities;
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
            List<FileLogDto> list = new List<FileLogDto>();
            var results = await unitOfWork.fileLogRepository.GetAll();
            foreach (var item in results)
            {
                list.Add(new FileLogDto() {
                Id = item.Id,
                    FileType = item.FileType,
                    FilePath = item.FilePath,
                    Success = item.Success,
                    ErrorMessage = item.ErrorMessage
                });
            }
            return list;
        }

        public async Task SaveFileLogDtoAsynce(FileLogDto fileLog)
        {
            try
            {
                var entity = new FileLog()
                {
                    FileType = fileLog.FileType,
                    FilePath = fileLog.FilePath,

                    Success = fileLog.Success,
                    ErrorMessage = fileLog.ErrorMessage,

                };
                await unitOfWork.fileLogRepository.Add(entity);
                unitOfWork.Complete();
            }
            catch (Exception ex) { 
            
            }
     
        }
    }
}
