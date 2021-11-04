using System;
using System.Reflection;
using System.Threading.Tasks;

using ExoftOfficeManager.Application.Services;
using ExoftOfficeManager.Application.Validators.Commands.Bookings;
using ExoftOfficeManager.Extensions;
using ExoftOfficeManager.Infrastructure;
using ExoftOfficeManager.Infrastructure.Configuration;
using ExoftOfficeManager.Infrastructure.Identity;

using FluentValidation;

using MediatR;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

using Newtonsoft.Json;

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
            services.AddRepositories();

            services.AddScoped<IEmailService, GmailService>();

            #region MediatR
            services.AddMediatR(Assembly.Load("ExoftOfficeManager.Application"));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            AssemblyScanner.FindValidatorsInAssemblyContaining<AddBookingCommandValidator>()
                .ForEach(result => {
                    services.AddTransient(result.InterfaceType, result.ValidatorType);
                });
            #endregion

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("MyConnection")));

            services.AddIdentity<AppIdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders();

            services.AddControllers()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            );

            services.Configure<EmailConfig>(Configuration.GetSection("Email"));

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

            CreateRoles(app.ApplicationServices).Wait();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static async Task CreateRoles(IServiceProvider serviceProvider)
        {
            //initializing custom roles 
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            //var UserManager = serviceProvider.GetRequiredService<UserManager<AppIdentityUser>>();
            string[] roleNames = { "Admin", "Developer" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    //create the roles and seed them to the database: Question 1
                    roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            ////Here you could create a super user who will maintain the web app
            //var poweruser = new AppIdentityUser
            //{
            //    UserName = Configuration["AppSettings:UserName"],
            //    Email = Configuration["AppSettings:UserEmail"],
            //};

            ////Ensure you have these values in your appsettings.json file
            //string userPWD = Configuration["AppSettings:UserPassword"];
            //var _user = await UserManager.FindByEmailAsync(Configuration["AppSettings:AdminUserEmail"]);

            //if (_user == null)
            //{
            //    var createPowerUser = await UserManager.CreateAsync(poweruser, userPWD);
            //    if (createPowerUser.Succeeded)
            //    {
            //        //here we tie the new user to the role
            //        await UserManager.AddToRoleAsync(poweruser, "Admin");

            //    }
            //}
        }
    }
}
