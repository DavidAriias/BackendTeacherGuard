using TeachersGuardAPI.App.UseCases.Attendace;
using TeachersGuardAPI.App.UseCases.PlaceUseCase;
using TeachersGuardAPI.App.UseCases.Schedule;
using TeachersGuardAPI.App.UseCases.User;
using TeachersGuardAPI.Config.constants;
using TeachersGuardAPI.Domain.Repositories;
using TeachersGuardAPI.Infraestructure.Persistence.Context;
using TeachersGuardAPI.Infraestructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("https://davidariias.github.io")
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});


builder.Services.Configure<MongoConfig>(builder.Configuration.GetSection("MongoSettings"));

builder.Services.AddSingleton<MongoContext>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAttendanceRepository, AttendanceRepository>();
builder.Services.AddScoped<IScheduleRepository, ScheduleRepository>();
builder.Services.AddScoped<IPlaceRepository,PlaceRepository>();

builder.Services.AddScoped<UserUseCase>();
builder.Services.AddScoped<ScheduleUseCase>();
builder.Services.AddScoped<PlaceUseCase>();
builder.Services.AddScoped<AttendanceUseCase>();


builder.Services.AddLogging();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("AllowOrigin");
app.UseCors("AllowSpecificOrigin");


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
