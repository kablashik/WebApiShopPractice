using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using WebApplicationL5.Data.Models;

namespace WebApplicationL5.Controllers;

public class CookieController : Controller
{
    private static List<User> _users = new List<User>
    {
        new User {UserId = 1, Name = "admin", Password = "admin", Role = "admin", Age = 39, Email = "Kos2akb@gmail.com"},
        new User {UserId = 2, Name = "user", Password = "123", Role = "user", Age = 20, Email = "Kos2akb@gmail.com" },
        new User {UserId = 3, Name = "oleg", Password = "qwerty", Role = "user", Age = 15, Email = "Kos2akb@gmail.com" },
        new User {UserId = 4, Name = "max", Password = "111", Role = "user", Age = 30, Email = "Kos2akb@gmail.com" },
    };

    [HttpGet]
    [Route("login2")]
    public IActionResult Login()
    {
        ViewData["ReturnUrl"] = HttpContext.Request.Query["returnUrl"];
        return View();
    }

    [HttpPost]
    [Route("login2")]
    public async Task<IActionResult> Login(User model, string returnUrl)
    {
        var user = AuthenticateUser(model.Name, model.Password);

        if (user != null)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role),
                new Claim("Age", user.Age.ToString()),
                new Claim("UserId", user.UserId.ToString() )
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return Redirect(returnUrl);
        }

        return View(model);
    }

    public User AuthenticateUser(string username, string password)
    {
        var user = _users.FirstOrDefault(u => u.Name == username & u.Password == password);

        if (user != null && user.Password == password)
        {
            return user; // Пользователь успешно аутентифицирован.
        }

        return null; // Пользователь не найден или пароль неверный.
    }

}