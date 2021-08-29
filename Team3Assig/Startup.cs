using Team3Assig.Data;
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
using Team3Assig.Controllers;
using Team3Assig.Services;


namespace Team3Assig
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
            services.AddSignalR();
            services.AddSingleton<IBroadcastService, BroadcastService>();
            services.AddSingleton<IDiplomataControllerSettings, DiplomataControllerSettings>();

            string databaseURL = Environment.GetEnvironmentVariable("DATABASE_URL");
            databaseURL = databaseURL != null ? ConvertHerokuStringToASPString(databaseURL) : Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(databaseURL));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews();
            EnsureUsersCreated(services).Wait();
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

                endpoints.MapHub<MessageHub>("/messagehub");
            });
        }

        private static async Task EnsureUsersCreated(IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            var adminUser = await EnsureUserCreated(userManager, "radu.sechei@principal33.com", "Parola1@");
            var operatorUser = await EnsureUserCreated(userManager, "radu.sechei2@principal33.com", "Parola2@");

            var adminRole = await EnsureRoleCreated(serviceProvider, "Administrator");
            var operatorRole = await EnsureRoleCreated(serviceProvider, "Operator");

            await userManager.AddToRoleAsync(adminUser, adminRole.Name);
            await userManager.AddToRoleAsync(operatorUser, operatorRole.Name);

            var users = await userManager.Users.ToListAsync();
            Console.WriteLine($"There are {users.Count} users now.");
        }

        private static async Task<IdentityUser> EnsureUserCreated(UserManager<IdentityUser> userManager, string name, string password)
        {
            var adminUser = await userManager.FindByNameAsync(name);
            if (adminUser == null)
            {
                await userManager.CreateAsync(new IdentityUser(name));
                adminUser = await userManager.FindByNameAsync(name);
                var tokenChangePassword = await userManager.GeneratePasswordResetTokenAsync(adminUser);

                var result = await userManager.ResetPasswordAsync(adminUser, tokenChangePassword, password);

                if (!adminUser.EmailConfirmed)
                {
                    var token = await userManager.GenerateEmailConfirmationTokenAsync(adminUser);
                    await userManager.ConfirmEmailAsync(adminUser, token);
                }
            }

            return adminUser;
        }

        private static async Task<IdentityRole> EnsureRoleCreated(ServiceProvider serviceProvider, string roleName)
        {
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();
            var adminRole = await roleManager.FindByNameAsync(roleName);
            if (adminRole == null)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
                adminRole = await roleManager.FindByNameAsync(roleName);
            }

            return adminRole;
        }

        public static string ConvertHerokuStringToASPString(string herokuConnectionString)
        {
            var databaseUri = new Uri(herokuConnectionString);
            string[] userInfo = databaseUri.UserInfo.Split(':');

            int port = databaseUri.Port;
            string host = databaseUri.Host;
            string userId = userInfo[0];
            string password = userInfo[1];
            string database = databaseUri.AbsolutePath[1..];

            string result = $"Host={host};Port={port};Database={database};User Id={userId};Password={password};Pooling=true;SSL Mode=Require;TrustServerCertificate=True;Include Error Detail=True";
            return result;
        }
    }
}