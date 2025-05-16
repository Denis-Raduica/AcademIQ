using AcademIQ.Models;
using AcademIQ.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AcademIQ.Repositories
{
    public class AnnouncementRepository : Repository<Announcements>, IAnnouncementRepository
    {
        public AnnouncementRepository(ClassroomContext context) : base(context) { }

        public async Task<IEnumerable<Announcements>> GetByCourseIdAsync(string courseId)
        {
            return await _dbSet
                .Where(a => a.CourseId == courseId)
                .Include(a => a.Course)
                .Include(a => a.Teacher)
                .Include(a => a.Attachments)
                .ToListAsync();
        }

        public async Task<IEnumerable<Announcements>> GetByTeacherIdAsync(string teacherId)
        {
            return await _dbSet
                .Where(a => a.TeacherId == teacherId)
                .Include(a => a.Course)
                .Include(a => a.Attachments)
                .ToListAsync();
        }

        public async Task<IEnumerable<Announcements>> GetWithDetailsAsync()
        {
            return await _dbSet
                .Include(a => a.Course)
                .Include(a => a.Teacher)
                .Include(a => a.Attachments)
                .ToListAsync();
        }
    }
}
