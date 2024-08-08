using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication2.Data.Entities;

namespace WebApplication2.Data.Configurations;

internal class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        builder.Property(d => d.Name).IsRequired().HasMaxLength(20);
        builder.Property(d => d.Experience).IsRequired();
        builder.Property(d => d.DepartmentId).IsRequired();
        builder.HasOne(d => d.Department).WithMany(d => d.Doctors).HasForeignKey(d => d.DepartmentId);
    }
}
