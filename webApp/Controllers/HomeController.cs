using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using System.Security.Claims;
using webApp.Models;
using System.Text.Json;
using System;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

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
            //string requestJson = "{\"studentId\": 1,\"method\": \"studentProfile\"}";
            //Student? stud = await _context.Students.FirstOrDefaultAsync(u => u.IdStudent == 1); // Student
            //string jsonData = JsonConvert.SerializeObject(stud); // json Student

            // Полученный ответ с сервера переводим из формата JSON в Student (Десериализация)
            string responseJson = "{\"idStudent\":1,\"lastName\":\"Смирнов\",\"firstName\":\"Даниил\",\"patronomic\":\"Ярославович\",\"gradebook\":\"22-мРИС-01МИ\",\"group\":\"мРИС-221\",\"tuitionType\":\"Бюджет\",\"tuitionForm\":\"Очное обучение\",\"trainingLevel\":\"Магистр\",\"studentCondition\":\"Является студентом\",\"speciality\":\"Информационные системы и технологии\",\"specialization\":\"Информационные системы и технологии цифровизации\",\"course\":1,\"dateTime\":\"24.11.2022T12:00:00\"}";
            Student? student = JsonConvert.DeserializeObject<Student?>(responseJson);

            // если данные сконвертированы в Student
            if (student != null)
                return View(student);
            else
            {
                return NotFound();
            }
        }


        //public async Task<IActionResult> Student()
        //{
        //    string _id = HttpContext.User.FindFirst("IdUser").Value;
        //    int id = Convert.ToInt32(_id);
        //    if (id == null || _context.Users == null)
        //    {
        //        return NotFound();
        //    }

        //    var user = await _context.Users.FirstOrDefaultAsync(m => m.IdUser == id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(user);
        //}
    } 
}