using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcademIQ.Models
{
    public class Enrollments
    {
        [Key]
        public string? EnrollmentId { get; set; }

        [ForeignKey("Student")]
        public string StudentId { get; set; }

        public Students Student { get; set; }

        [ForeignKey("Course")]
        public string CourseId { get; set; }

        public Courses Course { get; set; }

        public DateTime? EnrollmentDate { get; set; }
    }
}
