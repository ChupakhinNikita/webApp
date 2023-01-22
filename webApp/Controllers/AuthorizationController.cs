using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webApp.Models;

namespace webApp.Controllers
{
    public class AuthorizationController : Controller
    {
        public IActionResult Authorization()
        {
            return View();
        }
    }
}
