using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using webApp.Models;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;

namespace webApp.Controllers
{
    public class AccountController : Controller
    {

        private UsersContext _context;
        public AccountController(UsersContext context)
        {
            _context = context;
        }

        /*[HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(User user)
        {
            // получаем из формы login и пароль
            var form = HttpContext.Request.Form;

            string login = form["login"];
            string password = form["Password"];

            // находим пользователя 
            User? user = _context.Users.FirstOrDefault(u => u.Login == login && u.Password == password);

            *//*Обработать*//*
            // если пользователь не найден, отправляем статусный код 401
            if (user is null) 
                return Results.Unauthorized();
            *//*Обработать*//*

            var claims = new List<Claim> {
                new Claim(ClaimTypes.Name, user.Login),
                new Claim(ClaimTypes.Role, user.Role)
            };
            // создаем объект ClaimsIdentity
            var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            // установка аутентификационных куки
            await HttpContext.SignInAsync(claimsPrincipal);

            if (user.Role == "Администратор")
            {
                return RedirectToAction("Admin", "Home");
            }
            else if (user.Role == "Студент")
            {
                return RedirectToAction("Student", "Student");
            }
            else if (user.Role == "Преподователь")
            {
                return RedirectToAction("Teacher", "Teacher");
            }
        }*/

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                // находим пользователя 
                // User user = await _context.Users.FirstOrDefaultAsync(u => u.Login == model.Login && u.Password == model.Password);
                User? user = await _context.Users.FirstOrDefaultAsync(u => u.Login == model.Login && u.Password == model.Password);
                if (user != null)
                {
                    await Authenticate(user); // аутентификация

                    if (user.Role == "Администратор")
                    {
                        return RedirectToAction("Admin", "Home");
                    }
                    else if (user.Role == "Студент")
                    {
                        return RedirectToAction("Student", "Home");
                    }
                    else if (user.Role == "Преподователь")
                    {
                        return RedirectToAction("Teacher", "Home");
                    }
                }
            }
            return View(model);
        }
        private async Task Authenticate(User user)
        {
            var claims = new List<Claim> {
                new Claim(ClaimTypes.Name, user.Login),
                new Claim(ClaimTypes.Role, user.Role)
            };
            // создаем объект ClaimsIdentity
            var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(claimsPrincipal);
        }
    }
}
