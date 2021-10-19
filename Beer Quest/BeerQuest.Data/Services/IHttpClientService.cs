using System.Threading;
using System.Threading.Tasks;

namespace BeerQuest.Data.Services
{
    public interface IHttpClientService
    {
        Task<T?> Get<T>(
            string uri,
            CancellationToken cancellationToken = default);
    }
}
