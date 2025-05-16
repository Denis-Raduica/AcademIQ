using System.ComponentModel.DataAnnotations;

namespace AcademIQ.Models
{
    public class Courses
    {
        [Key]
        public string? CourseId { get; set; }

        public string? CourseName { get; set; }

        public string? Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsActive { get; set; }

        public string? Color { get; set; }

        public string? CourseCode { get; set; }

        public string? BackgroundImageUrl { get; set; }

        // Navigation properties for related entities
        public string? TeacherId { get; set; }
        public Teachers? Teacher { get; set; }

        public ICollection<Announcements>? Announcements { get; set; }

        public ICollection<Submissions>? Submissions { get; set; }

        public ICollection<Enrollments>? Enrollments { get; set; }
    }
}

