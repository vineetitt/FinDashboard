using FinDashboard.API.Models.Domain;
using FinDashboard.API.Models.DTOs.UserDto;

namespace FinDashboard.API.Repository.IRepository
{
    public interface IUserRepository
    {
        bool AddUser(User user);
        User GetUserById(int id);
        bool UpdateUser(int userId, UpdateUserDto updateUserDto);
        bool DeleteUserById(int id);
    }



}
