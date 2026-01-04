using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RouteGymManagementBLL.Attachment_Service;
using RouteGymManagementBLL.BusinessServices.Implementation;
using RouteGymManagementBLL.BusinessServices.Interfaces;
using RouteGymManagementBLL.Mapping;
using RouteGymManagementDAL.Data.Contexts;
using RouteGymManagementDAL.Data.DataSeed;
using RouteGymManagementDAL.Entities;
using RouteGymManagementDAL.Repositories.Classes;
using RouteGymManagementDAL.Repositories.Implementation;
using RouteGymManagementDAL.Repositories.Interfaces;
using RouteGymManagementDAL.UnitOfWorkPattern.Interfaces;

namespace RouteGymManagementPL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region DI Regsiteration

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<GymDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")); // ShortHand
            });

            //builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            //builder.Services.AddScoped<IPlanRepository, PlanRepository>();
            builder.Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            builder.Services.AddScoped(typeof(ISessionRepository), typeof(SessionRepository));
            builder.Services.AddScoped(typeof(IMembershipRepository), typeof(MembershipRepository));
            builder.Services.AddScoped(typeof(IAnalyticsService), typeof(AnalyticsService));
            builder.Services.AddScoped(typeof(IMemberService), typeof(MemberService));
            builder.Services.AddScoped(typeof(ITrainerService), typeof(TrainerService));
            builder.Services.AddScoped(typeof(IPlanService), typeof(PlanService));
            builder.Services.AddScoped(typeof(ISessionService), typeof(SessionService));
            builder.Services.AddScoped(typeof(IAttachmentService), typeof(AttachmentService));
            builder.Services.AddScoped(typeof(IAccountService), typeof(AccountService));
            builder.Services.AddScoped(typeof(IMembershipService), typeof(MembershipService));
            builder.Services.AddScoped(typeof(IBookingRepository), typeof(BookingRepository));
            builder.Services.AddScoped(typeof(IBookingService), typeof(BookingService));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(Config =>
            {

                Config.User.RequireUniqueEmail = true;

            }).AddEntityFrameworkStores<GymDbContext>();

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";  // No Need To write it - its by default
                options.AccessDeniedPath = "/Account/AccessDenied"; // No Need To write it - its by default
            });

            builder.Services.AddAutoMapper(x => x.AddProfile(new MappingProfiles()));

            #endregion


            var app = builder.Build();


            #region Migrate Database + Data Seeding

            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<GymDbContext>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();


            var pendingMigrations = dbContext.Database.GetMigrations();
            if (pendingMigrations?.Any() ?? false)
            {
                dbContext.Database.Migrate();
            }
            GymDbContextSeeding.SeedData(dbContext);
            IdentityDbContextSeeding.SeedData(roleManager, userManager);

            #endregion


            #region Configure PipeLine [Middlewares]

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
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();
            #endregion


            app.Run();
        }
    }
}
