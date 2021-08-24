using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace coderush.Controllers
{
    [Authorize(Roles = Pages.MainMenu.BlockTranslations.RoleName)]
    public class BlockTranslationsController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
    }
}
