using Bela.Application.ViewModels.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bela.WebMVC.ComponentModels
{
    public class HandVCModel
    {
        public bool IsPlayable { get; set; }
        public List<CardInHandModel> CardsInHand { get; set; }
    }
}
