using System.Threading.Tasks;

using ExoftOfficeManager.Application.Services.Repositories;
using ExoftOfficeManager.Domain.Entities;

using Microsoft.AspNetCore.Mvc;

namespace ExoftOfficeManager.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;

        public UserController(IUserRepository repo)
        {
            _repository = repo;
        }

        [HttpGet("users/dummy-user")]
        public async Task<IActionResult> GetDummyUser()
        {
            var users = await _repository.GetAllUsers();
            var randomUser = users[4];

            return Ok(new User
            {
                Id = randomUser.Id,
                Avatar = "https://localhost:44377/images/avatars/image.jpg",
                FullName = randomUser.FullName,
                Role = randomUser.Role,
                Email = randomUser.Email,
            });
        }
    }
}
