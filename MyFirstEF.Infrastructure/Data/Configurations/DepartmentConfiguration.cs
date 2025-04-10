using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyFirstEF.Domain.Entities;

namespace MyFirstEF.Infrastructure.Data.Configurations;

public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.HasKey(d => d.Id);

        builder.Property(d => d.Name)
               .IsRequired()
               .HasMaxLength(100);

        builder.HasMany(d => d.Employees)
               .WithOne(e => e.Department)
               .HasForeignKey(e => e.DepartmentId);

        // Seed data
        builder.HasData(
            new Department { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), Name = "Software Development" },
            new Department { Id = Guid.Parse("22222222-2222-2222-2222-222222222222"), Name = "Finance" },
            new Department { Id = Guid.Parse("33333333-3333-3333-3333-333333333333"), Name = "Accountant" },
            new Department { Id = Guid.Parse("44444444-4444-4444-4444-444444444444"), Name = "HR" }
        );
    }
}