using Domain.Entities;
using Infrastructure.Persistence.DataContext;
using Microsoft.EntityFrameworkCore;
using Application.Services.Interfaces;
using Application.Services;
using Domain.Repositories;
using Infrastructure.Repositories;
using Shared.Repositories;
using Infrastructure.UnitOfWork;
using DomainCore.Handlers;
using Domain.Commands.CreateChatRoom;
using Domain.Queries.GetChatRoomsByUser;
using DomainCore.MQ;
using Infrastructure.MQ;
using RabbitMQ.Client;
using Domain.Commands.SendMessage;
using Domain.Queries.GetMessagesByChatRoom;
using Application.Hubs;
using Application.Infrastructure;
using DomainCore.Bot;
using Bot;
using Infrastructure.Helpers;
using DomainCore.Helpers;
using Application.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Services
builder.Services.AddScoped<IChatRoomApplicationService, ChatRoomApplicationService>();
builder.Services.AddScoped<IMessageApplicationService, MessageApplicationService>();

// Unit of Work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Repositories
builder.Services.AddScoped<IChatRoomRepository, ChatRoomRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();

// Command Handlers
builder.Services.AddScoped<ICommandHandler<CreateChatRoomCommand>, CreateChatRoomCommandHandler>();
builder.Services.AddScoped<ICommandHandler<SendMessageCommand>, SendMessageCommandHandler>();

// Query Handlers
builder.Services.AddScoped<IQueryHandler<GetChatRoomsByUserQuery, GetChatRoomsByUserQueryResult>, GetChatRoomsByUserQueryHandler>();
builder.Services.AddScoped<IQueryHandler<GetMessagesByChatRoomQuery, GetMessagesByChatRoomQueryResult>, GetMessagesByChatRoomQueryHandler>();

// MQ
MQConfiguration mqConfiguration = new();
mqConfiguration.HostName = builder.Configuration.GetSection(MQConfiguration.ConfigurationName).GetValue(typeof(string), nameof(mqConfiguration.HostName)).ToString();
int.TryParse(builder.Configuration.GetSection(MQConfiguration.ConfigurationName).GetValue(typeof(string), nameof(mqConfiguration.Port)).ToString(), out int port);
if(port > 0)
    mqConfiguration.Port = port;

builder.Services.AddSingleton<IConnectionFactory, ConnectionFactory>(x => new ConnectionFactory() { HostName = mqConfiguration.HostName, Port = mqConfiguration.Port });
builder.Services.AddSingleton<IProducer, Producer>();
builder.Services.AddSingleton<IConsumer, Consumer>();

// SignalR
builder.Services.AddSignalR();
builder.Services.AddSingleton<MessageHub, MessageHub>();

// Helpers
builder.Services.AddSingleton<ICryptographyHelper, CryptographyHelper>();

// Bot
builder.Services.AddSingleton<IBot, StockCommandBot>();

// Configuration
ChatConfiguration chatConfiguration = new();
chatConfiguration.MaxMessagesPerChat = Convert.ToInt32(builder.Configuration.GetSection(ChatConfiguration.ConfigurationName).GetValue(typeof(int), nameof(chatConfiguration.MaxMessagesPerChat)));
builder.Services.AddSingleton<ChatConfiguration>(chatConfiguration);
// Identity
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

// SignalR Endpoint
app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<MessageHub>(HubManager.MESSAGE_HUB_ENDPOINT);
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
