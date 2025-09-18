using Microsoft.AspNetCore.Mvc;

namespace BookStoreMvc.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
