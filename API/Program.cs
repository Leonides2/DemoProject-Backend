using Domain.Repositories;
using Features.HubFolder;
using Features.UserFolder.Commands;
using Infrastructure.Persistance;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.4

var origin = Environment.GetEnvironmentVariable("origin") ?? "http://localhost:5173";

Console.WriteLine(origin);
builder.Services.AddCors(
    options => {
        options.AddPolicy("All", builder => 
            builder
            .AllowAnyHeader()
            .AllowAnyMethod()
            .WithOrigins(origin)
            .AllowCredentials()
        );
    }
);

var connectionString = Environment.GetEnvironmentVariable("DefaultConnection") ?? builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(
        connectionString
        //builder.Configuration.GetConnectionString("DefaultConnection")
        ));

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddMediatR(ctg =>
{
    ctg.RegisterServicesFromAssembly(typeof(CreateUserCommandHandler).Assembly);
});


builder.Services.AddSignalR();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var port = Environment.GetEnvironmentVariable("PORT") ?? "5178";
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
