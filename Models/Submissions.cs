using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcademIQ.Models
{
    public class Submissions
    {
        [Key]
        public string? SubmissionID { get; set; }

        public DateTime SubmissionDate { get; set; }

        public string Grade { get; set; }

        [ForeignKey("Student")]
        public string? StudentId { get; set; }

        public Students? Student { get; set; }



        [ForeignKey("Assignment")]
        public string? AssignmentID { get; set; }
        public Assignments? Assignment { get; set; }

        public ICollection<Attachments>? Attachments { get; set; }
    }
}
