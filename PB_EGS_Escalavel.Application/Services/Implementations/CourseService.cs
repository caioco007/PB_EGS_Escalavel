using Microsoft.EntityFrameworkCore;
using PB_EGS_Escalavel.Application.InputModels;
using PB_EGS_Escalavel.Application.Services.Interfaces;
using PB_EGS_Escalavel.Application.ViewModels;
using PB_EGS_Escalavel.Core.Entities;
using PB_EGS_Escalavel.Core.Repositories;
using PB_EGS_Escalavel.Infraestructure.Persistence;
using PB_EGS_Escalavel.Infraestructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PB_EGS_Escalavel.Application.Services.Implementations
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<int> CreateAsync(NewCourseInputModel inputModel)
        {
            var course = new Course(inputModel.Title, inputModel.Description, inputModel.IdTeacher, inputModel.TotalHours);

            await _courseRepository.AddAsync(course);

            return course.Id;
        }

        public async Task AddStudentCourseAsync(NewStudentCourseInputModel inputModel)
        {
            var userCourse = new UserCourse(inputModel.IdUser, inputModel.IdCourse);

            await _courseRepository.AddStudentToCourseAsync(userCourse);
        }

        public async Task DeleteAsync(int id)
        {
            //var course = _dbContext.Courses.SingleOrDefault(c => c.Id == id);

            //_dbContext.Remove(course);
            //_dbContext.SaveChanges();
        }

        public async Task<List<CourseViewModel>> GetAllAsync(string query)
        {
            var courses = await _courseRepository.GetAllAsync();

            var coursesViewModel = courses.Select(p => new CourseViewModel(p.Id, p.Title, p.CreatedAt)).ToList();

            return coursesViewModel;
        }

        public async Task<CourseDetailsViewModel> GetByIdAsync(int id)
        {
            var course = await _courseRepository.GetDetailsByIdAsync(id);

            if (course == null) return null;

            var courseDetailsViewModel = new CourseDetailsViewModel(
                course.Id,
                course.Title,
                course.Description,
                course.TotalHours,
                course.Teacher.FullName
                );

            return courseDetailsViewModel;
        }

        public async Task UpdateAsync(UpdateCourseInputModel inputModel)
        {
            var course = await _courseRepository.GetByIdAsync(inputModel.Id);

            course.Update(inputModel.Title, inputModel.Description, inputModel.TotalHours);

            await _courseRepository.SaveChangesAsync();
        }
    }
}
