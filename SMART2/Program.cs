using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SMART2.Domain;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var conStr = new SqlConnectionStringBuilder(
        builder.Configuration.GetConnectionString("SMART2Database")).ToString();
builder.Services.AddDbContextFactory<DomainDbContext>(
    options => options.UseSqlServer(conStr));

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
