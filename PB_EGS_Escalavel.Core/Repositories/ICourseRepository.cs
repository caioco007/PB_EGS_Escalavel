using PB_EGS_Escalavel.Core.Entities;

namespace PB_EGS_Escalavel.Core.Repositories
{
    public interface ICourseRepository
    {
        Task<List<Course>> GetAllAsync();
        Task<Course> GetDetailsByIdAsync(int id);
        Task<Course> GetByIdAsync(int id);
        Task AddAsync(Course course);
        Task AddStudentToCourseAsync(UserCourse userCourse);
        Task SaveChangesAsync();
    }
}
