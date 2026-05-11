 using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TextNuvem.Domain.BackOffice.Entities;
using TextNuvem.Domain.BackOffice.ValueObject;

namespace TextNuvem.Infra.Data.Mapping;

internal sealed class CustomerMap:IEntityTypeConfiguration<Customer>
{ 
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("customers");

        builder.HasKey(x => x.Id);
        
        builder.OwnsOne(x => x.Email, x =>
        {
            x.Property(x => x.Address)
                .HasColumnName("email")
                .HasMaxLength(255)
                .IsRequired();
            
            x.HasIndex(x => x.Address, "ix_customer_email")
                .IsUnique();
        });
        
        builder.OwnsOne(x => x.Password, x =>
        {
            x.Property(x => x.HashPassword)
                .HasColumnName("hash_password")
                .HasMaxLength(300)
                .IsRequired();
        });
        
        builder.OwnsOne(x => x.RefreshToken, x =>
        {
            x.Property(x => x.Token)
                .HasColumnName("refresh_token")
                .HasColumnType("TEXT")
                .IsRequired(false);
            
            x.Property(x => x.Expired)
                .HasColumnName("expired_refresh_token")
                .HasColumnType("timestamptz")
                .IsRequired(false);
        });
        
        builder.Property(x=>x.Name)
            .HasColumnName("name")
            .HasMaxLength(120)
            .IsRequired();

        builder.HasMany(x => x.Projects)
            .WithOne(x => x.Customer)
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasOne(x => x.LastProjectUpdate)
            .WithOne()
            .HasForeignKey<Customer>(x=>x.LastProjectIdUpdate)
            .HasConstraintName("fk_customer_last_project_update_id")
            .IsRequired(false);

        builder.Property(x => x.ChangesDate)
            .HasColumnType("jsonb"); 
        // .HasConversion<string>(x =>
        //         JsonSerializer.Serialize(x), x =>
        //         string.IsNullOrWhiteSpace(x)
        //             ? new List<ChangesDate>()
        //             : JsonSerializer.Deserialize<List<ChangesDate>>(x) ?? new()
        // );
    }
}