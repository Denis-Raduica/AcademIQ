using AcademIQ.Models;
using AcademIQ.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AcademIQ.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IStudentsService _studentsService;
        private readonly IUsersService _usersService;
        private readonly IEnrollmentService _enrollmentService;

        public StudentsController(
    IStudentsService studentsService,
    IUsersService usersService,
    IEnrollmentService enrollmentService)
        {
            _studentsService = studentsService;
            _usersService = usersService;
            _enrollmentService = enrollmentService;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            var students = await _studentsService.GetAllAsync();
            return View(students);
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var student = await _studentsService.GetByIdAsync(id);
            if (student == null)
                return NotFound();

            var enrollments = await _enrollmentService.GetByStudentIdAsync(id);
            student.Enrollments = enrollments.ToList();

            return View(student);
        }

        // GET: Students/Create
        public async Task<IActionResult> Create()
        {
            var users = await _usersService.GetAllUsersAsync();
            ViewData["UserId"] = new SelectList(users, "UserID", "Email");
            return View();
        }

        // POST: Students/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Email,ContactInfo,ProfilePictureUrl,GPA,YearOfStudy,Major")] Students student)
        {
            if (ModelState.IsValid)
            {
                student.Id = Guid.NewGuid().ToString(); // Auto-generate the ID here
                await _studentsService.AddAsync(student);
                return RedirectToAction(nameof(Index));
            }

            var users = await _usersService.GetAllUsersAsync();
            ViewData["UserId"] = new SelectList(users, "UserID", "Email", student.Id);
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var student = await _studentsService.GetByIdAsync(id);
            if (student == null)
                return NotFound();

            var users = await _usersService.GetAllUsersAsync();
            ViewData["UserId"] = new SelectList(users, "UserID", "Email", student.Id);
            return View(student);
        }

        // POST: Students/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,Email,ContactInfo,ProfilePictureUrl,GPA,YearOfStudy,Major")] Students student)
        {
            if (id != student.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                await _studentsService.UpdateAsync(student);
                return RedirectToAction(nameof(Index));
            }

            var users = await _usersService.GetAllUsersAsync();
            ViewData["UserId"] = new SelectList(users, "UserID", "Email", student.Id);
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var student = await _studentsService.GetByIdAsync(id);
            if (student == null)
                return NotFound();

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var student = await _studentsService.GetByIdAsync(id);
            if (student != null)
            {
                await _studentsService.DeleteAsync(student);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
