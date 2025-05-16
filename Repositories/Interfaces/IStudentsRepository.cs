using AcademIQ.Models;

namespace AcademIQ.Repositories.Interfaces
{
    public interface IStudentsRepository : IRepository<Students>
    {
        Task<IEnumerable<Students>> GetByMajorAsync(string major);
        Task<IEnumerable<Students>> GetByYearAsync(int year);
        Task<Students?> GetByEmailAsync(string email);
    }
}
