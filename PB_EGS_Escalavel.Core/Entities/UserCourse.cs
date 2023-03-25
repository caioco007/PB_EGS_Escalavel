using PB_EGS_Escalavel.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PB_EGS_Escalavel.Core.Entities
{
    public class UserCourse : BaseEntity
    {
        public UserCourse(int idUser, int idCourse)
        {
            IdUser = idUser;
            IdCourse = idCourse;
        }

        public int IdUser { get; private set; }
        public int IdCourse { get; private set; }
    }
}
