using AcademIQ.Models;

namespace AcademIQ.Services.Interfaces
{
    public interface ICourseService
    {
        Task<IEnumerable<Courses>> GetAllAsync();
        Task<IEnumerable<Courses>> GetActiveAsync();
        Task<IEnumerable<Courses>> GetWithTeachersAsync();
        Task<Courses?> GetByIdAsync(string id);
        Task AddAsync(Courses course);
        Task UpdateAsync(Courses course);
        Task DeleteAsync(string id);
        //Task<IEnumerable<Courses>> GetAllCoursesAsync(); // This is the method we'll implement.
    }
}
