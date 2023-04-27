using DiaryApp;
using DiaryApp.Controllers;

//var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    //http://localhost:3000
    //On Your Network: http://192.168.56.1:3000

    options.AddPolicy("Policy1",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000",
                                "http://192.168.56.1:3000",
                                "http://localhost:18842",
                                "http://192.168.56.1:18842").AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader();
        });
});

builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
