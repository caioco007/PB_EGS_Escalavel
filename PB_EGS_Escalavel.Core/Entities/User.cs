namespace PB_EGS_Escalavel.Core.Entities
{
    public class User : BaseEntity
    {
        public User(string fullName, string email, DateTime birthDate)
        {
            FullName = fullName;
            Email = email;
            BirthDate = birthDate;
            Active = true;

            CreatedAt = DateTime.Now;
            TeacherCourses = new List<Course>();
            StudentCourses = new List<Course>();
        }

        public string FullName { get; private set; }
        public string Email { get; private set; }
        public DateTime BirthDate { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public bool Active { get; set; }

        public List<Course> TeacherCourses { get; private set; }
        public List<Course> StudentCourses { get; private set; }
    }
}
