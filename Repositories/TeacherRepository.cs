using AcademIQ.Models;
using AcademIQ.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AcademIQ.Repositories
{
    public class TeacherRepository : Repository<Teachers>, ITeacherRepository
    {
        public TeacherRepository(ClassroomContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Teachers>> GetTeachersWithCoursesAsync()
        {
            return await _dbSet
                .Include(t => t.Courses)
                .ToListAsync();
        }
    }
}
