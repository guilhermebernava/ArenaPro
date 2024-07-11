using ArenaPro.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArenaPro.Infra.Data.Configurations;
public class TeamConfiguration : EntityConfiguration<Team>
{
    public override void Configure(EntityTypeBuilder<Team> builder)
    {
        base.Configure(builder);

        builder.Property(_ => _.Name).IsRequired();
        builder.Property(_ => _.Logo);

        builder.HasMany(_ => _.Players).WithOne(_ => _.Team).HasForeignKey(_ => _.TeamId);
        builder.HasMany(_ => _.MatchesResults).WithOne(_ => _.Team).HasForeignKey(_ => _.TeamId);
        builder.HasMany(_ => _.Tournaments).WithMany(_ => _.Teams);
        builder.HasMany(_ => _.Matches).WithMany(_ => _.Teams);
    }
}