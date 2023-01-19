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


var app = builder.Build();

/*// ������ ����������� � �� �� ����� ������������
string connection = builder.Configuration.GetConnectionString("DefaultConnection");

// ���������� ��������� UsersContext � �������� ������� � ����������
builder.Services.AddDbContext<UsersContext>(options => options.UseNpgsql(connection));

// ��������� ������
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
