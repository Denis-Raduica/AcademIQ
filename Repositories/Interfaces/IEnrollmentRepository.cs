using AcademIQ.Models;

namespace AcademIQ.Repositories.Interfaces
{
    public interface IEnrollmentRepository : IRepository<Enrollments>
    {
        Task<IEnumerable<Enrollments>> GetEnrollmentsWithDetailsAsync();
        Task<IEnumerable<Enrollments>> GetByStudentIdAsync(string studentId);
        Task<IEnumerable<Enrollments>> GetByCourseIdAsync(string courseId);
    }
}
