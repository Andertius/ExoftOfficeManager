using System;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ExoftOfficeManager.IdentityServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _usrManager;

        public AuthController(UserManager<ApplicationUser> usrManager)
        {
            _usrManager = usrManager;
        }

        [HttpPost]
        public IActionResult Login()
        {
            // Дістати дані з того шо прийшло ззовні
            // по логіну дістати юзера
            // згенерувати access refresh token
            // перезаписав юзера в базу
            // return Ok(тут токени вернути);
            throw new NotImplementedException();
        }

        [HttpPost]
        public IActionResult Refresh()
        {
            // приймати старий access token і refresh token
            // зі старого токена брати id юзера з клеймів і по тій id шукати юзера
            // якщо є юзер, порівняти refresh токени
            // якщо співпадає, викликати згенерувати нові access refresh токени
            throw new NotImplementedException();
        }
    }
}
