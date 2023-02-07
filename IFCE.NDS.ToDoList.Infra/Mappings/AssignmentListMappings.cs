using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NDS_ToDo.Domain.Entities;

namespace NDS_ToDo.Infra.Mappings;

public class AssignmentListMapping : IEntityTypeConfiguration<AssignmentList>
{
    public void Configure(EntityTypeBuilder<AssignmentList> builder)
    {
        builder
            .HasKey(c => c.Id);

        builder
            .Property(c => c.Name)
            .IsRequired();
        
        builder.
            Property(c => c.Description)
            .IsRequired()
            .HasColumnType("VARCHAR(255)");

        builder
            .Property(c => c.UserId)
            .IsRequired();

        builder
            .HasMany(c => c.Assignments)
            .WithOne(c => c.AssignmentList)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
