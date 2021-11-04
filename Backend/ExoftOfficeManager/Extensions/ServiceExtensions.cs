using ExoftOfficeManager.Application.Services.Repositories;
using ExoftOfficeManager.Infrastructure.Repositories;

using Microsoft.Extensions.DependencyInjection;

namespace ExoftOfficeManager.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
            => services.AddScoped<IMeetingRepository, MeetingRepository>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IWorkPlaceRepository, WorkPlaceRepository>()
                .AddScoped<IBookingRepository, BookingRepository>();
    }
}
