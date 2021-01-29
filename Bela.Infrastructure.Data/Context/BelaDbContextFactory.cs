using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bela.Infrastructure.Data.Context
{
    public class BelaDbContextFactory : IDesignTimeDbContextFactory<BelaDbContext>
    {
        public BelaDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BelaDbContext>();
            optionsBuilder.UseSqlServer(args[0]);

            return new BelaDbContext(optionsBuilder.Options);
        }
    }
}
