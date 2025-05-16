using AcademIQ.Models;
using AcademIQ.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AcademIQ.Repositories
{
    public class StudentsRepository : Repository<Students>, IStudentsRepository
    {
        public StudentsRepository(ClassroomContext context) : base(context) { }

        // Get students by major
        public async Task<IEnumerable<Students>> GetByMajorAsync(string major)
        {
            return await _dbSet.Where(s => s.Major == major).ToListAsync();
        }

        // Get students by year of study
        public async Task<IEnumerable<Students>> GetByYearAsync(int year)
        {
            return await _dbSet.Where(s => s.YearOfStudy == year).ToListAsync();
        }

        // Get a student by email
        public async Task<Students?> GetByEmailAsync(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(s => s.Email == email);
        }
    }
}
