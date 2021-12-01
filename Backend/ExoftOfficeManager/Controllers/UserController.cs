using System;
using System.Threading.Tasks;

using ExoftOfficeManager.Application.Services.Repositories;
using ExoftOfficeManager.Application.Users.Commands.UpdateUser;
using ExoftOfficeManager.Domain.Entities;
using ExoftOfficeManager.Requests;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace ExoftOfficeManager.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly IMediator _mediator;

        public UserController(
            IMediator mediator,
            IUserRepository repo)
        {
            _mediator = mediator;
            _repository = repo;
        }

        [HttpGet("users/dummy-user")]
        public async Task<IActionResult> GetDummyUser()
        {
            var users = await _repository.GetAllUsers();
            var randomUser = users[3];

            return Ok(new User
            {
                Id = randomUser.Id,
                Avatar = "https://localhost:44377/images/avatars/image.jpg",
                FullName = randomUser.FullName,
                Role = randomUser.Role,
                Email = randomUser.Email,
            });
        }

        [HttpPut("users/{userId}/update-user")]
        public async Task<IActionResult> UpdateUser(
            [FromRoute] Guid userId,
            [FromBody] UpdateUserRequest request)
        {
            await _mediator.Send(new UpdateUserCommand(
                userId,
                request.FullName,
                request.Avatar));

            return NoContent();
        }
    }
}
