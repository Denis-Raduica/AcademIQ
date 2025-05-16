using AcademIQ.Models;
using AcademIQ.Repositories.Interfaces;
using AcademIQ.Services.Interfaces;

namespace AcademIQ.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _repo;

        public CourseService(ICourseRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Courses>> GetAllAsync() => await _repo.GetAllAsync();

        public async Task<IEnumerable<Courses>> GetActiveAsync() => await _repo.GetActiveCoursesAsync();

        public async Task<IEnumerable<Courses>> GetWithTeachersAsync() => await _repo.GetCoursesWithTeacherAsync();

        public async Task<Courses?> GetByIdAsync(string id) => await _repo.GetByIdAsync(id);

        public async Task AddAsync(Courses course)
        {
            await _repo.AddAsync(course);
            await _repo.SaveChangesAsync();
        }

        public async Task UpdateAsync(Courses course)
        {
            _repo.Update(course);
            await _repo.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var course = await _repo.GetByIdAsync(id);
            if (course != null)
            {
                _repo.Delete(course);
                await _repo.SaveChangesAsync();
            }
        }
    }
}
