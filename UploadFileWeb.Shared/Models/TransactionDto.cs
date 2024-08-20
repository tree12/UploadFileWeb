using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UploadFileWeb.Shared.Models
{
    public class TransactionDto : BaseDto
    {
        public string TransactionId { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }
        public DateTime TransactionDate { get; set; }
        public char Status { get; set; }
        public string CStatus { get; set; }
    }
}
