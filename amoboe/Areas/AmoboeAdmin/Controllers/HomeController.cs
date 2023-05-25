using Microsoft.AspNetCore.Mvc;

namespace amoboe.Areas.AmoboeAdmin.Controllers
{
    [Area("Amoboeadmin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
