using eBooking.Static;
using Microsoft.AspNetCore.Identity;

namespace eBooking.Models
{
    public class AppDbInitializer
    {
        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {

                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole<int>(UserRoles.Admin));

                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole<int>(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<Client>>();
                string adminUserEmail = "admin@etickets.com";

                var adminUser =  await userManager.FindByEmailAsync(adminUserEmail);

                if (adminUser == null)
                {
                    var newAdminUser = new Client()
                    {
                        
                        //Name = "Admin User",
                        UserName = "admin-user",
                        FirstName= "Karim",
                        LastName="Gamal",
                        
                        Email = adminUserEmail,
                        NormalizedEmail= adminUserEmail,
                        EmailConfirmed = true
                    };
                     
                     await userManager.CreateAsync(newAdminUser, "Coding@1234");
                     //await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }


                string appUserEmail = "user@etickets.com";

                var appUser =  await userManager.FindByEmailAsync(appUserEmail);

                if (appUser == null)
                {
                    var newAppUser = new Client()
                    {
                        //Name = "Application User",
                        UserName = "app-user",
                        FirstName = "Karim",
                        LastName = "Gamal",
                        
                        Email = appUserEmail,
                        NormalizedEmail = adminUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAppUser, "Coding@1234");
                    //await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }
    }
}
