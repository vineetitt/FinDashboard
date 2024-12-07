using FinDashboard.API.Models.Domain;
using FinDashboard.API.Models.DTOs.UserDto;

namespace FinDashboard.API.Repository.IRepository
{
    public interface IUserRepository
    {
        User GetUserByEmail(string email);
        bool VerifyPassword(string password, string hashedPassword);
        bool AddUser(User user);
        User GetUserById(int id);
        bool UpdateUser(int userId, UpdateUserDto updateUserDto);
        bool DeleteUserById(int id);
    }

}
