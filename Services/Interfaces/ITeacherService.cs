// Services/Interfaces/ITeacherService.cs
using AcademIQ.Models;

namespace AcademIQ.Services.Interfaces
{
    public interface ITeacherService
    {
        Task<IEnumerable<Teachers>> GetAllAsync();
        Task<Teachers?> GetByIdAsync(string id);
        Task<IEnumerable<Teachers>> GetWithCoursesAsync();
        Task AddAsync(Teachers teacher);
        Task UpdateAsync(Teachers teacher);
        Task DeleteAsync(string id);
    }
}
