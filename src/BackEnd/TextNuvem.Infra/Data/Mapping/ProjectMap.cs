using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TextNuvem.Application.Services;
using TextNuvem.Domain.BackOffice.Entities;

namespace TextNuvem.Infra.Data.Mapping;

internal sealed class ProjectMap : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.ToTable("projects");

        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.LastUpdate)
            .HasColumnName("last_update_date")
            .HasColumnType("timestamptz")
            .IsRequired();

        builder.Property(x => x.Name)
            .HasMaxLength(160)
            .HasColumnName("name")
            .IsRequired();
    }
}