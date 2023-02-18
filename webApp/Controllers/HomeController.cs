using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using webApp.Models;
using Newtonsoft.Json;
using TransferLibrary.NetworkTransfer;
using TransferLibrary.Export;
using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace webApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly UsersContext _context;


        public HomeController(UsersContext context)
        {
            _context = context;
        }

        protected IRabbitTransfer TransferController { get; private set; } = default!;

        IServiceProvider service_provider = Host.CreateDefaultBuilder()
            .ConfigureServices((IServiceCollection service_collection) =>
            {
                service_collection.AddLogging().AddHttpClient().AddNetworkTransfer();
            }).Build().Services;


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
                var studentData = rabbit_client.SendMessage(RequestType.Authorization, user.Id, cancel_sourse.Token);
                StudentJSON? student = JsonConvert.DeserializeObject<StudentJSON?>(studentData.Result.JsonRecord[0].ToJson());

                if (studentData.Result == null) { Console.WriteLine("\n\tCANCELED\n"); }

                Console.WriteLine("\n");
                foreach (var item in studentData.Result.JsonRecord)
                {
                    foreach (var record in item) 
                        Console.WriteLine($"{record.Key}: {record.Value}");

                    Console.WriteLine("\n");
                }

                Console.WriteLine("\nJSON studentData: " + studentData.Result.JsonRecord[0].ToJson() + "\n");


                return View(student);
            }
            catch (AggregateException error) when (error.InnerException is TransferException)
            {
                Console.WriteLine($"\n{error.Message}");
                return View();
            }
        }


        public async Task<IActionResult> RecordBook()
        {
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
                var recordBookData = rabbit_client.SendMessage(RequestType.Statements, user.Id, cancel_sourse.Token);

                Console.WriteLine("\nJSON recordBookData: " + recordBookData.Result.JsonRecord[0].ToJson() + "\n");

                int count = recordBookData.Result.JsonRecord.Count() - 2;
                Console.WriteLine("\nKEY: " + count + "\n");


                List<RecordBookJSON> books = new List<RecordBookJSON>();

                for (int i = 0; i < count; i++)
                {
                    RecordBookJSON? bookData = JsonConvert.DeserializeObject<RecordBookJSON?>(recordBookData.Result.JsonRecord[i].ToJson());
                    books.Add(bookData);
                }

                foreach (var item in recordBookData.Result.JsonRecord)
                {
                    foreach (var record in item)
                        Console.WriteLine($"{record.Key}: {record.Value}");

                    Console.WriteLine("\n");
                }

                if (books != null)
                    return View(books);
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
            }
        }


        public async Task<IActionResult> Orders()
        {

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
                
                Console.WriteLine("\nJSON ordersData: " + ordersData.Result.JsonRecord[0].ToJson() + "\n");

                int count = ordersData.Result.JsonRecord.Count() - 2;
                Console.WriteLine("\nKEY: " + count + "\n");

                List<OrdersJSON> orders = new List<OrdersJSON>();

                for (int i = 0; i < count; i++) {
                    OrdersJSON orderd = JsonConvert.DeserializeObject<OrdersJSON>(ordersData.Result.JsonRecord[i].ToJson());
                    orders.Add(orderd);
                }

                OrdersJSON? order = JsonConvert.DeserializeObject<OrdersJSON?>(ordersData.Result.JsonRecord[0].ToJson());
                if (order != null)
                    return View(orders);
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
            }
        }
    }
    } 
