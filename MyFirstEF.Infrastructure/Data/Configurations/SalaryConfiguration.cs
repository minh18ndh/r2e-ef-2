using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyFirstEF.Domain.Entities;

namespace MyFirstEF.Infrastructure.Data.Configurations;

public class SalaryConfiguration : IEntityTypeConfiguration<Salary>
{
    public void Configure(EntityTypeBuilder<Salary> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Amount)
               .IsRequired()
               .HasColumnType("decimal(14,2)");
    }
}