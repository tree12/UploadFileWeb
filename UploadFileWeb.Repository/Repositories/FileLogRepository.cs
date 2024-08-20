using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UploadFileWeb.Entities;
using UploadFileWeb.Entities.Data.Entities;
using UploadFileWeb.Entities.Interfaces;
using UploadFileWeb.Repository.Repositories.Base;

namespace UploadFileWeb.Repository.Repositories
{
    public class FileLogRepository : GenericRepository<FileLog, int>, IFileLogRepository
    {
        public FileLogRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
