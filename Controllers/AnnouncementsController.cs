using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using AcademIQ.Models;
using AcademIQ.Services.Interfaces;
using NuGet.DependencyResolver;

namespace AcademIQ.Controllers
{
    public class AnnouncementsController : Controller
    {
        private readonly IAnnouncementService _announcementService;
        private readonly ICourseService _courseService;     // To populate dropdowns
        private readonly ITeacherService _teacherService;   // To populate dropdowns

        public AnnouncementsController(
            IAnnouncementService announcementService,
            ICourseService courseService,
            ITeacherService teacherService)
        {
            _announcementService = announcementService;
            _courseService = courseService;
            _teacherService = teacherService;
        }

        // GET: Announcements
        public async Task<IActionResult> Index()
        {
            var announcements = await _announcementService.GetWithDetailsAsync();
            return View(announcements);
        }

        // GET: Announcements/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var announcement = await _announcementService.GetByIdAsync(id);
            if (announcement == null)
                return NotFound();

            return View(announcement);
        }

        // GET: Announcements/Create
        public async Task<IActionResult> Create()
        {
            await PopulateDropdowns();
            return View();
        }

        // POST: Announcements/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AnnouncementId,Title,Description,CreatedDate,IsActive,CourseId,TeacherId")] Announcements announcement)
        {
            if (ModelState.IsValid)
            {
                // If your IDs are generated here
                announcement.AnnouncementId = Guid.NewGuid().ToString();
                announcement.CreatedDate = DateTime.UtcNow;

                await _announcementService.AddAsync(announcement);
                return RedirectToAction(nameof(Index));
            }
            await PopulateDropdowns(announcement.CourseId, announcement.TeacherId);
            return View(announcement);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAnnouncementFromCoursePage([FromBody] Announcements announcement)
        {
            if (ModelState.IsValid)
            {
                announcement.AnnouncementId = Guid.NewGuid().ToString(); // Auto-generate the ID here
                announcement.CreatedDate = DateTime.UtcNow;
                announcement.IsActive = true; // Assuming you want it active immediately
                await _announcementService.AddAsync(announcement);
                return Ok(new { message = "Announcement created successfully" });
            }

            return BadRequest(ModelState);
        }

        // GET: Announcements/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var announcement = await _announcementService.GetByIdAsync(id);
            if (announcement == null)
                return NotFound();

            await PopulateDropdowns(announcement.CourseId, announcement.TeacherId);
            return View(announcement);
        }

        // POST: Announcements/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("AnnouncementId,Title,Description,CreatedDate,IsActive,CourseId,TeacherId")] Announcements announcement)
        {
            if (id != announcement.AnnouncementId)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _announcementService.UpdateAsync(announcement);
                }
                catch (Exception)
                {
                    if (!await AnnouncementExists(announcement.AnnouncementId))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            await PopulateDropdowns(announcement.CourseId, announcement.TeacherId);
            return View(announcement);
        }

        // GET: Announcements/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var announcement = await _announcementService.GetByIdAsync(id);
            if (announcement == null)
                return NotFound();

            return View(announcement);
        }

        // POST: Announcements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _announcementService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> AnnouncementExists(string id)
        {
            var announcement = await _announcementService.GetByIdAsync(id);
            return announcement != null;
        }

        private async Task PopulateDropdowns(string? selectedCourseId = null, string? selectedTeacherId = null)
        {
            var courses = await _courseService.GetAllAsync();
            var teachers = await _teacherService.GetAllAsync();

            ViewData["CourseId"] = new SelectList(courses, "CourseId", "CourseName", selectedCourseId);
            ViewData["TeacherId"] = new SelectList(teachers, "Id", "FullName", selectedTeacherId);
        }
    }
}
