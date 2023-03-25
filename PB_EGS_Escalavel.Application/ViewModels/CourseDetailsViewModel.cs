using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PB_EGS_Escalavel.Application.ViewModels
{
    public class CourseDetailsViewModel
    {
        public CourseDetailsViewModel(int id, string title, string description, decimal totalHours, string teacherFullName)
        {
            Id = id;
            Title = title;
            Description = description;
            TotalHours = totalHours;
            TeacherFullName = teacherFullName;
        }

        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public decimal TotalHours { get; private set; }
        public string TeacherFullName { get; private set; }
    }
}
