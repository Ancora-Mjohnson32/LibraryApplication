using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryApplication.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using LibraryApplication.Security;

namespace LibraryApplication
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
            services.AddDbContextPool<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("BookDBConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 7;
                options.Password.RequiredUniqueChars = 2;
            }).AddEntityFrameworkStores<AppDbContext>();

            services.AddMvc(options => {
                var policy = new AuthorizationPolicyBuilder()
                            .RequireAuthenticatedUser()
                            .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            }).AddXmlDataContractSerializerFormatters();

            services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Administration/AccessDenied");
            });

            services.AddAuthorization(options =>
            {
                //need to declare permission on methods per controller
                options.AddPolicy("DeleteRolePolicy",
                    policy => policy.RequireAssertion(context =>
                    context.User.IsInRole("Admin") &&
                    context.User.HasClaim(claim => claim.Type == "Delete Role" && claim.Value == "true") ||
                    context.User.IsInRole("Super Admin")
                    ));

                options.AddPolicy("EditRolePolicy",
                    policy => policy.AddRequirements( new ManageAdminRolesAndClaimsRequirement()));
                
                options.AddPolicy("AdminRolePolicy",
                    policy => policy.RequireRole("Admin", "Super Admin"));

            });

            services.AddControllersWithViews();
            //Sets up an instance of MockBookRepository for the HomeController to use. (5)
            //(DO NOT IMPLEMENT THIS FUNCTION ON A CONTROLLER)
            services.AddScoped<IBookRepository, SQLBookRepository>();

            services.AddSingleton<IAuthorizationHandler,
                   CanEditOnlyOtherAdminRolesAndClaimsHandler>();

            services.AddSingleton<IAuthorizationHandler,
                    SuperAdminHandler>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
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
            });
        }
    }
}
