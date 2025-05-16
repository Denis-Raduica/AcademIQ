using AcademIQ.Models;
using AcademIQ.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcademIQ.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly ITeacherService _teacherService;
        private readonly IAnnouncementService _announcementService;
        private readonly IEnrollmentService _enrollmentService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CoursesController(ICourseService courseService, ITeacherService teacherService,
                                 IAnnouncementService announcementService, IEnrollmentService enrollmentService, IWebHostEnvironment webHostEnvironment)
        {
            _courseService = courseService;
            _teacherService = teacherService;
            _announcementService = announcementService;
            _enrollmentService = enrollmentService;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> CoursePage(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var course = await _courseService.GetByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            var announcements = await _announcementService.GetByCourseIdAsync(id);
            course.Announcements = announcements.ToList();

            var enrollments = await _enrollmentService.GetByCourseIdAsync(id);
            var enrolledStudents = enrollments.Select(e => e.Student).ToList();
            ViewData["EnrolledStudents"] = enrolledStudents;

            // Assuming you have a teacher service to get all teachers:
            var teachers = await _teacherService.GetAllAsync();
            ViewData["Teachers"] = teachers.ToList();

            return View(course);
        }




        // GET: Courses
        public async Task<IActionResult> Index()
        {
            var courses = await _courseService.GetAllAsync();
            return View(courses);
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var course = await _courseService.GetByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            // Get announcements for the course
            var announcements = await _announcementService.GetByCourseIdAsync(id);
            course.Announcements = announcements.ToList();

            // Get enrolled students
            var enrollments = await _enrollmentService.GetByCourseIdAsync(id);
            var enrolledStudents = enrollments.Select(e => e.Student).ToList();
            ViewData["EnrolledStudents"] = enrolledStudents;

            return View(course);
        }

        // GET: Courses/Create
        public async Task<IActionResult> Create()
        {
            var teachers = await _teacherService.GetAllAsync();
            ViewData["TeacherId"] = new SelectList(teachers, "Id", "Name");
            return View();
        }

        // POST: Courses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Courses course, IFormFile? backgroundImage)
        {
            if (ModelState.IsValid)
            {
                if (backgroundImage != null && backgroundImage.Length > 0)
                {
                    var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Assets", "Art");
                    var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(backgroundImage.FileName);
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using var stream = new FileStream(filePath, FileMode.Create);
                    await backgroundImage.CopyToAsync(stream);

                    course.BackgroundImageUrl = Path.Combine("Assets", "Art", uniqueFileName).Replace("\\", "/");
                }
                // Generate random color for the course
                course.CourseId = Guid.NewGuid().ToString(); // Auto-generate the ID here
                course.Color = GetRandomColor();
                await _courseService.AddAsync(course);
                return RedirectToAction(nameof(Index));
            }

            var teachers = await _teacherService.GetAllAsync();
            ViewData["TeacherId"] = new SelectList(teachers, "Id", "Name", course.TeacherId);
            return View(course);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var course = await _courseService.GetByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            var teachers = await _teacherService.GetAllAsync();
            ViewData["TeacherId"] = new SelectList(teachers, "Id", "Name", course.TeacherId);
            return View(course);
        }

        // POST: Courses/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CourseId,CourseName,Description,StartDate,EndDate,IsActive,Color,CourseCode,BackgroundImageUrl,TeacherId")] Courses course)
        {
            if (id != course.CourseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _courseService.UpdateAsync(course);
                }
                catch (Exception)
                {
                    if (await _courseService.GetByIdAsync(course.CourseId) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            var teachers = await _teacherService.GetAllAsync();
            ViewData["TeacherId"] = new SelectList(teachers, "Id", "Name", course.TeacherId);
            return View(course);
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var course = await _courseService.GetByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _courseService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private string GetRandomColor()
        {
            string[] colors = new string[] { "#ffeb3b", "#ff7043", "#66bb6a", "#42a5f5", "#ab47bc", "#26c6da", "#ffd54f", "#8d6e63" };
            var random = new Random();
            return colors[random.Next(colors.Length)];
        }
    }
}
