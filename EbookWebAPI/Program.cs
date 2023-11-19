using EbookWebAPI;
using EbookWebAPI.DAL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add Configuration EntityFrameWork DataContext
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("eBookConnection")).EnableSensitiveDataLogging());

//Add Automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//Inject class DAL
builder.Services.AddScoped<IEbook, EBookDAL>();
builder.Services.AddScoped<IEMail,EmailDAL>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

app.UseAuthorization();

app.MapControllers();

app.Run();
