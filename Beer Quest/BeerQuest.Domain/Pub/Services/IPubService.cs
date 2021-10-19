using System.Threading;
using System.Threading.Tasks;

namespace BeerQuest.Domain.Services
{
    public interface IPubService
    {
        Task<Pub?> Get(
            string name,
            CancellationToken cancellationToken = default);
    }
}
