using System.Threading.Tasks;

using ExoftOfficeManager.Domain.Entities;
using ExoftOfficeManager.Domain.Enums;

using Microsoft.AspNetCore.Mvc;

namespace ExoftOfficeManager.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public UserController()
        {
        }

        [HttpGet("users/dummy-user")]
        public async Task<IActionResult> GetUser()
        {
            return Ok(new User
            {
                Avatar = "https://localhost:44377/images/avatars/image.jpg",
                FullName = "Alissa White-Gluz",
                Role = UserRole.Developer,
            });
        }
    }
}
