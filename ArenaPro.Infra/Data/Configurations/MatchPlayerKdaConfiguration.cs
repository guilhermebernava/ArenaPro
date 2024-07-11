using ArenaPro.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArenaPro.Infra.Data.Configurations;
public class MatchPlayerKdaConfiguration : IEntityTypeConfiguration<MatchPlayerKda>
{
    public virtual void Configure(EntityTypeBuilder<MatchPlayerKda> builder)
    {
        builder.HasKey(_ => new { _.PlayerId, _.MatchId });
    }
}