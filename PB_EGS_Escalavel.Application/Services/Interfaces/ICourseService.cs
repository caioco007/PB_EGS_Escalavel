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
        Task<List<CourseViewModel>> GetAll(string query);
        Task<CourseDetailsViewModel> GetById(int id);
        Task<int> Create(NewCourseInputModel inputModel);
        Task Update(UpdateCourseInputModel inputModel);
        Task Delete(int id);
        Task AddStudentCourse(NewStudentCourseInputModel inputModel);
        //void CreateComment(CreateCommentInputModel inputModel);
    }
}
