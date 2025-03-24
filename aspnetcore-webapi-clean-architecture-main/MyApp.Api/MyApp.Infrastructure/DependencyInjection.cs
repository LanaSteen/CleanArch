using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MyApp.Core.Interfaces;
using MyApp.Core.Options;
using MyApp.Infrastructure.Data;
using MyApp.Infrastructure.Repositories;
using MyApp.Infrastructure.Services;

namespace MyApp.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDI(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>((provider, options) =>
            {
                options.UseSqlServer(provider.GetRequiredService<IOptionsSnapshot<ConnectionStringOptions>>().Value.DefaultConnection);
            });

            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IHotelRepository, HotelRepository>();
            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<IManagerRepository, ManagerRepository>();
            services.AddScoped<IGuestRepository, GuestRepository>();

            services.AddScoped<IReservationRepository, ReservationRepository>();

            services.AddScoped<IPasswordHasher, PasswordHasher>();

            services.AddScoped<IExternalVendorRepository, ExternalVendorRepository>();
   

            services.AddHttpClient<IJokeHttpClientService, JokeHttpClientService>(option =>
            {
                option.BaseAddress = new Uri("https://official-joke-api.appspot.com/");
               //https://official-joke-api.appspot.com/jokes/programming/random
            });

            return services;
        }
    }
}
