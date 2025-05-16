using AcademIQ.Models;

namespace AcademIQ.Repositories.Interfaces
{
    public interface IUsersRepository : IRepository<Users>
    {
        Task<Users?> GetByEmailAsync(string email);
        Task<IEnumerable<Users>> SearchByNameAsync(string name);
    }
}
