using AcademIQ.Models;

namespace AcademIQ.Services.Interfaces
{
    public interface IEnrollmentService
    {
        Task<IEnumerable<Enrollments>> GetAllAsync();
        Task<Enrollments?> GetByIdAsync(string id);
        Task<IEnumerable<Enrollments>> GetByStudentIdAsync(string studentId);
        Task<IEnumerable<Enrollments>> GetByCourseIdAsync(string courseId);
        Task<IEnumerable<Enrollments>> GetWithDetailsAsync();
        Task AddAsync(Enrollments enrollment);
        Task UpdateAsync(Enrollments enrollment);
        Task DeleteAsync(string id);
    }
}
