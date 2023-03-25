using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PB_EGS_Escalavel.Core.Entities;

namespace PB_EGS_Escalavel.Infrastructure.Persistence.Configurations
{
    public class UserCourseConfigurations : IEntityTypeConfiguration<UserCourse>
    {
        public void Configure(EntityTypeBuilder<UserCourse> builder)
        {
            builder
                .HasKey(p => p.Id);
        }
    }    
}