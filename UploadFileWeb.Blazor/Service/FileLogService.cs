namespace UploadFileWeb.Blazor.Service
{
    public class FileLogService
    {
        protected readonly HttpClient _httpClient;

        FileLogClient fileLogClient = null;
        public FileLogService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            fileLogClient = new FileLogClient(httpClient.BaseAddress?.AbsoluteUri, httpClient);

        }
        public async Task<List<FileLogDto>> GetFileLogDtosAsync()
        {
            var results = await fileLogClient.GetFileLogsAsync();
            return results.ToList();
        }
    }
}
