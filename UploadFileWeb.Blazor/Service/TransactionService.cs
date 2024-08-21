
using Microsoft.AspNetCore.Components.Forms;

namespace UploadFileWeb.Blazor.Service
{
    public class TransactionService 
    {
        protected readonly HttpClient _httpClient;

        TransactionClient transactionClient = null;
        public TransactionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            transactionClient = new TransactionClient(httpClient.BaseAddress?.AbsoluteUri, httpClient);

        }

        public async Task<List<TransactionDto>> GetTransactionsByCurrencyAsync(string currency)
        {
            var results = await transactionClient.GetTransactionsByCurrencyAsync(currency);
            return results.ToList();
        }

        public async Task<List<TransactionDto>> GetTransactionsByDateAsync(DateTime beginDate, DateTime endDate)
        {
            var results = await transactionClient.GetTransactionsByDateAsync(beginDate, endDate);
            return results.ToList();
        }

        public async Task<List<TransactionDto>> GetTransactionsByStatusAsync(string status)
        {
            var results = await transactionClient.GetTransactionsByStatusAsync(status);
            return results.ToList();
        }

        public async Task UploadFileAsync(IBrowserFile file)
        {
            using (var ms = new MemoryStream())
            {
                await file.OpenReadStream().CopyToAsync(ms);
                ms.Seek(0, SeekOrigin.Begin);
                await transactionClient.UploadFileAsync(new FileParameter(ms, file.Name, file.ContentType));
            }

        }
        public async Task UploadFileAsync(Stream stream, string fileName, string contentType)
        {
            MemoryStream ms = new MemoryStream();
            stream.CopyTo(ms);
            await transactionClient.UploadFileAsync(new FileParameter(ms, fileName, contentType));

        }

    }
}
