using ExoftOfficeManager.Application;
using ExoftOfficeManager.Application.Services;
using ExoftOfficeManager.Application.Services.Interfaces;
using ExoftOfficeManager.Domain.Entities;
using ExoftOfficeManager.Infrastructure;
using ExoftOfficeManager.Infrastructure.Repositories.EfCore;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
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
            services.AddScoped<IMeetingService, MeetingService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IWorkPlaceService, WorkPlaceService>();

            services.AddScoped<IRepository<Meeting>, EfCoreMeetingRepository>();
            services.AddScoped<IRepository<WorkPlace>, EfCoreWorkPlaceRepository>();
            services.AddScoped<IRepository<Booking>, EfCoreBookingRepository>();
            services.AddScoped<IRepository<User>, EfCoreUserRepository>();

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("MyConnection")));

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
