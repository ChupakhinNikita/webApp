using Microsoft.AspNetCore.Mvc;
using webApp.Models;

namespace webApp.Controllers
{
    public class AuthorizationController : Controller
    {
        UsersContext db = new UsersContext();

        public IActionResult Authorization()
        {
            return View();
        }

        public IActionResult AllUsers()
        {
            IEnumerable<User> users = db.Users.ToList();
            return View(users);
        }
    }
}
