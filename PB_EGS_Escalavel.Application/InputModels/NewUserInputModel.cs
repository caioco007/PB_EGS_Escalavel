using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PB_EGS_Escalavel.Application.InputModels
{
    public class NewUserInputModel
    {
        public string FullName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string Role { get; set; }
    }
}
