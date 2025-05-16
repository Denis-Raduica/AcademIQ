// Services/TeacherService.cs
using AcademIQ.Models;
using AcademIQ.Repositories.Interfaces;
using AcademIQ.Services.Interfaces;

namespace AcademIQ.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository _teacherRepo;

        public TeacherService(ITeacherRepository teacherRepo)
        {
            _teacherRepo = teacherRepo;
        }

        public async Task<IEnumerable<Teachers>> GetAllAsync() => await _teacherRepo.GetAllAsync();

        public async Task<Teachers?> GetByIdAsync(string id) => await _teacherRepo.GetByIdAsync(id);

        public async Task<IEnumerable<Teachers>> GetWithCoursesAsync() => await _teacherRepo.GetTeachersWithCoursesAsync();

        public async Task AddAsync(Teachers teacher)
        {
            await _teacherRepo.AddAsync(teacher);
            await _teacherRepo.SaveChangesAsync();
        }

        public async Task UpdateAsync(Teachers teacher)
        {
            _teacherRepo.Update(teacher);
            await _teacherRepo.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var teacher = await _teacherRepo.GetByIdAsync(id);
            if (teacher != null)
            {
                _teacherRepo.Delete(teacher);
                await _teacherRepo.SaveChangesAsync();
            }
        }
    }
}
