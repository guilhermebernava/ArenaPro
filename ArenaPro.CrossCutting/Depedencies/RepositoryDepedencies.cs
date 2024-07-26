using ArenaPro.Domain.Abstractions;
using ArenaPro.Infra.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ArenaPro.CrossCutting.Depedencies;

public static class RepositoryDepedencies
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ITeamRepository, TeamRepository>();
        services.AddScoped<ITournamentRepository, TournamentRepository>();
        services.AddScoped<IMatchRepository, MatchRepository>();
        services.AddScoped<IPlayerRepository, PlayerRepository>();
    }
}