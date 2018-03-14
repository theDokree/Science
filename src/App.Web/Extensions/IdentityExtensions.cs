using App.Library.Entity.DAO;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace App.Web.Extensions
{
    public static class IdentityExtensions
    {
        public static string GetDisplayName(this UserManager<ApplicationUser> userManager, IPrincipal principal)
        {
            var user = userManager.FindByNameAsync(principal.Identity.Name).Result;
            return user?.DisplayName;
        }

        public static string GetCompany(this UserManager<ApplicationUser> userManager, IPrincipal principal)
        {
            var user = userManager.FindByNameAsync(principal.Identity.Name).Result;
            return user?.Company;
        }

        public static IList<string> GetRoles(this UserManager<ApplicationUser> userManager, IPrincipal principal)
        {
            var user = userManager.FindByNameAsync(principal.Identity.Name).Result;
            var roles = userManager.GetRolesAsync(user).Result;
            return roles;
        }
    }
}
