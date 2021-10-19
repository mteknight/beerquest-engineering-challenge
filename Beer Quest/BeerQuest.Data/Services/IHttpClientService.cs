using System.Threading;
using System.Threading.Tasks;

using BeerQuest.Data.Models;

namespace BeerQuest.Data.Services
{
    public interface IHttpClientService
    {
        Task<PubResponseData?> Get<T>(
            string uri,
            CancellationToken cancellationToken = default);
    }
}
