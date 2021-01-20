using Bela.Application.Interfaces;
using Bela.WebMVC.ComponentModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bela.WebMVC.Views.Shared.Components.CallOnTable
{
    public class CallOnTableViewComponent : ViewComponent
    {
        private readonly IGameService _gameService;
        public CallOnTableViewComponent(IGameService gameService)
        {
            _gameService = gameService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int roundId, int playerPosition, bool isPartner)
        {
            CallOnTableVCModel model = new CallOnTableVCModel();
            model.CardUrls = await _gameService.GetListOfCardUrlsForCall(roundId, playerPosition, isPartner);
            return View(model);
        }
    }
}
