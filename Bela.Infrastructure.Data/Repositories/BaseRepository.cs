using Bela.Domain.Interfaces;
using Bela.Infrastructure.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bela.Infrastructure.Data.Repositories
{
    public abstract class BaseRepository : IBaseRepository
    {
        protected readonly BelaDbContext _dbContext;

        public BaseRepository(BelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Save()
        {
            return _dbContext.SaveChanges() > 0;
        }

        public async Task<bool> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
