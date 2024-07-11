using ArenaPro.Domain.Entities;
using ArenaPro.Infra.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace ArenaPro.Infra.Data;
public class AppDbContext : DbContext
{
    public DbSet<Team> Teams { get; set; }
    public DbSet<Tournament> Tournaments { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<Match> Matches { get; set; }
    public DbSet<MatchPlayerKda> MatchPlayerKdas { get; set; }
    public DbSet<TeamMatch> TeamMatches { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TournamentConfiguration());
        modelBuilder.ApplyConfiguration(new MatchConfiguration());
        modelBuilder.ApplyConfiguration(new PlayerConfiguration());
        modelBuilder.ApplyConfiguration(new TeamConfiguration());
        modelBuilder.ApplyConfiguration(new TeamMatchConfiguration());
        modelBuilder.ApplyConfiguration(new MatchPlayerKdaConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
