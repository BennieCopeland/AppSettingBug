using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AppSettingBug.Models;
using Microsoft.Extensions.Options;

namespace AppSettingBug.Controllers
{
    public class HomeController : Controller
    {
        private readonly FeatureToggles _toggles;
        
        public HomeController(IOptionsSnapshot<FeatureToggles> featureTogglesSnapshot)
        {
            _toggles = featureTogglesSnapshot.Value;
        }
        
        public IActionResult Index()
        {
            var vm = new IndexViewModel();
            
            if (_toggles.SayHello)
            {
                vm.Message = "Hello World";
            }
            
            return View(vm);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    public class IndexViewModel
    {
        public string Message { get; set; }
    }
}
