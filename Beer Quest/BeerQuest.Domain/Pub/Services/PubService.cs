using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using BeerQuest.Data.Services;

using Dawn;

namespace BeerQuest.Domain.Services
{
    public class PubService : IPubService
    {
        private const string Api = "https://datamillnorth.org/api/table/wk7xz_hf8bv";
        private readonly IHttpClientService httpClientService;

        public PubService(IHttpClientService httpClientService)
        {
            this.httpClientService = Guard.Argument(httpClientService, nameof(httpClientService)).NotNull().Value;
        }

        public async Task<Pub?> Get(
            string name,
            CancellationToken cancellationToken = default)
        {
            Guard.Argument(name, nameof(name)).NotNull().NotEmpty().NotWhiteSpace();

            var uri = $"{Api}?name={name}";

            try
            {
                return await this.httpClientService.Get<Pub>(uri, cancellationToken);
            }
            catch (HttpRequestException e)
            {
                return default;
            }
        }
    }
}
