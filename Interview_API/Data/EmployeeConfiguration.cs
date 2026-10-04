using Interview_API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Interview_API.Data
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e=>e.EmployeeId);
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(e => e.Email)
               .IsRequired()
               .HasMaxLength(100);
            builder.Property(e => e.Department)
               .IsRequired();
            builder.Property(e => e.Salary)
               .IsRequired();

               
        }
    }
}
