using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using System.Security.Claims;
using System;
using Microsoft.EntityFrameworkCore;
using webApp.Models;

var builder = WebApplication.CreateBuilder(args);

// Сервисы MVC
builder.Services.AddControllersWithViews();

// Подключение к БД
string connection = "Host=localhost;Port=5432;Database=users;Username=postgres;Password=123456";
builder.Services.AddDbContext<UsersContext>(options => options.UseNpgsql(connection));

// Подключение аутентификации с помощью куки
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => options.LoginPath = "/");
builder.Services.AddAuthorization();

var app = builder.Build();

app.UseAuthentication();   // Добавление middleware аутентификации 
app.UseAuthorization();   // Добавление middleware авторизации 

app.MapPost("/", async (string? returnUrl, HttpContext context, UsersContext db) =>
{
    // получаем из формы login и пароль
    var form = context.Request.Form;
    // если login и/или пароль не установлены, посылаем статусный код ошибки 400
    if (!form.ContainsKey("login") || !form.ContainsKey("password"))
        return Results.BadRequest("Логин и/или пароль не установлены");

    string login = form["login"];
    string password = form["password"];

    // находим пользователя 
    User? user = db.Users.FirstOrDefault(u => u.Login == login && u.Password == password);

    // если пользователь не найден, отправляем статусный код 401
    if (user is null) return Results.Unauthorized();

    var claims = new List<Claim> { 
        new Claim(ClaimTypes.Name, user.Login),
        new Claim(ClaimTypes.Role, user.Role)
    };
    // создаем объект ClaimsIdentity
    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
    // установка аутентификационных куки
    await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
    return Results.Redirect(returnUrl ?? "/Authorization/AllUsers");
});

app.MapGet("/logout", async (HttpContext context) =>
{
    await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    return Results.Redirect("/");
});

app.Map("/login", [Authorize] () => $"Hello World!");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Authorization}/{action=Authorization}/{id?}");

app.Run();
