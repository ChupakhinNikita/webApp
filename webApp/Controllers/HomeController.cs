using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using webApp.Models;
using Newtonsoft.Json;
/*using TransferLibrary.NetworkTransfer;
using TransferLibrary.Export;
*/

namespace webApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly UsersContext _context;


        public HomeController(UsersContext context)
        {
            _context = context;
        }

        /*protected IRabbitTransfer TransferController { get; private set; } = default!;

        IServiceProvider service_provider = Host.CreateDefaultBuilder()
            .ConfigureServices((IServiceCollection service_collection) =>
            {
                service_collection.AddLogging().AddHttpClient().AddNetworkTransfer();
            }).Build().Services;
*/

        [Authorize(Policy = "OnlyForAdmin")]
        public async Task<IActionResult> Admin()
        {
            string _idAdmin = HttpContext.User.FindFirst("IdUser").Value;
            int idAdmin = Convert.ToInt32(_idAdmin);

            if (_context.Users == null)
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
            if ( _context.Users == null)
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
            /*            string _id = HttpContext.User.FindFirst("IdUser").Value;
                        int id = Convert.ToInt32(_id);
                        if (_context.Users == null)
                        {
                            return NotFound();
                        }

                        var user = await _context.Users.FirstOrDefaultAsync(m => m.IdUser == id);

                        var rabbit_client = service_provider.GetService<IRabbitTransfer>()!;

                        using var cancel_sourse = new CancellationTokenSource();

                        try
                        {
                            var studentData = rabbit_client.SendMessage(RequestType.Authorization, user.Id, cancel_sourse.Token);
                            Student? student = JsonConvert.DeserializeObject<Student?>(studentData.Result.JsonRecord[0].ToJson());

                            //if (studentData.Result == null) { Console.WriteLine("\n\tCANCELED\n"); }

                            Console.WriteLine("\n");
                            foreach (var item in studentData.Result.JsonRecord)
                            {
                                foreach (var record in item) 
                                    Console.WriteLine($"{record.Key}: {record.Value}");

                                Console.WriteLine("\n");
                            }

                            Console.WriteLine("JSON studentData: " + studentData.Result.JsonRecord[0].ToJson() + "\n");
            */

            Student? student = new Student();
            // если данные сконвертированы в Student
            if (true)
                    return View(student);
                else
                {
                    return NotFound();
                }
        /*    }
            catch (AggregateException error) when (error.InnerException is TransferException)
            {
                Console.WriteLine("\n");

                Console.WriteLine($"{error.Message}");
                return View();
            }*/
            
        }


        public async Task<IActionResult> RecordBook()
        {
            /*            string _id = HttpContext.User.FindFirst("IdUser").Value;
                        int id = Convert.ToInt32(_id);
                        if (_context.Users == null)
                        {
                            return NotFound();
                        }

                        var user = await _context.Users.FirstOrDefaultAsync(m => m.IdUser == id);

                        var rabbit_client = service_provider.GetService<IRabbitTransfer>()!;


                        using var cancel_sourse = new CancellationTokenSource();
                        try
                        {
                            var recordBookData = rabbit_client.SendMessage(RequestType.Statements, user.Id, cancel_sourse.Token);
                            ());
                            Console.WriteLine("JSON recordBookData: " + recordBookData.Result.JsonRecord[0].ToJson() + "\n");


                            if (book != null)
                                return View(book);
                            else
                            {
                                return NotFound();
                            }
                        }
                        catch (AggregateException error) when (error.InnerException is TransferException)
                        {
                            Console.WriteLine("\n");

                            Console.WriteLine($"{error.Message}");
                            return View();
                        }*/
            RecordBook? book = new RecordBook();
            if (true)
                return View(book);
            else
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> Orders()
        {/*
            string _id = HttpContext.User.FindFirst("IdUser").Value;
            int id = Convert.ToInt32(_id);
            if (_context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FirstOrDefaultAsync(m => m.IdUser == id);

            var rabbit_client = service_provider.GetService<IRabbitTransfer>()!;

            using var cancel_sourse = new CancellationTokenSource();
            try
            {
                var ordersData = rabbit_client.SendMessage(RequestType.Orders, user.Id, cancel_sourse.Token);
                Orders? order = JsonConvert.DeserializeObject<Orders?>(ordersData.Result.JsonRecord[0].ToJson());
                Console.WriteLine("\nJSON ordersData: " + ordersData.Result.JsonRecord[0].ToJson() + "\n");


                if (order != null)
                    return View(order);
                else
                {
                    return NotFound();
                }
            }
            catch (AggregateException error) when (error.InnerException is TransferException)
            {
                Console.WriteLine("\n");

                Console.WriteLine($"{error.Message}");
                return View();
            }*/
            Orders? ord = new Orders();
            if (true)
                return View(ord);
            else
            {
                return NotFound();
            }
        }
    }
    } 
