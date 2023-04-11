using Microsoft.EntityFrameworkCore;
using PB_EGS_Escalavel.Core.Entities;
using PB_EGS_Escalavel.Core.Repositories;

namespace PB_EGS_Escalavel.Infraestructure.Persistence.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CourseRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Course course)
        {
            await _dbContext.Courses.AddAsync(course);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddStudentToCourseAsync(UserCourse userCourse)
        {
            await _dbContext.UserCourses.AddAsync(userCourse);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<int> CountStudentByCourseAsync(int courseId)
        {
            return await _dbContext.UserCourses.Where(x => x.IdCourse == courseId).CountAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var course = _dbContext.Courses.SingleOrDefault(c => c.Id == id);
            course.Inactive();

            _dbContext.SaveChanges();
        }

        public async Task<List<Course>> GetAllAsync() => await _dbContext.Courses.Where(x => x.Active).ToListAsync();

        public async Task<Course> GetByIdAsync(int id) => await _dbContext.Courses.SingleOrDefaultAsync(p => p.Id == id && p.Active);

        public async Task<Course> GetDetailsByIdAsync(int id)
        {
            return await _dbContext.Courses
                .Include(p => p.Teacher)
                .SingleOrDefaultAsync(p => p.Id == id && p.Active);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
