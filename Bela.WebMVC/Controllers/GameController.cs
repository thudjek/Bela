using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bela.WebMVC.Controllers
{
    [Authorize]
    public class GameController : Controller
    {
        public GameController()
        {

        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
