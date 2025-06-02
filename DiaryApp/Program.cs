using DiaryApp;
using DiaryApp.Controllers;
using DiaryAppDbContext; // ? Add this to use KeeplyDbContext
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("Policy1",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000",
                                "http://192.168.56.1:3000",
                                "http://localhost:18842",
                                "http://192.168.56.1:18842")
                  .AllowAnyMethod()
                  .AllowAnyOrigin()
                  .AllowAnyHeader();
        });
});

builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ? Add your DbContext with connection string from appsettings.json
builder.Services.AddDbContext<KeeplyDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DiaryAppDb"),
        ServerVersion.Parse("8.0.28-mysql")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors("Policy1");
app.MapControllers();
app.Run();
