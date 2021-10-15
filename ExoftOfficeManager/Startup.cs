using ExoftOfficeManager.Business.Services;
using ExoftOfficeManager.Business.Services.Interfaces;
using ExoftOfficeManager.DataAccess;
using ExoftOfficeManager.DataAccess.Repositories;

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
            services.AddSingleton<IMeetingService, MeetingService>()
                .AddSingleton<IRepository<Meeting>, MockedMeetingRepository>();

            services.AddSingleton<IWorkPlaceService, WorkPlaceService>()
                .AddSingleton<IRepository<WorkPlace>, MockedWorkPlaceRepository>()
                .AddSingleton<IRepository<Booking>, MockedBookingRepository>();

            services.AddSingleton<IUserService, UserService>()
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
