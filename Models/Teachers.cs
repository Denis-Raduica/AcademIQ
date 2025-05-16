using System.ComponentModel.DataAnnotations;

namespace AcademIQ.Models
{
    public class Teachers : Users
    {
       

        public int? Hours { get; set; }
        public string? Specialization { get; set; }

        public ICollection<Courses>? Courses { get; set; }



    }
}
