using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PB_EGS_Escalavel.Core.Entities;

namespace PB_EGS_Escalavel.Infrastructure.Persistence.Configurations
{
    public class CourseConfigurations : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder
                .HasKey(p => p.Id);

            builder
                .Property(p => p.TotalHours)
                .HasColumnType("decimal");

            builder
                .HasOne(p => p.Teacher)
                .WithMany(f => f.TeacherCourses)
                .HasForeignKey(p => p.IdTeacher)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
