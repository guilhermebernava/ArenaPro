using ArenaPro.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArenaPro.Infra.Data.Configurations;
public class TeamMatchConfiguration : IEntityTypeConfiguration<TeamMatch>
{
    public virtual void Configure(EntityTypeBuilder<TeamMatch> builder)
    {
        builder.HasKey(_ => new { _.TeamId, _.MatchId });
    }
}