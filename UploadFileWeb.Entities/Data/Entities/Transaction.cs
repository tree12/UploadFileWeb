using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UploadFileWeb.Entities.Data.Entities
{
    public class Transaction : BaseEntity<Transaction>
    {
        [Key]
        public string TransactionId { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }
        public DateTime TransactionDate { get; set; }
        public char Status { get; set; }
        public override void Configure(EntityTypeBuilder<Transaction> builder) {
            base.Configure(builder);
            builder.Property(e => e.TransactionId)
            .HasMaxLength(50);
        }
    }
}
