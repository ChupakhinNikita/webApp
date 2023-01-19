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


var app = builder.Build();

/*// Строка подключения к БД из файла конфигурации
string connection = builder.Configuration.GetConnectionString("DefaultConnection");

// Добавление контекста UsersContext в качестве сервиса в приложение
builder.Services.AddDbContext<UsersContext>(options => options.UseNpgsql(connection));

// получение данных
// app.MapGet("/", (UsersContext db) => db.Users.ToList());*/


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
    pattern: "{controller=Authorization}/{action=AllUsers}/{id?}");

app.Run();
