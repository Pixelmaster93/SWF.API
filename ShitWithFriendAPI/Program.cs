using Microsoft.EntityFrameworkCore;
using Serilog;
using ShitWithFriendAPI.DBContext;
using ShitWithFriendAPI.Repositories.Int;
using ShitWithFriendAPI.Repositories.Impl;
using ShitWithFriendAPI.Services.Int;
using ShitWithFriendAPI.Services.Impl;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Warning()
    .WriteTo.Console()
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDbContext<SWFContext>(DbContextOptions => 
DbContextOptions.UseSqlite(builder.Configuration.GetConnectionString("SWFDbConnectionString")));

// Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPoopRepository, PoopRepository>();
builder.Services.AddScoped<IGroupRepository, GroupRepository>();
builder.Services.AddScoped<IGameRepository, GameRepository>();
builder.Services.AddScoped<IHighScoreRepository, HighScoreRepository>();

// Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPoopService, PoopService>();
builder.Services.AddScoped<IGroupService, GroupService>();
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IHighScoreService, HighScoreService>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
