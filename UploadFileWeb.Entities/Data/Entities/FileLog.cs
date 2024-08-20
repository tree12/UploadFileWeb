using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UploadFileWeb.Entities.Data.Entities
{
    public class FileLog : BaseEntity<FileLog>
    {
        [Key]
        public int Id { get; set; }

        public string TransactionId { get; set; }
        public string FileType { get; set; }
        public string FilePath { get; set; }

        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }

        [ForeignKey("TransactionId")]
        public Transaction Transaction { get; set; }
        public override void Configure(EntityTypeBuilder<FileLog> builder)
        {
            base.Configure(builder);
            builder.Property(e => e.Success).HasDefaultValue(false);
        }
    }
}
