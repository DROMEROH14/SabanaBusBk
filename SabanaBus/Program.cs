using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SabanaBus.Context;
using SabanaBus.Repositories;
using SabanaBus.Repository;
using SabanaBus.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var conString = builder.Configuration.GetConnectionString("Connection");
builder.Services.AddDbContext<SabanaBusDbContext>(Options => Options.UseSqlServer(conString));

//Inyeccion de dependencias a los repositorios
builder.Services.AddScoped<IAssignmentService, AssignmentRepositories>();
builder.Services.AddScoped<IBusService, BusRepositories>();
builder.Services.AddScoped<INotificationService, NotificationRepositories>();
builder.Services.AddScoped<IPermissionsService, PermissionsRepositories>();
builder.Services.AddScoped<IPermissionsXUserTypesService, PermissionsXUserTypesRepositories>();
builder.Services.AddScoped<IRouteService, RouteRepositories>();
builder.Services.AddScoped<IScheduleService, ScheduleRepositories>();
builder.Services.AddScoped<IUserService, UserRepositories>();
builder.Services.AddScoped<IUserTypeService, UserTypeRepositories>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
 app.UseSwagger();
 app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
