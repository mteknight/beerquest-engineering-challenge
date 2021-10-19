using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using BeerQuest.Data.Services;
using BeerQuest.Domain.Mapper;

using Dawn;

namespace BeerQuest.Domain.Services
{
    public class PubService : IPubService
    {
        private const string Api = "https://datamillnorth.org/api/table/wk7xz_hf8bv";
        private readonly IHttpClientService httpClientService;
        private readonly IPubMapper mapper;

        public PubService(
            IHttpClientService httpClientService,
            IPubMapper mapper)
        {
            this.httpClientService = Guard.Argument(httpClientService, nameof(httpClientService)).NotNull().Value;
            this.mapper = Guard.Argument(mapper, nameof(mapper)).NotNull().Value;
        }

        public async Task<Pub?> Get(
            string name,
            CancellationToken cancellationToken = default)
        {
            Guard.Argument(name, nameof(name)).NotNull().NotEmpty().NotWhiteSpace();

            var uri = $"{Api}?name={name}";

            try
            {
                var data = await this.httpClientService.Get<Pub>(uri, cancellationToken);

                return this.mapper
                    .Map(data)
                    .SingleOrDefault();
            }
            catch (HttpRequestException e)
            {
                return default;
            }
        }
    }
}
