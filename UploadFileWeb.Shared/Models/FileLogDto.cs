using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UploadFileWeb.Shared.Models
{
    public class FileLogDto : BaseDto
    {
        public int Id { get; set; }

        public string TransactionId { get; set; }
        public string FileType { get; set; }
        public string FilePath { get; set; }

        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }

        public TransactionDto? Transaction { get; set; }
    }
}
