using AcademIQ.Models;

namespace AcademIQ.Repositories.Interfaces
{
    public interface ICourseRepository : IRepository<Courses>
    {
        Task<IEnumerable<Courses>> GetCoursesWithTeacherAsync();
        Task<IEnumerable<Courses>> GetActiveCoursesAsync();
    }
}
