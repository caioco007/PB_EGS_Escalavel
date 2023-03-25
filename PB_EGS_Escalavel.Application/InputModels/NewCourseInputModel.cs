using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PB_EGS_Escalavel.Application.InputModels
{
    public class NewCourseInputModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int IdTeacher { get; set; }
        public Decimal TotalHours { get; set; }
    }
}
