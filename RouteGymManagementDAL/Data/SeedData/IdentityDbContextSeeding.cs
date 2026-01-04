using Microsoft.AspNetCore.Identity;
using RouteGymManagementDAL.Entities;

namespace RouteGymManagementDAL.Data.DataSeed
{
    public static class IdentityDbContextSeeding
    {
        public static bool SeedData(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            try
            {
                var HasUsers = userManager.Users.Any();
                var HasRoles = roleManager.Roles.Any();
                if (HasRoles && HasUsers) return false;

                if (!HasRoles)
                {
                    var Roles = new List<IdentityRole>
                    {
                        new() {Name = "SuperAdmin" },
                        new() {Name = "Admin" }
                    };

                    foreach (var role in Roles)
                    {
                        if (!roleManager.RoleExistsAsync(role.Name!).Result)
                        {
                            roleManager.CreateAsync(role);
                        }
                    }
                }

                if (!HasUsers)
                {
                    var MainAdmin = new ApplicationUser()
                    {
                        FirstName = "Yousef",
                        LastName = "Nadi",
                        UserName = "YousefNadi",
                        Email = "yousefnadi01@gmail.com",
                        PhoneNumber = "01553234246"
                    };
                    userManager.CreateAsync(MainAdmin, "P@ssw0rd").Wait();
                    userManager.AddToRoleAsync(MainAdmin, "SuperAdmin").Wait();

                    var Admin = new ApplicationUser()
                    {
                        FirstName = "Sarah",
                        LastName = "Ali",
                        UserName = "SarahAli",
                        Email = "sarahali02@gmail.com",
                        PhoneNumber = "01553235256"
                    };
                    userManager.CreateAsync(Admin, "P@ssw0rd").Wait();
                    userManager.AddToRoleAsync(Admin, "Admin").Wait();
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Seeding Failed: {ex}");
                return false;
            }

        }
    }
}
