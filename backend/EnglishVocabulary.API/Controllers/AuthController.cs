using EnglishVocabulary.API.DTOs.Auth;
using EnglishVocabulary.API.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EnglishVocabulary.API.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public AuthController(
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        [ProducesResponseType(
       typeof(RegisterResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]


        public async Task<ActionResult<RegisterResponse>> Register(
        [FromBody] RegisterRequest request)
        {
            // Kiểm tra email đã tồn tại
            var existingEmail = await _userManager.FindByEmailAsync(
                request.Email.Trim());

            if (existingEmail is not null)
            {
                return Conflict(new
                {
                    message = "Email hoặc username đã được sử dụng."
                });
            }

            // Chuẩn hóa username
            var userName = request.Username.Trim();

            var existingUserName =
                await _userManager.FindByNameAsync(userName);

            if (existingUserName is not null)
            {
                return Conflict(new
                {
                    message = "Email hoặc username đã được sử dụng."
                });
            }

            // Tạo tài khoản
            var user = new ApplicationUser
            {
                UserName = userName,
                Email = request.Email.Trim(),
                FullName = userName,
                CreatedAt = DateTime.UtcNow
            };

            var result = await _userManager.CreateAsync(
                user,
                request.Password);

            // Identity kiểm tra chính sách mật khẩu
            if (!result.Succeeded)
            {
                var errors = result.Errors
                    .Select(error => new
                    {
                        code = error.Code,
                        description = error.Description
                    });

                return BadRequest(new
                {
                    message = "Đăng ký không thành công.",
                    errors
                });
            }

            var response = new RegisterResponse
            {
                Id = user.Id,
                Email = user.Email!,
                UserName = user.UserName!,
                CreatedAt = user.CreatedAt
            };

            return StatusCode(
                StatusCodes.Status201Created,
                response);
        }
    }
}
