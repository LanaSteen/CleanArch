using MyApp.Api;
using MyApp.Application.Validators;
using FluentValidation;
using MyApp.Application.Profiles;
using Microsoft.AspNetCore.Identity;
using MyApp.Core.Entities;
using MyApp.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using MyApp.Core.Interfaces;
using MyApp.Infrastructure.Repositories;
using FluentValidation.AspNetCore;
using MyApp.Api.ServicsEmail;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Hotel Management System", 
        Version = "v1",
        Description = "API for managing hotels and their reservations",
        Contact = new OpenApiContact
        {
            Name = "Lana Steen",
            Email = "L4n.Steen@gmail.com"
        }
    });
    // JWT
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddAutoMapper(typeof(HotelProfile));
builder.Services.AddAutoMapper(typeof(RoomProfile));
builder.Services.AddAutoMapper(typeof(ManagerProfile));
builder.Services.AddAutoMapper(typeof(GuestProfile));
builder.Services.AddAutoMapper(typeof(ReservationProfile));
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateHotelCommandValidator>();
builder.Services.AddAppDI(builder.Configuration);
builder.Services.AddTransient<IEmailService, EmailService>();
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

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]))
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    options.AddPolicy("GuestOnly", policy => policy.RequireRole("Guest"));
    options.AddPolicy("ManagerOnly", policy => policy.RequireRole("Manager"));
});
builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateHotelCommandValidator>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseStaticFiles();
    app.UseSwaggerUI(c =>
    {
        
        c.DocumentTitle = "Hotel Management System";
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hotel Management System");
        c.InjectStylesheet("/swagger-ui/custom-swagger.css");
    });

}
app.UseStatusCodePages(async context =>
{
    if (context.HttpContext.Response.StatusCode == StatusCodes.Status401Unauthorized ||
        context.HttpContext.Response.StatusCode == StatusCodes.Status403Forbidden)
    {
        context.HttpContext.Response.ContentType = "application/json";
        await context.HttpContext.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(new
        {
            message = "Not Authorized or No Permissions"
        }));
    }
});

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseMiddleware<ExceptionHandlingMiddleware>();
}

app.MapControllers();

app.Run();