using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bela.Domain.Interfaces
{
    public interface IBaseRepository
    {
        bool Save();
        Task<bool> SaveAsync();
    }
}
