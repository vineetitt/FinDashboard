using FinDashboard.API.Data;
using FinDashboard.API.Models.Domain;
using FinDashboard.API.Models.DTOs.UserDto;
using FinDashboard.API.Repository;
using FinDashboard.API.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
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
        private readonly TokenGenerator tokenGenerator;

        public UsersController(FinDashboardDbContext dbContext, IUserRepository userRepository , TokenGenerator tokenGenerator)
        {
            _dbContext = dbContext;
            this.userRepository = userRepository;
            this.tokenGenerator = tokenGenerator;
        }

        /// <summary>
        /// Authenticates a user and returns a token
        /// </summary>
        /// <param name="loginDto"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            try
            {
                if (string.IsNullOrEmpty(loginDto.Email) || string.IsNullOrEmpty(loginDto.Password))
                {
                    return BadRequest("Email or Password is missing.");
                }
                var user = userRepository.GetUserByEmail(loginDto.Email);
                if (user == null)
                {
                    return Unauthorized("Invalid email or password.");
                }
                var isPasswordValid = userRepository.VerifyPassword(loginDto.Password, user.PasswordHash);
                if (!isPasswordValid)
                {
                    return Unauthorized("Invalid email or password.");
                }
                var token = tokenGenerator.GenerateToken(user);
                return Ok(new
                {
                    Token = token,
                    User = new
                    {
                        user.UserID,
                        user.Username,
                        user.Email
                    }
                });
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

        /// <summary>
        /// Adds a new user to the system
        /// </summary>
        /// <param name="addUserDto"></param>
        /// <returns></returns>
        [Authorize]
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
                        Holdings = new List<Holding>()
                    }
                };

                var createdUser = userRepository.AddUser(user);
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

        /// <summary>
        /// Retrieves a user by their unique ID.
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        [Authorize]
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

        /// <summary>
        /// Updates the details of an existing user by their unique ID.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="updateUserDto"></param>
        /// <returns></returns>
        [Authorize]
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

        /// <summary>
        /// Deletes a user by their unique ID.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [Authorize]
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
