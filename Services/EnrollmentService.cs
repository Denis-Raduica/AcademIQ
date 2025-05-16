using AcademIQ.Models;
using AcademIQ.Repositories.Interfaces;
using AcademIQ.Services.Interfaces;

namespace AcademIQ.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IEnrollmentRepository _repo;

        public EnrollmentService(IEnrollmentRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Enrollments>> GetAllAsync() => await _repo.GetAllAsync();

        public async Task<Enrollments?> GetByIdAsync(string id) => await _repo.GetByIdAsync(id);

        public async Task<IEnumerable<Enrollments>> GetByStudentIdAsync(string studentId) =>
            await _repo.GetByStudentIdAsync(studentId);

        public async Task<IEnumerable<Enrollments>> GetByCourseIdAsync(string courseId) =>
            await _repo.GetByCourseIdAsync(courseId);

        public async Task<IEnumerable<Enrollments>> GetWithDetailsAsync() =>
            await _repo.GetEnrollmentsWithDetailsAsync();

        public async Task AddAsync(Enrollments enrollment)
        {
            await _repo.AddAsync(enrollment);
            await _repo.SaveChangesAsync();
        }

        public async Task UpdateAsync(Enrollments enrollment)
        {
            _repo.Update(enrollment);
            await _repo.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var enrollment = await _repo.GetByIdAsync(id);
            if (enrollment != null)
            {
                _repo.Delete(enrollment);
                await _repo.SaveChangesAsync();
            }
        }
    }
}
