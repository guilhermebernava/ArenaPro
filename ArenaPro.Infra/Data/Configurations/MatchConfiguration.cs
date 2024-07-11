using ArenaPro.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArenaPro.Infra.Data.Configurations;
public class MatchConfiguration : EntityConfiguration<Match>
{
    public override void Configure(EntityTypeBuilder<Match> builder)
    {
        base.Configure(builder);

        builder.Property(_ => _.Ended).IsRequired();
        builder.Property(_ => _.MatchDate).IsRequired();

        builder.HasMany(_ => _.Teams).WithMany(_ => _.Matches);
        builder.HasMany(_ => _.MatchesResults).WithOne(_ => _.Match).HasForeignKey(_ => _.MatchId);
        builder.HasMany(_ => _.MatchPlayerKdas).WithOne(_ => _.Match).HasForeignKey(_ => _.MatchId);
    }
}