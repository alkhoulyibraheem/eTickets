using eTickets.data;
using eTickets.data.Models;
using eTickets.infrastructure.AutoMapper;
using eTickets.infrastructure.Services;
using eTickets.infrastructure.Services.Actor;
using eTickets.infrastructure.Services.Category;
using eTickets.infrastructure.Services.Cinema;
using eTickets.infrastructure.Services.Customers;
using eTickets.infrastructure.Services.Director;
using eTickets.infrastructure.Services.Home;
using eTickets.infrastructure.Services.Movie;
using eTickets.infrastructure.Services.Users;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddAutoMapper(typeof(MapperProfile).Assembly);
            services.AddTransient<IFileService, FileService>();
            services.AddTransient<IUserServices, UserServices>();
            services.AddTransient<ICategoryServices , CategoryServices>();
            services.AddTransient<ICinemaServices, CinemaServices>();
            services.AddTransient<IDirectorServices, DirectorServices>();
            services.AddTransient<IActorServices, ActorServices>();
            services.AddTransient<IMovieServices, MovieServices>();
			services.AddTransient<ICustomerServices, CustomerServices>();
			services.AddTransient<IHomeServices, HomeServices>();
			services.AddTransient<IEmailService, EmailService>();



			services.AddIdentity<User , IdentityRole>(config =>
			{
				config.User.RequireUniqueEmail = true;
				config.Password.RequireDigit = false;
				config.Password.RequiredLength = 6;
				config.Password.RequireLowercase = false;
				config.Password.RequireNonAlphanumeric = false;
				config.Password.RequireUppercase = false;
				config.SignIn.RequireConfirmedEmail = false;
			})
                .AddEntityFrameworkStores<ApplicationDbContext>()
				.AddDefaultUI().AddDefaultTokenProviders();
            services.AddControllersWithViews();
            services.AddRazorPages();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

			

			app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
