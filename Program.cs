using Microsoft.EntityFrameworkCore;
using Data;
using Features.Clients;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("db")));

builder.Services.AddApplicationServices();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();