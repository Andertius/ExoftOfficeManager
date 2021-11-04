using System;
using System.Linq;
using System.Threading.Tasks;

using ExoftOfficeManager.Application.Services;
using ExoftOfficeManager.Application.Services.Repositories;
using ExoftOfficeManager.Domain.Entities;
using ExoftOfficeManager.Domain.Enums;
using ExoftOfficeManager.Infrastructure.Identity;
using ExoftOfficeManager.Requests.Auth;

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

        public AuthController(IUserRepository userRepo, UserManager<AppIdentityUser> usrMngr, SignInManager<AppIdentityUser> signInMngr)
        {
            _userRepository = userRepo;
            _userManager = usrMngr;
            _signInManager = signInMngr;
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
                    return Redirect(request.ReturnUrl ?? "/");
                }
            }

            ModelState.AddModelError("", "Invalid name or password");
            return Unauthorized();
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<RedirectResult> Logout([FromBody] string returnUrl = "/")
        {
            await _signInManager.SignOutAsync();
            return Redirect(returnUrl);
        }

        [HttpPost("signup")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Signup([FromBody] SignUpRequest request)
        {
            var user = new AppIdentityUser { UserName = request.UserName, Email = request.Email };
            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                if ((await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, false)).Succeeded)
                {
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var confirmationLink = Url.Action(nameof(ConfirmEmail), "Auth", new { userId = user.Id, token }, Request.Scheme);

                    _emailService.SendEmailConfirmationEmail(
                        $"Dear {user.UserName},\nHere is the <a href=\"{confirmationLink}\">link</a> to confirm your email.",
                        user.Email);

                    return NoContent();
                }
            }

            foreach (var err in result.Errors)
            {
                ModelState.AddModelError("", err.Description);
            }

            return Unauthorized();
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
                    IdentityUser = user,
                    Role = (UserRole)Enum.Parse(typeof(UserRole), (await _userManager.GetRolesAsync(user)).FirstOrDefault()),
                });

                return NoContent();
            }

            return Unauthorized("There was a problem confirming your email, please try again later.");
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

            return BadRequest();
        }
    }
}
