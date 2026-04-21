using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TextNuvem.Domain.BackOffice.Entities;

namespace TextNuvem.Infra.Data.Mapping;

internal sealed class CustomerMap:IEntityTypeConfiguration<Customer>
{ 
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customer");

        builder.HasKey(x => x.Id);
        
        builder.OwnsOne(x => x.Email, x =>
        {
            x.Property(x => x.Address)
                .HasColumnName("Email")
                .HasColumnType("VARCHAR")
                .HasMaxLength(255)
                .IsRequired();
            x.HasIndex(x => x.Address, "IX_Customer_Email")
                .IsUnique();
        });
        
        builder.OwnsOne(x => x.Password, x =>
        {
            x.Property(x => x.HashPassword)
                .HasColumnName("HashPassword")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(300)
                .IsRequired();
        });
        
        builder.OwnsOne(x => x.RefreshToken, x =>
        {
            x.Property(x => x.Token)
                .HasColumnName("RefreshToken")
                .HasColumnType("TEXT")
                .IsRequired(false);
            x.Property(x => x.Expired)
                .HasColumnName("ExpiredRefreshToken")
                .HasColumnType("DATETIME2")
                .IsRequired(false);
        });
        
        builder.Property(x=>x.Name)
            .HasColumnName("Name")
            .HasColumnType("VARCHAR")
            .HasMaxLength(120)
            .IsRequired();

        builder.HasMany(x => x.Projects)
            .WithOne(x => x.Customer)
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}