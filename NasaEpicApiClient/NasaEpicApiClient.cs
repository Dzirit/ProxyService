using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NasaEpicApiClient.Models;
using Newtonsoft.Json;

namespace NasaEpicApiClient
{
    public class NasaEpicApiClient : INasaEpicApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public NasaEpicApiClient(NasaEpicApiClientSettings nasaEpicApiClientSettings)
        {
            var url = nasaEpicApiClientSettings.Url;
            _apiKey = nasaEpicApiClientSettings.ApiKey;
            var handler = new HttpClientHandler
            {
                UseCookies = false // оставить так, чтобы куки прописывать в httpRequestMessage, иначе они будут игнорироваться и браться из CookieContainer
            };
            _httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri(url),
            };
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private async Task<TResponse> GetResponse<TResponse>(string url, CancellationToken cancellationToken)
        {
            var msg = new HttpRequestMessage(HttpMethod.Get, url);
            using (var httpResponseMessage = await _httpClient.SendAsync(msg, cancellationToken).ConfigureAwait(false))
            {
                if (httpResponseMessage.StatusCode == HttpStatusCode.NotFound)
                {
                    return default!;
                }

                httpResponseMessage.EnsureSuccessStatusCode();

                var content = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                var result = JsonConvert.DeserializeObject<TResponse>(content);
                return result;
            }
        }

        private async Task<RawImage> GetRawImage(string url, CancellationToken cancellationToken)
        {
            var msg = new HttpRequestMessage(HttpMethod.Get, url);
            using (var httpResponseMessage = await _httpClient.SendAsync(msg, cancellationToken).ConfigureAwait(false))
            {
                if (httpResponseMessage.StatusCode == HttpStatusCode.NotFound)
                {
                    return default!;
                }

                httpResponseMessage.EnsureSuccessStatusCode();

                var content = await httpResponseMessage.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
                var result = new RawImage() { ImageData = content };
                return result;
            }
        }

        public Task<IReadOnlyCollection<DateTime>> GetNaturalImageAvailableDates(CancellationToken cancellationToken = default)
        {
            return GetResponse<IReadOnlyCollection<DateTime>>($"api/natural/available/", cancellationToken);
        }

        public Task<IReadOnlyCollection<ImageModel>> GetImagesForDate(DateTime date, CancellationToken cancellationToken = default)
        {
            return GetResponse<IReadOnlyCollection<ImageModel>>($"api/natural/date/{date:yyyy-MM-dd}", cancellationToken);
        }

        public Task<RawImage> GetImageByDateName(DateTime date, string format, string name, CancellationToken cancellationToken = default)
        {
            return GetRawImage($"archive/natural/{date:yyyy}/{date:MM}/{date:dd}/{format}/epic_1b_{name}.{format}?", cancellationToken);
        }
    }
}
