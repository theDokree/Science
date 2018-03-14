using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.Library.Database.Initializers
{
    public abstract class DbInitializer
    {
        public abstract Task InitializeDatabase();
    }

}
