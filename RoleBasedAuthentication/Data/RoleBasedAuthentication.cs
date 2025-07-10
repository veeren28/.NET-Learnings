using Microsoft.AspNetCore.Identity;

namespace RoleBasedAuthentication.Data
{
    public static class RoleBasedAuthentication 
    {
        public static async Task SeedRole(IServiceProvider service)
        {
            var role = service.GetRequiredService<RoleManager<IdentityRole>>();
            // service provider is a class which is used to get services that are registered in our program.js
            //Role Mangaer is a service whisch is used to get  roles identity,
            // Identity Role represents the role of a user 
            string[] roles = { "Student,Admin" };
            foreach (string r in roles) {
                if (!await role.RoleExistsAsync(r)) {
                    await role.CreateAsync(new  IdentityRole(r));
                }
            }
        }
    }
}
