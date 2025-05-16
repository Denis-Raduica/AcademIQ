using AcademIQ.Models;

namespace AcademIQ.Repositories.Interfaces
{
    public interface ITeacherRepository : IRepository<Teachers>
    {
        // You can add teacher-specific methods here in the future
        Task<IEnumerable<Teachers>> GetTeachersWithCoursesAsync();
    }
}
