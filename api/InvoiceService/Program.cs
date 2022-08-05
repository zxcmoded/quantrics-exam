using InvoiceService.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.WebHost.UseUrls("http://localhost:5298");
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Services.AddSignalR();
builder.Services.AddTransient<IInvoiceRepository, InvoiceRepository>();

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

app.MapHub<InvoiceHub>("/hub");

app.UseCors(option =>
{
    option.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins("http://localhost:8080");
});

app.Run();
