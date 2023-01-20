using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using System.Security.Claims;
using System;
using Microsoft.EntityFrameworkCore;
using webApp.Models;

var builder = WebApplication.CreateBuilder(args);

// ������� MVC
builder.Services.AddControllersWithViews();

// ����������� � ��
string connection = "Host=localhost;Port=5432;Database=users;Username=postgres;Password=123456";
builder.Services.AddDbContext<UsersContext>(options => options.UseNpgsql(connection));

// ����������� �������������� � ������� ����
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => options.LoginPath = "/");
builder.Services.AddAuthorization();

var app = builder.Build();

app.UseAuthentication();   // ���������� middleware �������������� 
app.UseAuthorization();   // ���������� middleware ����������� 

app.MapPost("/", async (string? returnUrl, HttpContext context, UsersContext db) =>
{
    // �������� �� ����� login � ������
    var form = context.Request.Form;
    // ���� login �/��� ������ �� �����������, �������� ��������� ��� ������ 400
    if (!form.ContainsKey("login") || !form.ContainsKey("password"))
        return Results.BadRequest("����� �/��� ������ �� �����������");

    string login = form["login"];
    string password = form["password"];

    // ������� ������������ 
    User? user = db.Users.FirstOrDefault(u => u.Login == login && u.Password == password);

    // ���� ������������ �� ������, ���������� ��������� ��� 401
    if (user is null) return Results.Unauthorized();

    var claims = new List<Claim> { 
        new Claim(ClaimTypes.Name, user.Login),
        new Claim(ClaimTypes.Role, user.Role)
    };
    // ������� ������ ClaimsIdentity
    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
    // ��������� ������������������ ����
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
