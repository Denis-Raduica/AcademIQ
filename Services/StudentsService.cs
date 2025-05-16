using AcademIQ.Models;
using AcademIQ.Repositories.Interfaces;
using AcademIQ.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AcademIQ.Services
{
    public class StudentsService : IStudentsService
    {
        private readonly IStudentsRepository _studentsRepo;

        public StudentsService(IStudentsRepository studentsRepo)
        {
            _studentsRepo = studentsRepo;
        }

        // Get all students
        public async Task<IEnumerable<Students>> GetAllAsync()
        {
            return await _studentsRepo.GetAllAsync();
        }

        // Get student by ID
        public async Task<Students?> GetByIdAsync(string id)
        {
            return await _studentsRepo.GetByIdAsync(id);
        }

        // Get student by email
        public async Task<Students?> GetByEmailAsync(string email)
        {
            return await _studentsRepo.GetByEmailAsync(email);
        }

        // Get students by major
        public async Task<IEnumerable<Students>> GetByMajorAsync(string major)
        {
            return await _studentsRepo.GetByMajorAsync(major);
        }

        // Get students by year of study
        public async Task<IEnumerable<Students>> GetByYearAsync(int year)
        {
            return await _studentsRepo.GetByYearAsync(year);
        }

        // Add a student
        public async Task AddAsync(Students student)
        {
            await _studentsRepo.AddAsync(student);
            await _studentsRepo.SaveChangesAsync();
        }

        // Update a student
        public async Task UpdateAsync(Students student)
        {
            _studentsRepo.Update(student);
            await _studentsRepo.SaveChangesAsync();
        }

        // Delete a student
        public async Task DeleteAsync(Students student)
        {
            _studentsRepo.Delete(student);
            await _studentsRepo.SaveChangesAsync();
        }
    }
}
