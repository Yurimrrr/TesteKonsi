using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using TesteKonsi.Domain.Contracts.Infra.Services;

namespace TesteKonsi.Infra.Services.Utils
{
    public class HttpRequestService : IHttpRequestService
    {
        private static readonly HttpClient HttpClient = new HttpClient();

        public async Task<T?> HttpRequest<T>
            (string url, string resource, string body, HttpMethod method, string? token = "")
        {
            object? content = null;
            var uri = new Uri($"{url}{resource}");

            var request = new HttpRequestMessage
            {
                Method = method,
                RequestUri = uri,
                Content = new StringContent(body, Encoding.UTF8, "application/json")
            };

            if (token != String.Empty)
            {
                HttpClient.DefaultRequestHeaders.Authorization 
                    = new AuthenticationHeaderValue("Bearer", token);
            }

            var resultado = await HttpClient.SendAsync(request);

            if (resultado.StatusCode != System.Net.HttpStatusCode.OK)
                throw new HttpRequestException($"{resultado.StatusCode}-{resultado.RequestMessage}");

            content = JsonConvert.DeserializeObject<T>(resultado.Content.ReadAsStringAsync().Result);

            return (T)content;
        }
    }
}
