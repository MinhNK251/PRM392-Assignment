using AirWaterStore.API.DTOs;
using AirWaterStore.Business.Interfaces;
using AirWaterStore.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AirWaterStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/user?pageNumber=1&pageSize=10
        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] int userId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var users = await _userService.GetAllAsync(userId, pageNumber, pageSize);
            var total = await _userService.GetTotalCountAsync();

            return Ok(new
            {
                totalCount = total,
                data = users
            });
        }

        // GET: api/user/5
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetByIdAsync(int userId)
        {
            var user = await _userService.GetByIdAsync(userId);
            if (user == null) return NotFound();

            return Ok(user);
        }

        // POST: api/user/login
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDto dto)
        {
            var user = await _userService.LoginAsync(dto.Email, dto.Password);
            if (user == null) return Unauthorized("Invalid email or password");
            if ((bool)user.IsBan) return Unauthorized("User is Banned");
            return Ok(user);
        }

        // POST: api/user
        [HttpPost]
        public async Task<IActionResult> AddCustomerAsync([FromBody] RegisterDto dto)
        {
            if (dto.Password != dto.ConfirmPassword)
                return BadRequest("Passwords do not match");

            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                Password = dto.Password,
                Role = 1 // Customer
            };

            await _userService.AddAsync(user);
            return Ok(user);
        }

        // POST: api/user/staff
        [HttpPost("staff")]
        public async Task<IActionResult> AddStaffAsync([FromBody] RegisterDto dto)
        {
            if (dto.Password != dto.ConfirmPassword)
                return BadRequest("Passwords do not match");

            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                Password = dto.Password,
                Role = 2 // Staff
            };

            await _userService.AddAsync(user);
            return Ok(user);
        }

        // PUT: api/user/5
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateUserDto dto)
        {
            var user = await _userService.GetByIdAsync(dto.UserId);
            if (user == null)
                return NotFound();

            user.Username = dto.Username;
            await _userService.UpdateAsync(user);
            return NoContent();
        }

        // PUT: api/user/reset-password
        [HttpPut("reset-password")]
        public async Task<IActionResult> ResetPasswordAsync([FromBody] ResetPasswordDto dto)
        {
            if (dto.Password != dto.ConfirmPassword)
                return BadRequest("Passwords do not match");

            var user = await _userService.GetByIdAsync(dto.UserId);
            if (user == null)
                return NotFound();

            user.Password = dto.Password;
            await _userService.UpdateAsync(user);
            return NoContent();
        }

        // PUT: api/user/ban-unban
        [HttpPut("ban-unban/{userId}")]
        public async Task<IActionResult> BanUnbanAsync(int userId)
        {
            var user = await _userService.GetByIdAsync(userId);
            if (user == null)
                return NotFound();
            user.IsBan = !user.IsBan;
            await _userService.UpdateAsync(user);
            return NoContent();
        }
    }
}
