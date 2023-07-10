using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace UndoAssessment.Services
{
    public class RequestService : IRequestService
    {
        private static readonly HttpClient _httpClient = new HttpClient()
        {
            BaseAddress = new Uri("https://malkarakundostagingpublicapi.azurewebsites.net")
        };

        public async Task<T> Get<T>(string url, CancellationToken cancellationToken = default)
        {
            var request = await _httpClient.GetAsync(url, cancellationToken);

            var content = await request.Content.ReadAsStringAsync();

            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var result = JsonSerializer.Deserialize<T>(content, options);

            return result;
        }
    }
}
