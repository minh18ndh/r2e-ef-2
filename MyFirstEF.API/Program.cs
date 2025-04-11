using MyFirstEF.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using MyFirstEF.Infrastructure;
using MyFirstEF.Application.MappingProfiles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddDbContext<MyFirstEFDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();