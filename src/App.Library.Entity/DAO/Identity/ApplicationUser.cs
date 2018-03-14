using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace App.Library.Entity.DAO
{
    public class ApplicationUser : IdentityUser<string>
    {
        public string Company { get; set; }

        public string ExternalProvider { get; set; }

        public string DisplayName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }
    }
    public class ApplicationRole : IdentityRole<string>
    {
        public string Descriptions { get; set; }
    }
}
