using LastTodoApp.DataContext.Data;
using LastTodoApp.DataContext.Services;
using LastTodoApp.Domain.Entities;
using LastTodoApp.Web.Configuration;
using LastTodoApp.Web.Repositories;
using LastTodoApp.Web.Repositories.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services
    .AddDefaultControllers()
    .AddSwaggerGen()
    .AddIdentity()
    .AddServicesAndRepositories()
    .AddAuthAndAuthorize();

var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json")
    .Build();

var connectionString = configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext(connectionString);

var app = builder.Build();

using (var serviceScope = app.Services.CreateScope())
{
    await Seed.SeedUsersAndRolesAsync(serviceScope.ServiceProvider);
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseStatusCodePagesWithReExecute("/Home/Error/{0}");
    app.UseHsts();
}

app.UseMiddleware<SwaggerAuthorizationMiddleware>();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();




