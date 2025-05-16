using AcademIQ.Models;

namespace AcademIQ.Services.Interfaces
{
    public interface IUsersService
    {
        Task<IEnumerable<Users>> GetAllUsersAsync();
        Task<Users?> GetUserByIdAsync(string id);
        Task<Users?> GetUserByEmailAsync(string email);
        Task<IEnumerable<Users>> SearchUsersByNameAsync(string name);

        Task AddUserAsync(Users user);
        Task UpdateUserAsync(Users user);
        Task DeleteUserAsync(Users user);
    }
}
