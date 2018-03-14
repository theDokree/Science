using System;
using System.Collections.Generic;
using System.Text;

namespace App.Library.Common.Constants
{
    public static class PredefinedValues
    {
        public static IEnumerable<string> EnabledRoles { get; }

        static PredefinedValues()
        {
            EnabledRoles = new[] { Roles.SystemAdministrator, Roles.SystemOperator, Roles.SubdivisionAdministrator, Roles.SubdivisionOperator, Roles.NewsOperator };
        }
    }

}
