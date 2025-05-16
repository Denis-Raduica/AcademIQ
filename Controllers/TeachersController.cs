using AcademIQ.Models;
using AcademIQ.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace AcademIQ.Controllers
{
    public class TeachersController : Controller
    {
        private readonly ITeacherService _teacherService;
        private readonly IUsersService _usersService;

        public TeachersController(ITeacherService teacherService, IUsersService usersService)
        {
            _teacherService = teacherService;
            _usersService = usersService;
        }

        // GET: Teachers
        public async Task<IActionResult> Index()
        {
            var teachers = await _teacherService.GetAllAsync();
            return View(teachers);
        }

        // GET: Teachers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _teacherService.GetByIdAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        // GET: Teachers/Create
        public async Task<IActionResult> Create()
        {
            // Fetch all users to assign to a teacher
            var users = await _usersService.GetAllUsersAsync();
            ViewData["UserID"] = new SelectList(users, "UserId", "Email");
            return View();
        }

        // POST: Teachers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name, Email, ProfilePictureUrl, ContactInfo, Hours, Specialization")] Teachers teacher)
        {
            if (ModelState.IsValid)
            {
                teacher.Id = Guid.NewGuid().ToString(); // Auto-generate the ID here
                await _teacherService.AddAsync(teacher);
                return RedirectToAction(nameof(Index));
            }

            var users = await _usersService.GetAllUsersAsync();
            ViewData["UserID"] = new SelectList(users, "UserId", "Email", teacher.UserID);
            return View(teacher);
        }

        // GET: Teachers/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _teacherService.GetByIdAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }

            var users = await _usersService.GetAllUsersAsync();
            ViewData["UserID"] = new SelectList(users, "UserId", "Email", teacher.UserID);
            return View(teacher);
        }

        // POST: Teachers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("UserID, Name, Email, ProfilePictureUrl, ContactInfo, Hours, Specialization")] Teachers teacher)
        {
            if (id != teacher.UserID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _teacherService.UpdateAsync(teacher);
                return RedirectToAction(nameof(Index));
            }

            var users = await _usersService.GetAllUsersAsync();
            ViewData["UserID"] = new SelectList(users, "UserId", "Email", teacher.UserID);
            return View(teacher);
        }

        // GET: Teachers/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _teacherService.GetByIdAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        // POST: Teachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _teacherService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
