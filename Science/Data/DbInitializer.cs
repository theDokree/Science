using Science.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;

namespace Science.Data
{
    public class DbInitializer : CreateDatabaseIfNotExists<ScienceContext>
    {
        protected override void Seed(ScienceContext context)
        {
            base.Seed(context);
        }

    }

}
