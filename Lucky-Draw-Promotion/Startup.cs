using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Lucky_Draw_Promotion.Data;
using Lucky_Draw_Promotion.Models;
using Lucky_Draw_Promotion.Models.Account;
using System.IO;

namespace Lucky_Draw_Promotion
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
            services.AddControllersWithViews();
            services.AddSession();

           

            services.AddMvc();
            services.AddAutoMapper(typeof(Startup));
            services.AddRazorPages();
            services.AddAuthentication();

            services.AddDbContext<DataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, IdentityRole>(opt =>
            {
                opt.Password.RequiredLength = 6;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireUppercase = true;
                opt.Password.RequireNonAlphanumeric = false;
                opt.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<DataContext>().AddDefaultTokenProviders();
            services.AddAuthorization(option =>
            {
                option.AddPolicy("RequireAdministratorRole", policy => policy.RequireRole("SuperAdmin,Administrator"));
            });          
            services.Configure<DataProtectionTokenProviderOptions>(opt =>
                  opt.TokenLifespan = TimeSpan.FromMinutes(3));
            services.AddScoped<SmtpClient>((serviceProvider) =>
            {
                var config = serviceProvider.GetRequiredService<IConfiguration>();
                return new SmtpClient()
                {
                    Host = config.GetValue<String>("Email:Smtp:Host"),
                    Port = config.GetValue<int>("Email:Smtp:Port"),
                    Credentials = new NetworkCredential(
                            config.GetValue<String>("Email:Smtp:Username"),
                            config.GetValue<String>("Email:Smtp:Password")
                        )
                };
            });
           

            services.Configure<GoogleCaptchaConfig>(Configuration.GetSection("GoogleRecaptcha"));

        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider services)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
              name: "MyAreaAdmin",
              pattern: "{area:exists}/{controller=HomeAdmin}/{action=Index}/{id?}");


                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Login}/{id?}");
            });
        }
    }
}
