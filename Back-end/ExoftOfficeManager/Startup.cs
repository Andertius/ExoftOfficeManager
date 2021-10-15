using ExoftOfficeManager.Business.Services;
using ExoftOfficeManager.Business.Services.Interfaces;
using ExoftOfficeManager.DataAccess;
using ExoftOfficeManager.DataAccess.Repositories;
using ExoftOfficeManager.DataAccess.Repositories.Mocked;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace ExoftOfficeManager
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddSingleton<IRepository<Booking>, MockedBookingRepository>()
            //    .AddSingleton<IRepository<WorkPlace>, MockedWorkPlaceRepository>();

            services.AddScoped<IMeetingService, MeetingService>()
                .AddSingleton<IRepository<Meeting>, MockedMeetingRepository>();

            services.AddScoped<IWorkPlaceService, WorkPlaceService>()
                .AddSingleton<IRepository<WorkPlace>, MockedWorkPlaceRepository>()
                .AddSingleton<IRepository<Booking>, MockedBookingRepository>();

            services.AddScoped<IUserService, UserService>()
                .AddSingleton<IRepository<User>, MockedUserRepository>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ExoftOfficeManager", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ExoftOfficeManager v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
