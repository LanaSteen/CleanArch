using MyApp.Api;
using MyApp.Application.Validators;
using FluentValidation;
using MyApp.Application.Profiles;
using MyApp.Infrastructure.Middleware;
using Microsoft.AspNetCore.Identity;
using MyApp.Core.Entities;
using MyApp.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.Configure<ConnectionStringOptions>(builder.Configuration.GetSection(ConnectionStringOptions.SectionName));
builder.Services.AddAutoMapper(typeof(HotelProfile));
builder.Services.AddAutoMapper(typeof(RoomProfile));
builder.Services.AddAutoMapper(typeof(ManagerProfile));
builder.Services.AddAutoMapper(typeof(GuestProfile));
builder.Services.AddAutoMapper(typeof(ReservationProfile));
builder.Services.AddValidatorsFromAssemblyContaining<CreateHotelCommandValidator>();
builder.Services.AddAppDI(builder.Configuration);
builder.Services.AddIdentity<UserEntity, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequiredUniqueChars = 1;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();  

builder.Services.AddScoped<UserManager<UserEntity>>();
var app = builder.Build();


 

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.MapControllers();

app.Run();
