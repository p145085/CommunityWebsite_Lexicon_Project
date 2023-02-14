using CommunityWebsite_Lexicon_Project.Data;
using CommunityWebsite_Lexicon_Project.Interfaces;
using CommunityWebsite_Lexicon_Project.Models.BaseModels;
using CommunityWebsite_Lexicon_Project.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace CommunityWebsite_Lexicon_Project
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddIdentity<Account, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders()
                .AddRoles<IdentityRole>();

            // Add services to the container.
            //builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
                "Server=(localdb)\\MSSQLLocalDB;Database=CommunityWebsite_Database;Trusted_Connection=True;MultipleActiveResultSets=true"
                ));

            //builder.Services.AddScoped<IRoleStore<IdentityRole>, RoleStore<IdentityRole>>();

            builder.Services.AddScoped<IAccountRepository, AccountRepository>();
            builder.Services.AddScoped<IPostRepository, PostRepository>();
            builder.Services.AddScoped<IImageRepository, ImageRepository>();
            builder.Services.AddScoped<ITagRepository, TagRepository>();
            //builder.Services.AddScoped<IEventRepository, EventRepository>();
            //builder.Services.AddScoped<IForumThreadRepository, ForumThreadRepository>();

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

                endpoints.MapControllerRoute(
                    name: "blog",
                    pattern: "blog/{controller=Blog}/{action=Index}/{id?}",
                    defaults: new { controller = "Blog", action = "Index" });

                endpoints.MapControllerRoute(
                    name: "event",
                    pattern: "event/{controller=Event}/{action=Index}/{id?}",
                    defaults: new { controller = "Event", action = "Index" });

                endpoints.MapControllerRoute(
                    name: "forum",
                    pattern: "forum/{controller=Forum}/{action=Index}/{id?}",
                    defaults: new { controller = "Forum", action = "Index" });
            });

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<ApplicationDbContext>();
                var userManager = services.GetRequiredService<UserManager<Account>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                var signInManager = services.GetRequiredService<SignInManager<Account>>();
                //var roleStore = services.GetService<RoleStore<IdentityRole>>();
                //var roleManager = services.GetService<RoleManager<Account>>();
                //var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                RoleStore<IdentityRole> roleStore = new RoleStore<IdentityRole>(context);


                var userExists = await userManager.GetUsersInRoleAsync("Admin");


                var admin = await roleStore.CreateAsync(
                    new IdentityRole(){
                        Name = "Admin",
                        NormalizedName = "ADMIN",
                    }
                );
                var blogger = await roleStore.CreateAsync(
                    new IdentityRole()
                    {
                        Name = "Blogger",
                        NormalizedName = "BLOGGER",
                    }
                );
                var discusser = await roleStore.CreateAsync(
                    new IdentityRole()
                    {
                        Name = "Discusser",
                        NormalizedName = "DISCUSSER",
                    }
                );
                var planner = await roleStore.CreateAsync(
                    new IdentityRole()
                    {
                        Name = "Planner",
                        NormalizedName = "PLANNER",
                    }
                );

                if (userExists.Count() < 1)
                {
                    var user = new Account { 
                        UserName = "Populus", 
                        Email = "emil.a.johansson@gmail.com"
                    };
                    var password = "kOW9k$4e@BT8";
                    await userManager.CreateAsync(user, password);
                    await userManager.AddToRoleAsync(user, "Admin");
                    await userManager.AddToRoleAsync(user, "Blogger");
                    await userManager.AddToRoleAsync(user, "Discusser");
                    await userManager.AddToRoleAsync(user, "Planner");
                }

                context.Database.EnsureCreated(); //If not using EF migrations

                context.Database.Migrate();
            }
            await app.RunAsync();
        }
    }
}