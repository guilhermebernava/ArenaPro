using ArenaPro.CrossCutting.Depedencies;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ArenaPro.CrossCutting;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        DbDepedencies.AddDb(services, configuration);
        RepositoryDepedencies.AddRepositories(services);
        ServicesDepedencies.AddServices(services);

        return services;
    }
}
