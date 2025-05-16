using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcademIQ.Models
{
    public class Students : Users
    {
       

        public double? GPA { get; set; }
        public int? YearOfStudy { get; set; }
        public string? Major { get; set; }

        

        public ICollection<Enrollments>? Enrollments { get; set; }

    }
}
