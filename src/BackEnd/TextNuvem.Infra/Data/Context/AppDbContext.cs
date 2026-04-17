using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using TextNuvem.Application.Services;
using TextNuvem.Domain.BackOffice.Entities;
using TextNuvem.Infra.Dtos.Folders;

namespace TextNuvem.Infra.Data.Context;

public sealed class AppDbContext(DbContextOptions<AppDbContext>options) : DbContext(options)
{
    public DbSet<Project>Projects { get; set; }
    public DbSet<Customer>Customers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var compactor = this.GetService<ICompactorService>();

        modelBuilder.Entity<Project>()
            .Property(x => x.Folders)
            .HasConversion<string>(
                x => compactor.CompressObject(x),
                x => (string.IsNullOrWhiteSpace(x)
                    ? new List<Folder>()
                    : compactor.DecompressObject<List<FolderDatabaseDto>>(x)
                        .Select(x=>(Folder)x).ToList())
            ).HasColumnType("TEXT");
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(Console.WriteLine);
    }
} 