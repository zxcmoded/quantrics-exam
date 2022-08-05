using AssetService.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.WebHost.UseUrls("http://localhost:5266");
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Services.AddTransient<IAssetRepository, AssetRepository>();

builder.Services.AddDbContext<examContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("exam"));
});

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

app.UseCors(option =>
{
    option.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
});

app.Run();
