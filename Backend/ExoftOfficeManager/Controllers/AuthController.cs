using System;
using System.Linq;
using System.Threading.Tasks;

using ExoftOfficeManager.Application.Services;
using ExoftOfficeManager.Application.Services.Repositories;
using ExoftOfficeManager.Application.Users.Commands.AddUser;
using ExoftOfficeManager.Application.Users.Queries.FindUserByEmail;
using ExoftOfficeManager.Domain.Entities;
using ExoftOfficeManager.Domain.Enums;
using ExoftOfficeManager.Infrastructure.Identity;
using ExoftOfficeManager.Requests.Auth;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ExoftOfficeManager.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;

        private readonly UserManager<AppIdentityUser> _userManager;
        private readonly SignInManager<AppIdentityUser> _signInManager;
        private readonly RoleManager<AppIdentityRole> _roleManager;

        private readonly IMediator _mediator;

        public AuthController(
            IUserRepository userRepo,
            IEmailService mailService,
            UserManager<AppIdentityUser> usrMngr,
            SignInManager<AppIdentityUser> signInMngr,
            RoleManager<AppIdentityRole> roleManager,
            IMediator mediator)
        {
            _userRepository = userRepo;

            _userManager = usrMngr;
            _signInManager = signInMngr;
            _roleManager = roleManager;

            _emailService = mailService;
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user is not null)
            {
                await _signInManager.SignOutAsync();

                if ((await _signInManager.PasswordSignInAsync(request.UserName, request.Password, false, false)).Succeeded)
                {
                    return NoContent();
                }
            }

            ModelState.AddModelError("", "Invalid name or password");
            var error = new
            {
                message = "The request is invalid.",
                error = ModelState.Values.SelectMany(e=> e.Errors.Select(er=>er.ErrorMessage))
            };

            return BadRequest(error);
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return NoContent();
        }

        [HttpPost("signup")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Signup([FromBody] SignUpRequest request)
        {
            var user = new AppIdentityUser { UserName = request.UserName, Email = request.Email };
            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                if ((await _mediator.Send(new FindUserByEmailQuery(request.Email))).User is null)
                {
                    await _mediator.Send(new AddUserCommand(new User
                    {
                        Email = request.Email,
                        FullName = request.FullName,
                        Role = request.Role,
                    }));
                }

                await _userManager.AddToRoleAsync(user, Enum.GetName(typeof(UserRole), request.Role));

                if ((await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, false)).Succeeded)
                {
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var confirmationLink = Url.Action(nameof(ConfirmEmail), "Auth", new { userId = user.Id, token }, Request.Scheme);

                    _emailService.SendEmailConfirmationEmail(
                        $"Dear {user.UserName},<br/>Here is the <a href=\"{confirmationLink}\">link</a> to confirm your email.",
                        user.Email);

                    return NoContent();
                }
            }

            foreach (var err in result.Errors)
            {
                ModelState.AddModelError("", err.Description);
            }

            ModelState.AddModelError("", "Invalid name or password");
            var error = new
            {
                message = "The request is invalid.",
                error = ModelState.Values.SelectMany(e => e.Errors.Select(er => er.ErrorMessage))
            };

            return BadRequest(error);
        }

        [HttpPut("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId is null || token is null)
            {
                return BadRequest();
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user is null)
            {
                return BadRequest();
            }

            if ((await _userManager.ConfirmEmailAsync(user, token)).Succeeded)
            {
                await _userRepository.AddUser(new User
                {
                    Avatar = "",
                    Role = (UserRole)Enum.Parse(typeof(UserRole), (await _userManager.GetRolesAsync(user)).FirstOrDefault()),
                });

                return NoContent();
            }

            return Unauthorized("There was a problem confirming your email");
        }

        [HttpPut("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user is not null)
            {
                var result = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);

                if (result.Succeeded)
                {
                    return NoContent();
                }

                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
            }

            ModelState.AddModelError("", "Invalid name or password");
            var error = new
            {
                message = "The request is invalid.",
                error = ModelState.Values.SelectMany(e => e.Errors.Select(er => er.ErrorMessage))
            };

            return BadRequest(error);
        }
    }
}
