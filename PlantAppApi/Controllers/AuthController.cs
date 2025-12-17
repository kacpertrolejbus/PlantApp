using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlantAppApi.Models;

namespace PlantAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(PlantAppDbContext context) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register(string email, string password)
        {
            if (await context.Users.AnyAsync(u => u.Email == email))
                return BadRequest("Email zajęty");

            
            var newUser = new User { Email = email, Password = password };

            context.Users.Add(newUser);
            await context.SaveChangesAsync();
            return Ok(new { id = newUser.IdUsers });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await context.Users
                .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);

            if (user == null) return Unauthorized();
            return Ok(new { id = user.IdUsers, email = user.Email });
        }
    }
}