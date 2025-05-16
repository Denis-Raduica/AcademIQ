using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using AcademIQ.Models;
using AcademIQ.Services.Interfaces;
using AcademIQ.Services;

namespace AcademIQ.Controllers
{
    public class EnrollmentsController : Controller
    {
        private readonly IEnrollmentService _enrollmentService;
        private readonly ICourseService _courseService;
        private readonly IStudentsService _studentService;

        public EnrollmentsController(
            IEnrollmentService enrollmentService,
            ICourseService courseService,
            IStudentsService studentService)
        {
            _enrollmentService = enrollmentService;
            _courseService = courseService;
            _studentService = studentService;
        }

        // GET: Enrollments
        public async Task<IActionResult> Index()
        {
            var enrollments = await _enrollmentService.GetWithDetailsAsync();
            return View(enrollments);
        }

        // GET: Enrollments/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null) return NotFound();

            var enrollment = await _enrollmentService.GetByIdAsync(id);
            if (enrollment == null) return NotFound();

            return View(enrollment);
        }

        // GET: Enrollments/Create
        public async Task<IActionResult> Create()
        {
            var courses = await _courseService.GetAllAsync();
            var students = await _studentService.GetAllAsync();

            ViewData["CourseId"] = new SelectList(courses, "CourseId", "CourseName");
            ViewData["StudentId"] = new SelectList(students, "Id", "Email");

            return View();
        }

        // POST: Enrollments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentId,CourseId")] Enrollments enrollment)
        {
            if (ModelState.IsValid)
            {
                enrollment.EnrollmentId = Guid.NewGuid().ToString(); // Auto-generate the ID here
                enrollment.EnrollmentDate = DateTime.UtcNow;
                await _enrollmentService.AddAsync(enrollment);
                return RedirectToAction(nameof(Index));
            }

            var courses = await _courseService.GetAllAsync();
            var students = await _studentService.GetAllAsync();

            ViewData["CourseId"] = new SelectList(courses, "CourseId", "CourseName", enrollment.CourseId);
            ViewData["StudentId"] = new SelectList(students, "Id", "Email", enrollment.StudentId);

            return View(enrollment);
        }


        // GET: Enrollments/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null) return NotFound();

            var enrollment = await _enrollmentService.GetByIdAsync(id);
            if (enrollment == null) return NotFound();

            var courses = await _courseService.GetAllAsync();
            var students = await _studentService.GetAllAsync();

            ViewData["CourseId"] = new SelectList(courses, "CourseId", "CourseName", enrollment.CourseId);
            ViewData["StudentId"] = new SelectList(students, "Id", "Email", enrollment.StudentId);

            return View(enrollment);
        }

        // POST: Enrollments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Enrollments enrollment)
        {
            if (id != enrollment.EnrollmentId)
                return NotFound();

            if (ModelState.IsValid)
            {
                await _enrollmentService.UpdateAsync(enrollment);
                return RedirectToAction(nameof(Index));
            }

            var courses = await _courseService.GetAllAsync();
            var students = await _studentService.GetAllAsync();

            ViewData["CourseId"] = new SelectList(courses, "CourseId", "CourseName", enrollment.CourseId);
            ViewData["StudentId"] = new SelectList(students, "Id", "Email", enrollment.StudentId);

            return View(enrollment);
        }

        // GET: Enrollments/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null) return NotFound();

            var enrollment = await _enrollmentService.GetByIdAsync(id);
            if (enrollment == null) return NotFound();

            return View(enrollment);
        }

        // POST: Enrollments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _enrollmentService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
