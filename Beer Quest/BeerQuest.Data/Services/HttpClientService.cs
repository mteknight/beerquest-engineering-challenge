using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using BeerQuest.Data.Models;

using Dawn;

using Newtonsoft.Json;

namespace BeerQuest.Data.Services
{
    public class HttpClientService : IHttpClientService
    {
        private readonly IHttpClientFactory httpClientFactory;

        public HttpClientService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = Guard.Argument(httpClientFactory, nameof(httpClientFactory)).NotNull().Value;
        }

        public async Task<PubResponseData?> Get<T>(
            string uri,
            CancellationToken cancellationToken = default)
        {
            using var client = this.httpClientFactory.CreateClient();
            var response = await client.GetAsync(uri, cancellationToken);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync(cancellationToken);

            return JsonConvert.DeserializeObject<PubResponseData>(json);
        }
    }
}
