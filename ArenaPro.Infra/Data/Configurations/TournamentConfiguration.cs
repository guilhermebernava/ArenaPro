using ArenaPro.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArenaPro.Infra.Data.Configurations;
public class TournamentConfiguration : EntityConfiguration<Tournament>
{
    public override void Configure(EntityTypeBuilder<Tournament> builder)
    {
        base.Configure(builder);

        builder.Property(_ => _.Name).IsRequired();
        builder.Property(_ => _.Ended).IsRequired();
        builder.Property(_ => _.Prize);

        builder.HasMany(_ => _.Teams)
               .WithMany(_ => _.Tournaments);

        builder.HasMany(_ => _.Matches)
               .WithOne(_ => _.Tournament)
               .HasForeignKey(_ => _.TournamentId);
    }
}