using FinDashboard.API.Data;
using FinDashboard.API.Models.Domain;
using FinDashboard.API.Models.DTOs;
using FinDashboard.API.Repository;
using FinDashboard.API.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace FinDashboard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly FinDashboardDbContext _dbContext;
        private readonly IUserRepository userRepository;

        public UsersController(FinDashboardDbContext dbContext, IUserRepository userRepository)
        {
            _dbContext = dbContext;
            this.userRepository = userRepository;
        }

        [HttpPost]
        public IActionResult AddUser([FromBody] AddUserDto addUserDto)
        {
            try
            {
                if (addUserDto == null || string.IsNullOrEmpty(addUserDto.Email) || string.IsNullOrEmpty(addUserDto.UserName))
                {
                    return BadRequest("Invalid user data");
                }

                var user = new User()
                {
                    Username = addUserDto.UserName,
                    Email = addUserDto.Email,
                    PasswordHash = addUserDto.HashPassword,
                    Portfolio = new Portfolio()
                    {
                        Assets = new List<Asset>()
                    }
                };

                var createdUser = userRepository.AddUser(user);
                //var result = new AddUserDto()
                //{
                //    Email = createdUser.Email,
                //    UserName = createdUser.UserName,
                //};
                return Ok();
            }
            catch (CustomException ex)
            {
                return StatusCode(ex.statusCode, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetUserById(int userid)
        {
            try
            {
                var resultedUser = userRepository.GetUserById(userid);
                return Ok(resultedUser);
            }
            catch (CustomException ex)
            {
                return StatusCode(ex.statusCode, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{userId}")]
        public IActionResult UpdateUserById(int userId, [FromBody] UpdateUserDto updateUserDto)
        {
            try
            {
                var isUpdated = userRepository.UpdateUser(userId, updateUserDto);
                return Ok();
            }
            catch (CustomException ex)
            {
                return StatusCode(ex.statusCode, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public IActionResult DeleteUserById(int userId)
        {
            try
            {
                var isUserDeleted = userRepository.DeleteUserById(userId);
                return Ok();
            }
            catch (CustomException ex)
            {
                return StatusCode(ex.statusCode, ex.Message);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }
    }
}
