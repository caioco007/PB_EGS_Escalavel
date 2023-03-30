using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PB_EGS_Escalavel.Core.Entities;

namespace PB_EGS_Escalavel.Infrastructure.Persistence.Configurations
{
    public class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasKey(p => p.Id);

            builder
                .Property(p => p.BirthDate)
                .HasColumnType("datetime");

            builder
                .Property(p => p.CreatedAt)
                .HasColumnType("datetime")
                .HasDefaultValueSql("GETDATE()");

            builder
                .HasMany(u => u.StudentCourses)
                .WithOne()
                .HasForeignKey(p => p.IdCourse)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}