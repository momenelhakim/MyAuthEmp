using Microsoft.AspNetCore.Identity;

namespace MyAuthEmp.Models
{
 
        public class SeedData
        {
            public static async Task Initialize(IServiceProvider serviceProvider)
            {
                var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();


                string[] roleNames = { "Admin", "Manager", "Employee" };


                foreach (var roleName in roleNames)
                {
                    if (!await roleManager.RoleExistsAsync(roleName))
                    {
                        var result = await roleManager.CreateAsync(new IdentityRole(roleName));
                        if (!result.Succeeded)
                        {
                            throw new Exception($"Failed to create role '{roleName}': {string.Join(", ", result.Errors.Select(e => e.Description))}");
                        }
                    }
                }


                var users = new List<(string UserName, string Email, string Password, string Role)>
        {
            ("admin@domain.com", "admin@domain.com", "Password123!", "Admin"),
            ("momenelhakim", "momenelhakim@yahoo.com", "MoeMen2013@", "Manager"),
            ("john.doe", "john.doe@example.com", "JohnDoe2024!", "Employee"),
            ("jane.smith", "jane.smith@example.com", "JaneSmith2024!", "Manager"),
            ("employee.user", "employee.user@example.com", "EmpUser2024!", "Employee")
        };


                foreach (var (userName, email, password, role) in users)
                {
                    var user = new IdentityUser { UserName = userName, Email = email, EmailConfirmed = true };

                    if (await userManager.FindByEmailAsync(email) == null)
                    {
                        var createUserResult = await userManager.CreateAsync(user, password);
                        if (!createUserResult.Succeeded)
                        {
                            throw new Exception($"Failed to create user '{userName}': {string.Join(", ", createUserResult.Errors.Select(e => e.Description))}");
                        }

                        var addToRoleResult = await userManager.AddToRoleAsync(user, role);
                        if (!addToRoleResult.Succeeded)
                        {
                            throw new Exception($"Failed to assign role '{role}' to user '{userName}': {string.Join(", ", addToRoleResult.Errors.Select(e => e.Description))}");
                        }
                    }
                }
            }
        }
    }

