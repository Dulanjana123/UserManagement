using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagemnt.Data;
using UserManagemnt.Repositories;

namespace UserManagemnt
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        //Dependancy injection is use to add services to application as dependancies
        public void ConfigureServices(IServiceCollection services)
        {
            //adding razorpages to application as a dependancy
            services.AddRazorPages();

            //add controllers to application as a dependancy
            services.AddControllers();

            //Inject DbContext 
            services.AddDbContext<PMSDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("PmsDbConnectionString")));

            //Inject Authentication DbContext
            services.AddDbContext<AuthDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("AuthDbConnectionString")));

            //Use identity and use AuthDbContext to identity
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AuthDbContext>();

            //Configure Identity user password
            services.Configure<IdentityOptions>(options =>
            {
                //Default password setting
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
            });

            //redirect user to the loggin page
            services.ConfigureApplicationCookie(opttions =>
            {
                opttions.LoginPath = "/Login";
                opttions.AccessDeniedPath = "/AccessDenied";
            });


            //Inject Repositories so that we can use repository inside any other web page.
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IImageRepository, ImageRepositoryCloudinary>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.

        //we need to configure the Middleware components within the Configure() method
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //Common exception handling middleware
                app.UseExceptionHandler("/Error");
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
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });
        }
    }
}
