using AcademIQ.Models;

namespace AcademIQ.Repositories.Interfaces
{
    public interface IAnnouncementRepository : IRepository<Announcements>
    {
        Task<IEnumerable<Announcements>> GetByCourseIdAsync(string courseId);
        Task<IEnumerable<Announcements>> GetByTeacherIdAsync(string teacherId);
        Task<IEnumerable<Announcements>> GetWithDetailsAsync();
    }
}
