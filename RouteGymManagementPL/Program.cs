using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RouteGymManagementBLL;
using RouteGymManagementBLL.Services.Classes;
using RouteGymManagementBLL.Services.Interfaces;
using RouteGymManagementBLL.ViewModels.AnalyticsViewModels;
using RouteGymManagementDAL.Data.Contexts;
using RouteGymManagementDAL.Data.DataSeed;
using RouteGymManagementDAL.Repositories.Classes;
using RouteGymManagementDAL.Repositories.Interfaces;

namespace RouteGymManagementPL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<GymDbContext>(options =>
            {
                //options.UseSqlServer(builder.Configuration.GetSection("ConnectionStrings")["DefaultConnection"]);
                //options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]);
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")); // ShortHand
            });

            //builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            //builder.Services.AddScoped<IPlanRepository, PlanRepository>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<ISessionRepository, SessionRepository>();
            builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();
            builder.Services.AddScoped<IMemberService, MemberService>();
            builder.Services.AddScoped<ITrainerService, TrainerService>();

            builder.Services.AddAutoMapper(x => x.AddProfile(new MappingProfiles()));

            var app = builder.Build();


            #region Migrate Database + Data Seeding

            using var scope = app.Services.CreateScope(); 
            var dbContext = scope.ServiceProvider.GetRequiredService<GymDbContext>();
            var pendingMigrations = dbContext.Database.GetMigrations();
                if (pendingMigrations?.Any() ?? false)
                {
                    dbContext.Database.Migrate();
                }
            GymDbContextSeeding.SeedData(dbContext);
            #endregion

            // Configure the HTTP request pipeline. 
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();

            //app.MapControllerRoute(
            //    name: "Trainers", // Route Name
            //    pattern: "Couth/{action}", // 
            //    defaults: new { controller = "Trainer", action = "Index" }
            //    );

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}") // Variable Segment
                // if i didn't write controller it will by default(Home) || action it will by default(Index)
                .WithStaticAssets();

            app.Run();
        }
    }
}
