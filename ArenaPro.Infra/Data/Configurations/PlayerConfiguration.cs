using ArenaPro.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArenaPro.Infra.Data.Configurations;
public class PlayerConfiguration : EntityConfiguration<Player>
{
    public override void Configure(EntityTypeBuilder<Player> builder)
    {
        base.Configure(builder);

        builder.Property(_ => _.Nick).IsRequired();
        builder.Property(_ => _.Name);
        builder.Property(_ => _.Age);
        builder.Property(_ => _.Genre);
        builder.Property(_ => _.Email);

        builder.HasOne(_ => _.Team).WithMany(_ => _.Players).HasForeignKey(_ => _.TeamId);
    }
}