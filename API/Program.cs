using System.Text;
using Domain.Entities;
using Domain.Repositories;
using Features.HubFolder;
using Features.Interfaces;
using Features.UserFolder.Commands;
using Infrastructure.Persistance;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Shared.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.4

var origin = Environment.GetEnvironmentVariable("origin") ?? "http://localhost:5173";
var secret = builder.Configuration["JwtSettings:Key"] ?? "thisadefaultsecret";
var connectionString = Environment.GetEnvironmentVariable("DefaultConnection") ?? builder.Configuration.GetConnectionString("DefaultConnection");
var port = Environment.GetEnvironmentVariable("PORT") ?? "5178";


Console.WriteLine(secret);

builder.Services.AddCors(
    options =>
    {
        options.AddPolicy("All", builder =>
            builder
            .AllowAnyHeader()
            .AllowAnyMethod()
            .WithOrigins(origin)
            .AllowCredentials()
        );
    }
);


builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));


builder.Services.AddAuthentication(ctg =>
{
    ctg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    ctg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    ctg.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}
).AddJwtBearer(config =>
{
    config.RequireHttpsMetadata = false;
    config.SaveToken = true;
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero,
        ValidateIssuer = false,
        ValidateAudience = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret))
    };
});

// Services

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

//Object injection
builder.Services.Configure<JwtSettings>
(
    builder.Configuration.GetSection("JwtSettings")
    
);
builder.Services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();

builder.Services.AddScoped<ISecurityService, SecurityService>();

builder.Services.AddMediatR(ctg =>
{
    ctg.RegisterServicesFromAssembly(typeof(CreateUserCommandHandler).Assembly);
});


builder.Services.AddSignalR();

builder.Services.AddMemoryCache();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.WebHost.UseUrls($"http://*:{port}");


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("All");

app.MapControllers();

app.MapHub<UserHub>("/SignalR");

app.Run();
