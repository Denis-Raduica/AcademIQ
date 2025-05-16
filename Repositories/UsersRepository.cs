using AcademIQ.Models;
using AcademIQ.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AcademIQ.Repositories
{
    public class UsersRepository : Repository<Users>, IUsersRepository
    {
        public UsersRepository(ClassroomContext context) : base(context) { }

        public async Task<Users?> GetByEmailAsync(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<IEnumerable<Users>> SearchByNameAsync(string name)
        {
            return await _dbSet
                .Where(u => u.Name.Contains(name))
                .ToListAsync();
        }
    }
}
