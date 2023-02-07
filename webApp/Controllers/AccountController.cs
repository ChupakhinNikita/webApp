using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using webApp.Models;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using Newtonsoft.Json;
using System;

namespace webApp.Controllers
{
    public class AccountController : Controller
    {

        private UsersContext _context;
        public AccountController(UsersContext context)
        {
            _context = context;
        }

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
                        //Student? student = await _context.Students.FirstOrDefaultAsync(u => u.IdStudent == 1);
                        //string jsonData = JsonConvert.SerializeObject(student);

                        return RedirectToAction("Student", "Home");
                    }
                    else if (user.Role == "Преподователь")
                    {
                        //Teacher? teacher = await _context.Teachers.FirstOrDefaultAsync(u => u.IdTeacher == 1);
                        //string jsonData = JsonConvert.SerializeObject(teacher);

                        return RedirectToAction("Teacher", "Home");
                    }
                }
                else 
                {
                    ModelState.AddModelError("Name", "Некорректные логин и(или) пароль");
                }
            }
            return View(model);
        }
        private async Task Authenticate(User user)
        {
            var claims = new List<Claim> {
                new Claim(ClaimTypes.Name, user.Login),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim("IdUser" , user.IdUser.ToString())
            };
            // создаем объект ClaimsIdentity
            var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(claimsPrincipal);
        }

        [HttpGet]
        public async Task<IActionResult> Logout() 
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
