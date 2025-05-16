using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mail;

namespace AcademIQ.Models
{
    public class Announcements
    {
        [Key]
        public string? AnnouncementId { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }

        public bool IsActive { get; set; }


        [ForeignKey("Course")]
        public string? CourseId { get; set; }
        public Courses? Course { get; set; }

        [ForeignKey("Teacher")]
        public string? TeacherId { get; set; }
        public Teachers? Teacher { get; set; }

        // List of attached files
        public ICollection<Attachments>? Attachments { get; set; }
    }
}
