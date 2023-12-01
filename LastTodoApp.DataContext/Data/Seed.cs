using LastTodoApp.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastTodoApp.DataContext.Data
{
    public class Seed
    {
        public static async System.Threading.Tasks.Task SeedUsersAndRolesAsync(IServiceProvider serviceProvider)
        {

            using (var serviceScope = serviceProvider.GetRequiredService<IServiceProvider>().CreateScope())
            {
                try {
                    // Roles
                    var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                    await SeedRolesAsync(roleManager);

                    // Users

                    var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();
                    await SeedUsersAsync(userManager);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error Message: " + ex.ToString());
                }
            }
        
        }

        // Seed Role Async

        public static async System.Threading.Tasks.Task SeedRolesAsync(RoleManager<IdentityRole> roleManager) {

            await CreateRoleAsync(roleManager, ERole.ADMIN);
            await CreateRoleAsync(roleManager, ERole.MANAGER);
            await CreateRoleAsync(roleManager, ERole.USER);

        }

        // Create Role Async

        public static async System.Threading.Tasks.Task CreateRoleAsync(RoleManager<IdentityRole> roleManager, ERole role) 
        { 
        
            var roleStringName = role.ToString();
            if (!await roleManager.RoleExistsAsync(roleStringName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleStringName));
                Console.WriteLine($"{roleStringName} role created successfully.");
            }

        }

        // Seed user Async

        public static async System.Threading.Tasks.Task SeedUsersAsync(UserManager<User> userManager)
        {
            await CreateUserAsync(userManager, "admin@gmail.com", "Admin", "Admin@1234?", ERole.ADMIN);
            await CreateUserAsync(userManager, "manager@gmail.com", "Manager", "Manager@1234?", ERole.MANAGER);
            await CreateUserAsync(userManager, "user@gmail.com", "User", "User@1234?", ERole.USER);
        }

        // create User Async 

        private static async System.Threading.Tasks.Task CreateUserAsync(UserManager<User> userManager, string email, string userName, string password, ERole role)
        {
            var existingUser = await userManager.FindByEmailAsync(email);

            if (existingUser == null)
            {
                var newUser = new User
                {
                    UserName = userName,
                    Email = email,
                    EmailConfirmed = true,
                };

                var result = await userManager.CreateAsync(newUser, password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(newUser, role.ToString());
                    Console.WriteLine($"{userName} user created and added to {role} role successfully.");
                }
                else Console.WriteLine($"Error creating {userName} user: {string.Join(", ", result.Errors)}");
            }
            else Console.WriteLine($"{userName} user already exists.");
        }
    }
}
