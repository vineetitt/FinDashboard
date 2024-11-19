using FinDashboard.API.Data;
using FinDashboard.API.Models.Domain;
using FinDashboard.API.Models.DTOs.UserDto;
using FinDashboard.API.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinDashboard.API.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly FinDashboardDbContext finDashboardDbContext;

        public UserRepository(FinDashboardDbContext finDashboardDbContext)
        {
            this.finDashboardDbContext = finDashboardDbContext;
        }

        public bool AddUser(User user)
        {
            var existingUser = finDashboardDbContext.Users.FirstOrDefault(u => u.Email == user.Email || u.Username == user.Username);

            if (existingUser != null)
            {
                if (existingUser.IsActive == false)
                {
                    existingUser.IsActive = true;
                }
                else
                {
                    throw new CustomException("User with either such username or email already exists", 400);
                }

            }

            finDashboardDbContext.Users.Add(user);
            finDashboardDbContext.SaveChanges();

            var portfolio = new Portfolio()
            {
                UserId = user.UserID,
                CurrentValue = 0,
                InvestedValue = 0,
            };
            finDashboardDbContext.Portfolios.Add(portfolio);
            return true;
        }

        public User GetUserById(int userId)
        {
            var user = finDashboardDbContext.Users
                .Include(u => u.Portfolio)
                    .ThenInclude(p => p.Assets)
                .FirstOrDefault(u => u.UserID == userId);
            if (user != null)
            {
                return user;
            }
            else
            {
                throw new CustomException($"User with userId: {userId} not found", 200);
            }

        }

        public bool UpdateUser(int userId, UpdateUserDto updateUserDto)
        {
            var user = finDashboardDbContext.Users.FirstOrDefault(user => user.UserID == userId);

            if (user != null)
            {
                user.Username = updateUserDto.Username;
                user.Email = updateUserDto.Email;
                finDashboardDbContext.SaveChanges();
                return true;
            }
            else
            {
                throw new CustomException($"User with userId: {userId} not found", 200);
            }

        }

        public bool DeleteUserById(int id)
        {
            var userToDelete = finDashboardDbContext.Users.FirstOrDefault(user => user.UserID == id);

            if (userToDelete != null)
            {
                userToDelete.IsActive = false;
                finDashboardDbContext.SaveChanges();
                return true;
            }
            else
            {
                throw new CustomException($"User with userId: {id} not found", 200);
            }
        }
    }
}
