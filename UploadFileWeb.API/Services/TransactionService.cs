using UploadFileWeb.API.Services.Base;
using UploadFileWeb.Shared.Constants;
using UploadFileWeb.Shared.Interfaces;
using UploadFileWeb.Shared.Models;

namespace UploadFileWeb.API.Services
{
    public class TransactionService : BaseService, ITransactionService
    {
        public TransactionService(IServiceProvider services) : base(services)
        {
        }

        public async Task<List<TransactionDto>> GetTransactionsByCurrencyAsync(string currency)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TransactionDto>> GetTransactionsByDateAsync(DateTime beginDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TransactionDto>> GetTransactionsByStatusAsync(string status)
        {
            throw new NotImplementedException();
        }

        public async Task<ReturnResult> UploadFileAsync(IFormFile? File, FileType fileType)
        {
            throw new NotImplementedException();
        }
    }
}
