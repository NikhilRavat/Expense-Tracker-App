using System.Text.Json;

namespace ExpenseApp.Services
{
    public interface IHttpService
    {
        HttpClient GetHttpClient();
        Task<T> HttpGet<T>(string url);
    }

    public class HttpService : IHttpService
    {
        private readonly IConfiguration _config;

        public HttpService(IConfiguration config)
        {
            _config = config;
        }

        public HttpClient GetHttpClient()
        {
            return new HttpClient()
            {
                BaseAddress = new Uri(_config.GetValue<string>("ApiUrl") ?? "")
            };
        }

        public async Task<T> HttpGet<T>(string url)
        {           
            var response = await GetHttpClient().GetAsync(url);
            var contentStr = await response.Content.ReadAsStringAsync();
            //var contentStream = await response.Content.ReadAsStreamAsync();

            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<T>(contentStr) ?? Activator.CreateInstance<T>();
            }
            return Activator.CreateInstance<T>();
        }
    }
}
