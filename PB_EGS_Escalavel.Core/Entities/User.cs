using System.Data;

namespace PB_EGS_Escalavel.Core.Entities
{
    public class User : BaseEntity
    {
        public User(string fullName, string email, DateTime birthDate, string password, string role)
        {
            FullName = fullName;
            Email = email;
            BirthDate = birthDate;
            Active = true;
            Password = password;
            Role = role;

            StudentCourses = new List<UserCourse>();
            TeacherCourses = new List<Course>();
        }

        public string FullName { get; private set; }
        public string Email { get; private set; }
        public DateTime BirthDate { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public bool Active { get; set; }
        public string Password { get; private set; }
        public string Role { get; private set; }

        public List<UserCourse> StudentCourses { get; private set; }
        public List<Course> TeacherCourses { get; private set; }
    }
}
