using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TextNuvem.Application.Services;
using TextNuvem.Domain.BackOffice.Entities;

namespace TextNuvem.Infra.Data.Mapping;

internal sealed class ProjectMap : IEntityTypeConfiguration<Project>
{
    
    private readonly ICompactorService _compactorService;

    public ProjectMap(ICompactorService compactorService)
    {
        _compactorService = compactorService;
    }
    
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.ToTable("Project");

        builder.HasKey(x => x.Id);
        

        builder.Property(x => x.LastUpdate)
            .HasColumnName("LastUpdateDate")
            .HasColumnType("DATETIME2")
            .IsRequired();

        builder.Property(x => x.Name)
            .HasMaxLength(160)
            .HasColumnType("VARCHAR")
            .HasColumnName("Name")
            .IsRequired();
    }
}