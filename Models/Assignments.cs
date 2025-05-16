using System.ComponentModel.DataAnnotations;

namespace AcademIQ.Models
{
    public class Assignments : Announcements
    {

        public DateTime DueDate { get; set; }

        public ICollection<Submissions> Submissions { get; set; }
    }
}
