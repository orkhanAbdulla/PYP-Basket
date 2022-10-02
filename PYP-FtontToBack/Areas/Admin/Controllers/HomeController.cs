using Microsoft.AspNetCore.Mvc;

namespace PYP_FtontToBack.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        [Area("admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
