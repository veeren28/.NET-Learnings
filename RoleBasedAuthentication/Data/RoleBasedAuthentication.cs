using Microsoft.AspNetCore.Identity;

namespace RoleBasedAuthentication.Data
{
    public static class RoleSeeder
    {
        public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            // Step 1: Create Roles
            string[] roleNames = { "Admin", "User" };
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Step 2: Create default admin user
            var adminEmail = "admin@example.com";
            var adminPassword = "Admin@123"; // Strong password

            // ✅ Properly await this task
            var existingUser = await userManager.FindByEmailAsync(adminEmail);

            if (existingUser == null)
            {
                var user = new IdentityUser
                {
                    UserName = adminEmail,
                    Email = adminEmail // ✅ This was mistakenly set to the password before
                };

                var createResult = await userManager.CreateAsync(user, adminPassword);
                if (createResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
                else
                {
                    // Log or throw the error messages from createResult.Errors
                    foreach (var error in createResult.Errors)
                    {
                        Console.WriteLine($"Error creating admin user: {error.Description}");
                    }
                }
            }

            // ✅ Don't return anything here — this is not a controller or view
        }
    }
}
