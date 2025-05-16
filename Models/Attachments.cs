using System.ComponentModel.DataAnnotations;

namespace AcademIQ.Models
{
    public class Attachments
    {
        [Key]
        public string? AttachmentId { get; set; }

        public string FileName { get; set; }

        public string FileUrl { get; set; }

        public string? AnnouncementId { get; set; }

        public Announcements? Announcement { get; set; }
    }
}
