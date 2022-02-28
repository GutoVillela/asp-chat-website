using Domain.Entities;
using Infrastructure.Persistence.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Application.Services.Interfaces;
using Application.Services;
using Domain.Repositories;
using Infrastructure.Repositories;
using Shared.Repositories;
using Infrastructure.UnitOfWork;
using Shared.Handlers;
using Domain.Commands.CreateChatRoom;
using Domain.Queries.GetChatRoomsByUser;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Services
builder.Services.AddScoped<IChatRoomApplicationService, ChatRoomApplicationService>();

// Unit of Work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Repositories
builder.Services.AddScoped<IChatRoomRepository, ChatRoomRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Command Handlers
builder.Services.AddScoped<ICommandHandler<CreateChatRoomCommand>, CreateChatRoomCommandHandler>();

// Query Handlers
builder.Services.AddScoped<IQueryHandler<GetChatRoomsByUserQuery, GetChatRoomsByUserQueryResult>, GetChatRoomsByUserQueryHandler>();

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
