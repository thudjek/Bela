using Bela.Domain.Interfaces;
using Bela.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bela.Infrastructure.Data.Repositories
{
    public class GameRepository : IGameRepository
    {
        public BelaDbContext _dbContext;
        public GameRepository(BelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

    }
}
