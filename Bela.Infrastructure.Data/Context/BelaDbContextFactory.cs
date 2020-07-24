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
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-4G1N4SQ\\SQLEXPRESS;Initial Catalog=Bela;Integrated Security=True;MultipleActiveResultSets=True;");

            return new BelaDbContext(optionsBuilder.Options);
        }
    }
}
