using AcademIQ.Models;
using AcademIQ.Repositories.Interfaces;
using AcademIQ.Services.Interfaces;

namespace AcademIQ.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepo;

        public UsersService(IUsersRepository usersRepo)
        {
            _usersRepo = usersRepo;
        }

        public async Task<IEnumerable<Users>> GetAllUsersAsync()
        {
            return await _usersRepo.GetAllAsync();
        }

        public async Task<Users?> GetUserByIdAsync(string id)
        {
            return await _usersRepo.GetByIdAsync(id);
        }

        public async Task<Users?> GetUserByEmailAsync(string email)
        {
            return await _usersRepo.GetByEmailAsync(email);
        }

        public async Task<IEnumerable<Users>> SearchUsersByNameAsync(string name)
        {
            return await _usersRepo.SearchByNameAsync(name);
        }

        public async Task AddUserAsync(Users user)
        {
            await _usersRepo.AddAsync(user);
            await _usersRepo.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(Users user)
{
    var existingUser = await _usersRepo.GetByIdAsync(user.Id);
    if (existingUser == null)
        throw new InvalidOperationException("User not found");

    // Update properties individually
    existingUser.Name = user.Name;
    existingUser.Email = user.Email;
    existingUser.ContactInfo = user.ContactInfo;
    existingUser.ProfilePictureUrl = user.ProfilePictureUrl;

    _usersRepo.Update(existingUser); // only update the tracked entity
    await _usersRepo.SaveChangesAsync();
}


        public async Task DeleteUserAsync(Users user)
        {
            _usersRepo.Delete(user);
            await _usersRepo.SaveChangesAsync();
        }
    }
}
