using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyFirstEF.Domain.Entities;

namespace MyFirstEF.Infrastructure.Data.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(e => e.JoinedDate)
               .IsRequired();

        builder.HasOne(e => e.Salary)
               .WithOne(s => s.Employee)
               .HasForeignKey<Salary>(s => s.EmployeeId);
    }
}