using AcademIQ.Models;
using AcademIQ.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace AcademIQ.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersService _userService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UsersController(IUsersService userService, IWebHostEnvironment webHostEnvironment)
        {
            _userService = userService;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAllUsersAsync();
            return View(users);
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound();

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Users user, IFormFile? profilePicture)
        {
            if (ModelState.IsValid)
            {
                // Handle profile picture
                if (profilePicture != null && profilePicture.Length > 0)
                {
                    var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Assets", "Art");
                    var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(profilePicture.FileName);
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using var stream = new FileStream(filePath, FileMode.Create);
                    await profilePicture.CopyToAsync(stream);

                    user.ProfilePictureUrl = Path.Combine("Assets", "Art", uniqueFileName).Replace("\\", "/");
                }

                await _userService.AddUserAsync(user);
                return RedirectToAction(nameof(Index));
            }

            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound();

            return View(user);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Users user, IFormFile? profilePicture)
        {
            if (id != user.UserID)
                return NotFound();

            if (ModelState.IsValid)
            {
                if (profilePicture != null && profilePicture.Length > 0)
                {
                    var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Assets", "Art");
                    var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(profilePicture.FileName);
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using var stream = new FileStream(filePath, FileMode.Create);
                    await profilePicture.CopyToAsync(stream);

                    user.ProfilePictureUrl = Path.Combine("Assets", "Art", uniqueFileName).Replace("\\", "/");
                }

                await _userService.UpdateUserAsync(user);
                return RedirectToAction(nameof(Index));
            }

            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound();

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user != null)
            {
                await _userService.DeleteUserAsync(user);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
