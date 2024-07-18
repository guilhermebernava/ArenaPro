using ArenaPro.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArenaPro.Infra.Data.Configurations;
public class MatchResultConfiguration : IEntityTypeConfiguration<MatchResult>
{
    public virtual void Configure(EntityTypeBuilder<MatchResult> builder)
    {
        builder.HasKey(_ => new { _.TeamId, _.MatchId });
    }
}