using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Library.Database.Initializers
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection UseDbInitializer<T>(this IServiceCollection serviceCollection) where T : DbInitializer
        {
            serviceCollection.AddScoped<T>();
            return serviceCollection;
        }
    }

}
