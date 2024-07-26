using ArenaPro.Application.Abstractions.MatchServices;
using ArenaPro.Application.Abstractions.PlayerServices;
using ArenaPro.Application.Abstractions.TeamServices;
using ArenaPro.Application.Abstractions.TournamentServices;
using ArenaPro.Application.Services.MatchServices;
using ArenaPro.Application.Services.PlayerServices;
using ArenaPro.Application.Services.TeamServices;
using ArenaPro.Application.Services.TournamentServices;
using Microsoft.Extensions.DependencyInjection;

namespace ArenaPro.CrossCutting.Depedencies;

public static class ServicesDepedencies
{
    public static void AddServices(this IServiceCollection services)
    {
        #region TEAM SERVICES
        services.AddScoped<ITeamCreateServices, TeamCreateServices>();
        services.AddScoped<ITeamDeleteServices, TeamDeleteServices>();
        services.AddScoped<ITeamGetAllServices, TeamGetAllServices>();
        services.AddScoped<ITeamGetByIdServices, TeamGetByIdServices>();
        services.AddScoped<ITeamGetByNameServices, TeamGetByNameServices>();
        services.AddScoped<ITeamUpdateServices, TeamUpdateServices>();
        #endregion

        #region TOURNAMENT SERVICES
        services.AddScoped<ITournamentCreateServices, TournamentCreateServices>();
        services.AddScoped<ITournamentDeleteServices, TournamentDeleteServices>();
        services.AddScoped<ITournamentGetAllServices, TournamentGetAllServices>();
        services.AddScoped<ITournamentGetByIdServices, TournamentGetByIdServices>();
        services.AddScoped<ITournamentGetByNameServices, TournamentGetByNameServices>();
        services.AddScoped<ITournamentUpdateServices, TournamentUpdateServices>();
        #endregion

        #region PLAYER SERVICES
        services.AddScoped<IPlayerCreateServices, PlayerCreateServices>();
        services.AddScoped<IPlayerDeleteServices, PlayerDeleteServices>();
        services.AddScoped<IPlayerGetAllServices, PlayerGetAllServices>();
        services.AddScoped<IPlayerGetByIdServices, PlayerGetByIdServices>();
        services.AddScoped<IPlayerGetByNickServices, PlayerGetByNickServices>();
        services.AddScoped<IPlayerGetByTeamIdServices, PlayerGetByTeamIdServices>();
        services.AddScoped<IPlayerUpdateServices, PlayerUpdateServices>();
        #endregion

        #region MATCH SERVICES
        services.AddScoped<IMatchAddMatchResultServices, MatchAddMatchResultServices>();
        services.AddScoped<IMatchAddPlayerKdaServices, MatchAddPlayerKdaServices>();
        services.AddScoped<IMatchCreateServices, MatchCreateServices>();
        services.AddScoped<IMatchDeleteServices, MatchDeleteServices>();
        services.AddScoped<IMatchGetAllServices, MatchGetAllServices>();
        services.AddScoped<IMatchGetByIdServices, MatchGetByIdServices>();
        services.AddScoped<IMatchGetByDateServices, MatchGetByDateServices>();
        services.AddScoped<IMatchGetByTournamentIdServices, MatchGetByTournamentIdServices>();
        services.AddScoped<IMatchRemoveMatchResultServices, MatchRemoveMatchResultServices>();
        services.AddScoped<IMatchRemovePlayerKdaServices, MatchRemovePlayerKdaServices>();
        services.AddScoped<IMatchUpdateServices, MatchUpdateServices>();
        #endregion
    }
}