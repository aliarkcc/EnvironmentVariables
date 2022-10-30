using EnvironmentVariables.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EnvironmentVariables.Controllers
{
    public class HomeController : Controller
    {
        IWebHostEnvironment WebHostEnvironment;
        IConfiguration Configuration { get; }

        public HomeController(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            WebHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            string connStr = Configuration.GetSection("Connections").Value;
            ViewBag.ConnectionString = connStr;

            if (WebHostEnvironment.IsDevelopment())
            {
                ViewBag.Environment = "Development";
            }
            else if (WebHostEnvironment.IsStaging())
            {
                ViewBag.Environment = "Staging";
            }
            else if (WebHostEnvironment.IsEnvironment("Test"))
            {
                ViewBag.Environment = "Test";
            }
            else if (WebHostEnvironment.IsProduction())
            {
                ViewBag.Environment = "Production";
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}