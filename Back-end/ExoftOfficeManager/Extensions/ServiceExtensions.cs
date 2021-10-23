using ExoftOfficeManager.Application;
using ExoftOfficeManager.Application.CommandHandlers;
using ExoftOfficeManager.Application.CommandHandlers.Interfaces;
using ExoftOfficeManager.Application.QueryHandlers;
using ExoftOfficeManager.Application.QueryHandlers.Interfaces;
using ExoftOfficeManager.Application.Services;
using ExoftOfficeManager.Application.Services.Interfaces;
using ExoftOfficeManager.Domain.Entities;
using ExoftOfficeManager.Infrastructure.Repositories.EfCore;

using Microsoft.Extensions.DependencyInjection;

namespace ExoftOfficeManager.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
            => services.AddScoped<IMeetingService, MeetingService>()
                .AddScoped<IUserService, UserService>()
                .AddScoped<IWorkPlaceService, WorkPlaceService>()
                .AddScoped<IBookingService, BookingService>();

        public static IServiceCollection AddCommandHandlers(this IServiceCollection services)
            => services.AddScoped<IBookingCommandHandler, BookingCommandHandler>()
                .AddScoped<IMeetingCommandHandler, MeetingCommandHandler>()
                .AddScoped<IUserCommandHandler, UserCommandHandler>()
                .AddScoped<IWorkPlaceCommandHandler, WorkPlaceCommandHandler>();

        public static IServiceCollection AddQueryHandlers(this IServiceCollection services)
            => services.AddScoped<IBookingQueryHandler, BookingQueryHandler>()
                .AddScoped<IMeetingQueryHandler, MeetingQueryHandler>()
                .AddScoped<IUserQueryHandler, UserQueryHandler>()
                .AddScoped<IWorkPlaceQueryHandler, WorkPlaceQueryHandler>();

        public static IServiceCollection AddRepositories(this IServiceCollection services)
            => services.AddScoped<IRepository<Meeting>, EfCoreMeetingRepository>()
                .AddScoped<IRepository<WorkPlace>, EfCoreWorkPlaceRepository>()
                .AddScoped<IRepository<Booking>, EfCoreBookingRepository>()
                .AddScoped<IRepository<User>, EfCoreUserRepository>();
    }
}
