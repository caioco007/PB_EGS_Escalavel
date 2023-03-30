using PB_EGS_Escalavel.Core.Entities;

namespace PB_EGS_Escalavel.Core.Entities
{
    public class Course : BaseEntity
    {
        public Course(string title, string description, int idTeacher, decimal totalHours)
        {
            Title = title;
            Description = description;
            IdTeacher = idTeacher;
            TotalHours = totalHours;

            CreatedAt = DateTime.Now;
        }

        public string Title { get; private set; }
        public string Description { get; private set; }
        public int IdTeacher { get; private set; }
        public User Teacher { get; private set; }
        public decimal TotalHours { get; private set; }
        public DateTime CreatedAt { get; private set; }


        public void Update(string title, string description, decimal totalHours)
        {
            Title = title;
            Description = description;
            TotalHours = totalHours;
        }
    }
}
