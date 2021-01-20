using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bela.WebMVC.ComponentModels;
using System.Security.Claims;
using Bela.WebMVC.Extensions;
using Bela.Application.Interfaces;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using System.Text.Encodings.Web;
using Bela.Application.ViewModels.Game;

namespace Bela.WebMVC.Views.Shared.Components.CardsInHand
{
    public class CardsInHandViewComponent : ViewComponent
    {
        private readonly IGameService _gameService;
        public CardsInHandViewComponent(IGameService gameService)
        {
            _gameService = gameService;
        }

        public async Task<IViewComponentResult> InvokeAsync(bool allCards, bool isPlayable, int trump)
        {
            HandVCModel model = new HandVCModel();
            var user = (ClaimsPrincipal)User;
            var userId = user.GetUserId();

            model.IsPlayable = isPlayable;
            model.CardsInHand = await _gameService.GetCardHandModelListForPlayer(userId, allCards, trump);

            return View(model);
        }
    }
}
