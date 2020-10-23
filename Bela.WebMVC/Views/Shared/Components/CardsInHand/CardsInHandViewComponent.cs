using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bela.WebMVC.ComponentModels;
using System.Security.Claims;
using Bela.WebMVC.Extensions;
using Bela.Application.Interfaces;

namespace Bela.WebMVC.Views.Shared.Components.CardsInHand
{
    public class CardsInHandViewComponent : ViewComponent
    {
        private readonly IGameService _gameService;
        public CardsInHandViewComponent(IGameService gameService)
        {
            _gameService = gameService;
        }

        public async Task<IViewComponentResult> InvokeAsync(bool allCards, bool isPlayable)
        {
            HandVCModel model = new HandVCModel();
            var user = (ClaimsPrincipal)User;
            var userId = user.GetUserId();

            model.IsPlayable = isPlayable;
            model.CardsInHand = await _gameService.GetCardHandModelListForPlayer(userId, allCards);

            //model.IsPlayable = true;
            //model.CardsInHand = new List<Application.ViewModels.Game.CardInHandModel>()
            //{
            //    new Application.ViewModels.Game.CardInHandModel() { CardString = "HA", CardUrl = "imgs/cards/herc_as.png" },
            //    new Application.ViewModels.Game.CardInHandModel() { CardString = "HK", CardUrl = "imgs/cards/herc_kralj.png" },
            //    new Application.ViewModels.Game.CardInHandModel() { CardString = "HK", CardUrl = "imgs/cards/herc_kralj.png" },
            //    new Application.ViewModels.Game.CardInHandModel() { CardString = "HK", CardUrl = "imgs/cards/herc_kralj.png" },
            //    new Application.ViewModels.Game.CardInHandModel() { CardString = "HK", CardUrl = "imgs/cards/herc_kralj.png" },
            //    new Application.ViewModels.Game.CardInHandModel() { CardString = "HK", CardUrl = "imgs/cards/herc_kralj.png" },
            //    new Application.ViewModels.Game.CardInHandModel() { CardString = "HK", CardUrl = "imgs/cards/herc_kralj.png" }
            //};

            return View(model);
        }
    }
}
