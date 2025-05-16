using AcademIQ.Models;
using AcademIQ.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AcademIQ.Repositories
{
    public class EnrollmentRepository : Repository<Enrollments>, IEnrollmentRepository
    {
        public EnrollmentRepository(ClassroomContext context) : base(context) { }

        public async Task<IEnumerable<Enrollments>> GetEnrollmentsWithDetailsAsync()
        {
            return await _dbSet
                .Include(e => e.Student)
                .Include(e => e.Course)
                .ToListAsync();
        }

        public async Task<IEnumerable<Enrollments>> GetByStudentIdAsync(string studentId)
        {
            return await _dbSet
                .Where(e => e.StudentId == studentId)
                .Include(e => e.Course)
                .ToListAsync();
        }

        public async Task<IEnumerable<Enrollments>> GetByCourseIdAsync(string courseId)
        {
            return await _dbSet
                .Where(e => e.CourseId == courseId)
                .Include(e => e.Student)
                .ToListAsync();
        }
    }
}
