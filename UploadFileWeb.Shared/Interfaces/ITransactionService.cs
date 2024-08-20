using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UploadFileWeb.Shared.Constants;
using UploadFileWeb.Shared.Models;

namespace UploadFileWeb.Shared.Interfaces
{
    public interface ITransactionService : IService
    {
        public Task<ReturnResult> UploadFileAsync(IFormFile? File, FileType fileType);
        public Task<List<TransactionDto>> GetTransactionsByCurrencyAsync(string currency);
        public Task<List<TransactionDto>> GetTransactionsByDateAsync(DateTime beginDate, DateTime endDate);
        public Task<List<TransactionDto>> GetTransactionsByStatusAsync(string status);
    }
}
