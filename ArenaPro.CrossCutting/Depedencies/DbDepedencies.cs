using ArenaPro.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ArenaPro.CrossCutting.Depedencies;
public static class DbDepedencies
{
    public static void AddDb(this IServiceCollection services, IConfiguration configuration)
    {
        var sqlServerConnection = configuration["Db"];
        services.AddDbContext<AppDbContext>(options => options.UseNpgsql(sqlServerConnection));
    }
}
