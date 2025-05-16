using AcademIQ.Models;
using AcademIQ.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AcademIQ.Repositories
{
    public class CourseRepository : Repository<Courses>, ICourseRepository
    {
        public CourseRepository(ClassroomContext context) : base(context) { }

        public async Task<IEnumerable<Courses>> GetCoursesWithTeacherAsync()
        {
            return await _dbSet
                .Include(c => c.Teacher)
                .ToListAsync();
        }

        public async Task<IEnumerable<Courses>> GetActiveCoursesAsync()
        {
            return await _dbSet
                .Where(c => c.IsActive)
                .ToListAsync();
        }
    }
}
