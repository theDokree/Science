namespace Science.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Science.Data.ScienceContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Science.Data.ScienceContext context)
        {

            base.Seed(context);
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            context.Bases.AddOrUpdate(
                new Models.Base { Id = 1, Name = "qqwwee" },
                new Models.Base { Id = 2, Name = "aassdd" },
                new Models.Base { Id = 3, Name = "123" });
        }
    }
}
