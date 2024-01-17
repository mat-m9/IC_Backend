using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using IC_Backend.Models;
using System.Numerics;
using Microsoft.EntityFrameworkCore;

namespace IC_Backend.Services
{
    public class PopulateDB
    {
        public static async void Initialize(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                DatabaseContext context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

                DefaultRolesServices rolesServices = scope.ServiceProvider.GetRequiredService<DefaultRolesServices>();

                
                List<string> roles = rolesServices.GetRolesList();
                var roleStore = new RoleStore<IdentityRole>(context);
                foreach (string role in roles)
                {
                    if (!context.Roles.Any(r => r.NormalizedName == role.ToUpper()))
                    {
                        await roleStore.CreateAsync(new()
                        {
                            Name = role,
                            NormalizedName = role.ToUpper(),
                            ConcurrencyStamp = Guid.NewGuid().ToString()
                        });
                    }
                }

                var user = new Usuario()
                {
                    UserName = "Administrador",
                    NormalizedUserName = "ADMINISTRADOR",
                    Email = "admin@test.com",
                    NormalizedEmail = "ADMIN@TEST.COM",
                    celular = "null",
                    SecurityStamp = Guid.NewGuid().ToString()
                };
                var rol = await roleStore.FindByNameAsync(await rolesServices.GetDefaultRole("administrador"));       


                UserManager<Usuario> _userManager = scope.ServiceProvider.GetService<UserManager<Usuario>>();
                if (!context.Users.Any(u => u.Email == user.Email))
                {
                    var password = new PasswordHasher<Usuario>();
                    var hashed = password.HashPassword(user, "P!1admin");
                    user.PasswordHash = hashed;
                    var userStore = new UserStore<Usuario>(context);
                    var result = await userStore.CreateAsync(user);
                    await _userManager.AddToRoleAsync(user, rol.NormalizedName);
                }
                else
                {
                    var user1 = await context.Users.Where(u => u.Email == user.Email).FirstOrDefaultAsync();
                    await _userManager.AddToRoleAsync(user1, rol.Name);
                }

                var userVendedor = new Usuario()
                {
                    UserName = "Vendedor",
                    NormalizedUserName = "VENDEDOR",
                    Email = "vendedor@test.com",
                    NormalizedEmail = "VENDEDOR@TEST.COM",
                    celular = "0987654321",
                    SecurityStamp = Guid.NewGuid().ToString()
                };
                var rolVendedor = await roleStore.FindByNameAsync(await rolesServices.GetDefaultRole("vendedor"));


                if (!context.Users.Any(u => u.Email == userVendedor.Email))
                {
                    var password = new PasswordHasher<Usuario>();
                    var hashed = password.HashPassword(userVendedor, "PassWord_1");
                    userVendedor.PasswordHash = hashed;
                    var userStore = new UserStore<Usuario>(context);
                    var result = await userStore.CreateAsync(userVendedor);
                    await _userManager.AddToRoleAsync(userVendedor, rolVendedor.NormalizedName);
                }
                else
                {
                    var user1 = await context.Users.Where(u => u.Email == userVendedor.Email).FirstOrDefaultAsync();
                    await _userManager.AddToRoleAsync(user1, rolVendedor.Name);
                }
            }
        }
    }
}
