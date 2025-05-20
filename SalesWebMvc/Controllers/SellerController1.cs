using Microsoft.AspNetCore.Mvc;

namespace SalesWebMvc.Controllers
{
    public class SellerController1 : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
