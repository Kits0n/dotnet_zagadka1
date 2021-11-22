using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using test6.Models;
using test6.Tools;

namespace test6.Controllers
{
    [MiddlewareFilter(typeof(LocalizationPipeline))]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStringLocalizer<HomeController> _stringLocalizer;

        public HomeController(ILogger<HomeController> logger, IStringLocalizer<HomeController> stringLocalizer)
        {
            _logger = logger;
            _stringLocalizer = stringLocalizer;
        }

        public IActionResult Index()
        {
            ViewData["klucz"] = _stringLocalizer["jakis tekst"];
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult CustomViewLocation()
        {
            return View();
        }
        
        /* ************************************************************* */
        public IActionResult AlaKot()
        {
            return View();
        }
        /* ************************************************************* */

        public IActionResult DefinedType()
        {
            int x = 6;
            return View(x);
        }

        public IActionResult CustomHtmlCode()
        {
            string code = "<div style=\"color: red;\">Pewien tekst</div><script>alert(\"1111\")</script>";
            return View("CustomHtmlCode", code);
        }

        public IActionResult Intelisense()
        {
            return View(new ErrorViewModel { RequestId = "123" });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
