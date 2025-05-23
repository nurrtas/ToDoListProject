using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Data;
using TodoList.BackApp.TodoApplication.TodoEntities.Entities;
using TodoList.BackApp.TodoApplication.TodoEntities.ItemDtos;

namespace TodoList.BackApp.TodoApplication.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IConfiguration _configuration;
        private readonly BackApp.TodoEntities.Entities.Entities.ApplicationDbContext _context;

        public AuthController(UserManager<UserEntity> userManager, IConfiguration configuration, BackApp.TodoEntities.Entities.Entities.ApplicationDbContext context)
        {
            _userManager = userManager;
            _configuration = configuration;
            _context = context;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterItemDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Transaction başlatılıyor, IsolationLevel girilmesi gerekiyor
            await using var transaction = await _context.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted); // ✔️


            try
            {
                var user = new UserEntity
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    Name = model.UserName,  
                    CreatedAt = DateTime.UtcNow
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await transaction.CommitAsync();
                    return Ok(new { Message = "Kullanıcı başarıyla oluşturuldu" });
                }
                else
                {
                    await transaction.RollbackAsync();
                    return BadRequest(new { Errors = result.Errors });
                }
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, new
                {
                    Message = "Kullanıcı oluşturulurken hata oluştu",
                    Error = ex.Message,
                    Inner = ex.InnerException?.Message,
                    StackTrace = ex.StackTrace
                });
            }

        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginItemDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var token = GenerateJwtToken(user);
                return Ok(new { Token = token });
            }

            return Unauthorized(new { Message = "Geçersiz kullanıcı adı veya şifre" });
        }

        private string GenerateJwtToken(UserEntity user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var key = Encoding.ASCII.GetBytes(jwtSettings["SecretKey"]);


            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.UserName ?? ""),
                new Claim(ClaimTypes.Email, user.Email ?? "")
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
