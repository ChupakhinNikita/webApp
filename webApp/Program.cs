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
    .AddCookie(options =>
    {
        options.LoginPath = "/Authorization/Authorization";
        options.AccessDeniedPath = "/Authorization/Authorization";
    });

// ����������� �����������
builder.Services.AddAuthorization(opts => {

    opts.AddPolicy("OnlyForAdmin", policy => {
        policy.RequireClaim(ClaimTypes.Role, "�������������");
    });
    opts.AddPolicy("OnlyForTeacher", policy => {
        policy.RequireClaim(ClaimTypes.Role, "�������������");
    });
    opts.AddPolicy("OnlyForStudent", policy => {
        policy.RequireClaim(ClaimTypes.Role, "�������");
    });
});

var app = builder.Build();

app.MapPost("/", async (string? returnUrl, HttpContext context, UsersContext db) =>
{
    // �������� �� ����� login � ������
    var form = context.Request.Form;

    string login = form["login"];
    string password = form["Password"];

    // ������� ������������ 
    User? user = db.Users.FirstOrDefault(u => u.Login == login && u.Password == password);

    /*����������*/
    // ���� ������������ �� ������, ���������� ��������� ��� 401
    if (user is null) return Results.Unauthorized();
    /*����������*/

    var claims = new List<Claim> { 
        new Claim(ClaimTypes.Name, user.Login),
        new Claim(ClaimTypes.Role, user.Role)
    };
    // ������� ������ ClaimsIdentity
    var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
    await context.SignInAsync(claimsPrincipal); // ��������� ������������������ ����

    if (user.Role == "�������������")
    {
        return Results.Redirect(returnUrl ?? "/Authorization/Authorization/admin");
    }
    else if (user.Role == "�������")
    {
        return Results.Redirect(returnUrl ?? "/Authorization/Authorization/student");
    }
    else if (user.Role == "�������������")
    {
        return Results.Redirect(returnUrl ?? "/Authorization/Authorization/teacher");
    }
    else 
    {
        return Results.Redirect(returnUrl ?? $"/Authorization/Authorization/login");
    }
});

// ������ ������ ��� ���������
app.Map("/Authorization/Authorization/student", [Authorize(Policy = "OnlyForStudent")] (HttpContext context) =>
{
    var login = context.User.FindFirst(ClaimTypes.Name);
    var role = context.User.FindFirst(ClaimTypes.Role);
    return $"�����: {login?.Value}\n����: {role?.Value}\nYou are student";
});
// ������ ������ ��� ��������������
app.Map("/Authorization/Authorization/admin", [Authorize(Policy = "OnlyForAdmin")] (HttpContext context) =>
{
    var login = context.User.FindFirst(ClaimTypes.Name);
    var role = context.User.FindFirst(ClaimTypes.Role);
    return $"�����: {login?.Value}\n����: {role?.Value}\nYou are Admin";
});

// ������ ������ ��� ��������������
app.Map("/Authorization/Authorization/teacher", [Authorize(Policy = "OnlyForTeacher")] (HttpContext context) =>
{
    var login = context.User.FindFirst(ClaimTypes.Name);
    var role = context.User.FindFirst(ClaimTypes.Role);
    return $"�����: {login?.Value}\n����: {role?.Value}\nYou are Teacher";
});

app.Map("/Authorization/Authorization/login", [Authorize] (HttpContext context) =>
{
    var login = context.User.FindFirst(ClaimTypes.Name);
    var role = context.User.FindFirst(ClaimTypes.Role);
    return $"�����: {login?.Value}\n����: {role?.Value}";
});

app.MapGet("/logout", async (HttpContext context) =>
{
    await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    return "������ �������";
});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

/*app.UseRouting();*/

app.UseAuthentication();   // ���������� middleware �������������� 
app.UseAuthorization();   // ���������� middleware ����������� 

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Authorization}/{action=Authorization}/{id?}");

app.Run();
