using Microsoft.AspNetCore.Mvc;

namespace Erfa.PruductionManagement.Api.Controllers
{
    public class CatalogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
