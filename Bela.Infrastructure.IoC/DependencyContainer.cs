using Bela.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bela.Infrastructure.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            
        }

        public static void AddDbContext(IServiceCollection services, string connString)
        {
            services.AddDbContext<BelaDbContext>(o => o.UseSqlServer(connString));
        }
    }
}
