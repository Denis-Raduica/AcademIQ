using AcademIQ.Models;

namespace AcademIQ.Services.Interfaces
{
    public interface IAnnouncementService
    {
        Task<IEnumerable<Announcements>> GetAllAsync();
        Task<Announcements?> GetByIdAsync(string id);
        Task<IEnumerable<Announcements>> GetByCourseIdAsync(string courseId);
        Task<IEnumerable<Announcements>> GetByTeacherIdAsync(string teacherId);
        Task<IEnumerable<Announcements>> GetWithDetailsAsync();
        Task AddAsync(Announcements announcement);
        Task UpdateAsync(Announcements announcement);
        Task DeleteAsync(string id);
    }
}
