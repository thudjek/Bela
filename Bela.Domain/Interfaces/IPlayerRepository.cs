using Bela.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bela.Domain.Interfaces
{
    public interface IPlayerRepository : IBaseRepository
    {
        void CreatePlayer(Player player);
    }
}
