using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using webApp.Models;

namespace webApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly UsersContext _context;

        public HomeController(UsersContext context)
        {
            _context = context;
        }

        [Authorize(Policy = "OnlyForAdmin")]
        public async Task<IActionResult> Admin()
        {
            string _idAdmin = HttpContext.User.FindFirst("IdUser").Value;
            int idAdmin = Convert.ToInt32(_idAdmin); 

            if (idAdmin == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.IdUser == idAdmin);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [Authorize(Policy = "OnlyForTeacher")]
        public async Task<IActionResult> Teacher()
        {
            string _id = HttpContext.User.FindFirst("IdUser").Value;
            int id = Convert.ToInt32(_id);
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FirstOrDefaultAsync(m => m.IdUser == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [Authorize(Policy = "OnlyForStudent")]
        public async Task<IActionResult> Student()
        {
            string _idStudent = HttpContext.User.FindFirst("IdUser").Value;
            int idStudent = Convert.ToInt32(_idStudent);
            if (idStudent == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FirstOrDefaultAsync(m => m.IdUser == idStudent);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
    }
}
