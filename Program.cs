using CommunityWebsite_Lexicon_Project.Data;
using CommunityWebsite_Lexicon_Project.Interfaces;
using CommunityWebsite_Lexicon_Project.Models.BaseModels;
using CommunityWebsite_Lexicon_Project.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CommunityWebsite_Lexicon_Project
{
    public class Program
    {
        private static readonly RoleManager<IdentityRole> roleManager;
        private static readonly UserManager<Account> userManager;

        internal class DbInitializer
        {
            internal static async Task Initialize(
                ApplicationDbContext context, UserManager<Account> userManager
                )
            {
                context.Database.EnsureCreated(); //If not using EF migrations

                try
                {
                    RoleStore<IdentityRole> roleStore = new RoleStore<IdentityRole>(context);
                    var createAdmin = await roleStore.CreateAsync(
                        new IdentityRole()
                        {
                            Name = "Admin",
                            NormalizedName = "ADMIN",
                        });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                Account adm = await userManager.FindByNameAsync("Admin");

                try
                {
                    IdentityResult result = await userManager.AddToRoleAsync(adm, "Admin");

                    if (result.Succeeded)
                    {
                        Console.WriteLine(adm.UserName + " has been added as an Admin.");
                    }
                    else
                    {
                        Console.WriteLine(adm.UserName + " could not be added as an Admin.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                context.Database.Migrate();
            }
        }

        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddIdentity<Account, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Add services to the container.
            //builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
                "Server=(localdb)\\MSSQLLocalDB;Database=CommunityWebsite_Database;Trusted_Connection=True;MultipleActiveResultSets=true"
                ));
            builder.Services.AddScoped<IAccountRepository, AccountRepository>();
            builder.Services.AddScoped<IPostRepository, PostRepository>();
            builder.Services.AddScoped<IEventRepository, EventRepository>();
            builder.Services.AddScoped<IForumThreadRepository, ForumThreadRepository>();

            
            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 10;
                options.Password.RequiredUniqueChars = 1;

                options.User.RequireUniqueEmail = true;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            //app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            //app.MapRazorPages();

            //new WebHostBuilder().UseContentRoot(Directory.GetCurrentDirectory());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "admin",
                    pattern: "admin/{controller=Admin}/{action=Index}/{id?}",
                    defaults: new { controller = "Admin", action = "Index" });

                endpoints.MapControllerRoute(
                    name: "account",
                    pattern: "account/{controller=Account}/{action=Index}/{id?}",
                    defaults: new { controller = "Account", action = "Index" });

                endpoints.MapControllerRoute(
                    name: "role",
                    pattern: "role/{controller=Role}/{action=Index}/{id?}",
                    defaults: new { controller = "Role", action = "Index" });

                endpoints.MapControllerRoute(
                    name: "news",
                    pattern: "news/{controller=NewsFeed}/{action=Index}/{id?}",
                    defaults: new { controller = "NewsFeed", action = "Index" });
            });

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                UserManager<Account> userManager = services.GetService<UserManager<Account>>();
                var context = services.GetRequiredService<ApplicationDbContext>();
                await DbInitializer.Initialize(context, userManager);
            }

            await app.RunAsync();
        }
    }
}