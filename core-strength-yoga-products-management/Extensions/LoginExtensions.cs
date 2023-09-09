using core_strength_yoga_products_management.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace core_strength_yoga_products_management.Extensions
{
    public static class LoginExtensions
    {
        public static async Task<ManagementUser> UpsertUser(this ManagementUser user, UserManager<ManagementUser> userManager)
        {
            if (!userManager.Users.Any(r => r == user))
            {
                await userManager.CreateAsync(user);
            }

            return user;
        }

        public static async Task<ManagementUser> AddUserToRole(this ManagementUser user, UserManager<ManagementUser> userManager, string role)
        {
            await userManager.AddToRoleAsync(user, role);

            return user;
        }

        public static async Task<ManagementUser> UpdateRoles(this ManagementUser user, UserManager<ManagementUser> userManager, 
            RoleManager<IdentityRole> roleManager, IEnumerable<string> roles)
        {
            foreach (var role in roles)
            {
                await roleManager.CreateAsync(new IdentityRole(role));
                user = await user.AddUserToRole(userManager, role);
            }

            return user;
        }
    }
}
