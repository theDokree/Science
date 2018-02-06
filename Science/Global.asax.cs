using Science.Data;
using Science.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Science
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
           Database.SetInitializer(new DbInitializer());

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ScienceContext, Configuration>());

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
