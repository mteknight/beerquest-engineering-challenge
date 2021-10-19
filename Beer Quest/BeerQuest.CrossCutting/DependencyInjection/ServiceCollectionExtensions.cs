using BeerQuest.Data.Services;
using BeerQuest.Domain.Mapper;
using BeerQuest.Domain.Services;

using Microsoft.Extensions.DependencyInjection;

namespace BeerQuest.CrossCutting.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureDataDependencies(this IServiceCollection services)
        {
            return services
                .AddHttpClient()
                .AddSingleton<IHttpClientService, HttpClientService>();

        }

        public static IServiceCollection ConfigureDomainDependencies(this IServiceCollection services)
        {
            return services
                .AddSingleton<IPubMapper, PubMapper>()
                .AddSingleton<IPubService, PubService>();
        }
    }
}
