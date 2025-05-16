using AcademIQ.Models;
using AcademIQ.Repositories.Interfaces;
using AcademIQ.Services.Interfaces;

namespace AcademIQ.Services
{
    public class AnnouncementService : IAnnouncementService
    {
        private readonly IAnnouncementRepository _repo;

        public AnnouncementService(IAnnouncementRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Announcements>> GetAllAsync() => await _repo.GetAllAsync();

        public async Task<Announcements?> GetByIdAsync(string id) => await _repo.GetByIdAsync(id);

        public async Task<IEnumerable<Announcements>> GetByCourseIdAsync(string courseId) =>
            await _repo.GetByCourseIdAsync(courseId);

        public async Task<IEnumerable<Announcements>> GetByTeacherIdAsync(string teacherId) =>
            await _repo.GetByTeacherIdAsync(teacherId);

        public async Task<IEnumerable<Announcements>> GetWithDetailsAsync() =>
            await _repo.GetWithDetailsAsync();

        public async Task AddAsync(Announcements announcement)
        {
            await _repo.AddAsync(announcement);
            await _repo.SaveChangesAsync();
        }

        public async Task UpdateAsync(Announcements announcement)
        {
            _repo.Update(announcement);
            await _repo.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var announcement = await _repo.GetByIdAsync(id);
            if (announcement != null)
            {
                _repo.Delete(announcement);
                await _repo.SaveChangesAsync();
            }
        }
    }
}
