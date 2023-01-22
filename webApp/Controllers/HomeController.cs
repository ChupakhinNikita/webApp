using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webApp.Models;

namespace webApp.Controllers
{
    public class HomeController : Controller
    {
        [Authorize(Policy = "OnlyForAdmin")]
        public IActionResult Admin()
        {
            return View();
        }

        [Authorize(Policy = "OnlyForTeacher")]
        public IActionResult Teacher()
        {
            return View();
        }

        [Authorize(Policy = "OnlyForStudent")]
        public IActionResult Student()
        {
            return View();
        }
    }
}
