using PB_EGS_Escalavel.Application.InputModels;
using PB_EGS_Escalavel.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PB_EGS_Escalavel.Application.Services.Interfaces
{
    public interface ICourseService
    {
        Task<List<CourseViewModel>> GetAllAsync(string query);
        Task<CourseDetailsViewModel> GetByIdAsync(int id);
        Task<int> CreateAsync(NewCourseInputModel inputModel);
        Task UpdateAsync(UpdateCourseInputModel inputModel);
        Task DeleteAsync(int id);
        Task AddStudentCourseAsync(NewStudentCourseInputModel inputModel);
    }
}
