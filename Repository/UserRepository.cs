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
        private readonly IPortfolioRepository portfolioRepository;

        public UserRepository(FinDashboardDbContext finDashboardDbContext, IPortfolioRepository portfolioRepository)
        {
            this.finDashboardDbContext = finDashboardDbContext;
            this.portfolioRepository = portfolioRepository;
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
            portfolioRepository.AddPortfolioByUserId(user.UserID);
            return true;
        }

        public User GetUserById(int userId)
        {
            var user = finDashboardDbContext.Users
                .Include(u => u.Portfolio)
                    .ThenInclude(p => p.Holdings)
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
            using var transaction = finDashboardDbContext.Database.BeginTransaction();
            try
            {
                var userToDelete = finDashboardDbContext.Users
                    .Include(u => u.Portfolio)
                        .ThenInclude(p => p.Holdings)
                    .Include(u => u.Portfolio)
                        .ThenInclude(p => p.Transactions)
                    //.Include(u=>u.Portfolio) 
                    //    .ThenInclude(p=>p.)
                    .FirstOrDefault(u => u.UserID == id);
                if (userToDelete == null)
                {
                    throw new CustomException($"User with userId: {id} not found", 404);
                }
                if (userToDelete.Portfolio.Transactions.Any())
                {
                    finDashboardDbContext.Transactions.RemoveRange(userToDelete.Portfolio.Transactions);
                }
                if (userToDelete.Portfolio.Holdings.Any())
                {
                    finDashboardDbContext.Holdings.RemoveRange(userToDelete.Portfolio.Holdings);
                }
                finDashboardDbContext.Users.Remove(userToDelete);
                finDashboardDbContext.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new CustomException($"Failed to delete user with userId: {id}", 500);
            }
        }
    }
}
