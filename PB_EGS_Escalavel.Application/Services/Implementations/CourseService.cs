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

        public async Task<bool> AddStudentCourseAsync(NewStudentCourseInputModel inputModel)
        {
            var userCourse = new UserCourse(inputModel.IdUser, inputModel.IdCourse);

            await _courseRepository.AddStudentToCourseAsync(userCourse);
            return true;
        }

        public async Task<bool> DeleteAsync(int id, int userId)
        {
            var courses = await _courseRepository.GetAllAsync();
            if (!courses.Any(x => x.Id == id && x.IdTeacher == userId)) return false;

            if (await _courseRepository.CountStudentByCourseAsync(id) > 0) return false;

            await _courseRepository.DeleteAsync(id);
            return true;
        }

        public async Task<List<CourseViewModel>> GetAllAsync()
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

        public async Task<bool> UpdateAsync(UpdateCourseInputModel inputModel, int userId)
        {
            var courses = await _courseRepository.GetAllAsync();
            if (!courses.Any(x => x.Id == inputModel.Id && x.IdTeacher == userId)) return false;

            if (await _courseRepository.CountStudentByCourseAsync(inputModel.Id) == 0) return false;

            var course = await _courseRepository.GetByIdAsync(inputModel.Id);
            if (course == null) return false;

            course.Update(inputModel.Title, inputModel.Description, inputModel.TotalHours);

            await _courseRepository.SaveChangesAsync();
            return true;
        }
    }
}
