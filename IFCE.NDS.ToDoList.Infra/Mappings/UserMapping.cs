using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NDS_ToDo.Domain.Entities;

namespace NDS_ToDo.Infra.Mappings;

public class UserMapping : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .HasKey(c => c.Id);
        builder
            .Property(c => c.Name)
            .IsRequired()
            .HasColumnType("varchar(150)");
        builder
            .Property(c => c.Email)
            .IsRequired()
            .HasColumnType("varchar(100)");
        builder
            .Property(c => c.Password)
            .IsRequired()
            .HasColumnType("varchar(255)");
        builder
            .Property(c => c.CreatedAt)
            .ValueGeneratedOnAdd()
            .HasColumnType("datetime");
        builder
            .Property(c => c.UpdatedAt)
            .ValueGeneratedOnAddOrUpdate()
            .HasColumnType("datetime");
        builder
            .HasMany(c => c.Assignments)
            .WithOne(c => c.User)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}