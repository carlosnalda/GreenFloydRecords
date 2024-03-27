using CarlosNalda.GreenFloydRecords.WebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CarlosNalda.GreenFloydRecords.WebApp.Controllers
{
    public class InvalidActionController : Controller
    {
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("InvalidAction/PageNotFound")]
        public IActionResult PageNotFound()
        {
            return View();
        }
    }
}
