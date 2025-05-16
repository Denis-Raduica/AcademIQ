using AcademIQ.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AcademIQ.Services.Interfaces
{
    public interface IStudentsService
    {
        // Get all students
        Task<IEnumerable<Students>> GetAllAsync();

        // Get student by ID
        Task<Students?> GetByIdAsync(string id);

        // Get student by email
        Task<Students?> GetByEmailAsync(string email);

        // Get students by major
        Task<IEnumerable<Students>> GetByMajorAsync(string major);

        // Get students by year of study
        Task<IEnumerable<Students>> GetByYearAsync(int year);

        // Add a new student
        Task AddAsync(Students student);

        // Update an existing student
        Task UpdateAsync(Students student);

        // Delete a student
        Task DeleteAsync(Students student);
    }
}
