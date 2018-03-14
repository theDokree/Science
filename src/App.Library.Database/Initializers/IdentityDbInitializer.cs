using App.Library.Common.Constants;
using App.Library.Database.Context;
using App.Library.Entity.DAO;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Library.Database.Initializers
{
    public class IdentityDbInitializer : DbInitializer
    {
        private UserManager<ApplicationUser> UserManager { get; }

        private RoleManager<ApplicationRole> RoleManager { get; }

        private SignInManager<ApplicationUser> SignInManager { get; }

        private AppIdentityDbContext DbContext { get; set; }

        public IdentityDbInitializer(AppIdentityDbContext dbContext, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
            SignInManager = signInManager;
            DbContext = dbContext;
        }

        public override async Task InitializeDatabase()
        {
            var pendingMigrations = DbContext.Database.GetPendingMigrations();
            if (pendingMigrations.Count() > 0)
            {
                DbContext.Database.Migrate();
            }

            try
            {
                await InitializeRoles();
                await InitializeUsers();
            }
            catch (Exception e)
            {

            }
        }

        private async Task InitializeRoles()
        {
            foreach (var role in PredefinedValues.EnabledRoles)
            {
                if (RoleManager.Roles.FirstOrDefault(r => r.Name == role) == null)
                {
                    await RoleManager.CreateAsync(new ApplicationRole { Name = role });
                }
            }
        }

        private async Task InitializeUsers()
        {
            var name = "admin@admin.ru";
            var displayName = "Администратор";
            var password = "Ghjcnjytnfr123#@!";
            var email = "admin@admin.ru";
            var company = "3061";

            if (UserManager.Users.FirstOrDefault(r => r.UserName == name) == null)
            {
                await UserManager.CreateAsync(new ApplicationUser
                {
                    UserName = name,
                    DisplayName = displayName,
                    Company = company,
                    Email = email,
                    ExternalProvider = null,
                }, password);
                var user = await UserManager.FindByNameAsync(name);
                await UserManager.AddToRoleAsync(user, Roles.SystemAdministrator);
            }
        }
    }
}


