using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyFirstEF.Domain.Entities;

namespace MyFirstEF.Infrastructure.Data.Configurations;

public class ProjectEmployeeConfiguration : IEntityTypeConfiguration<ProjectEmployee>
{
    public void Configure(EntityTypeBuilder<ProjectEmployee> builder)
    {
        builder.HasKey(pe => new { pe.ProjectId, pe.EmployeeId });

        builder.Property(pe => pe.Enable)
               .IsRequired();

        builder.HasOne(pe => pe.Project)
               .WithMany(p => p.ProjectEmployees)
               .HasForeignKey(pe => pe.ProjectId);

        builder.HasOne(pe => pe.Employee)
               .WithMany(e => e.ProjectEmployees)
               .HasForeignKey(pe => pe.EmployeeId);
    }
}