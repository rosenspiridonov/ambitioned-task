using Microsoft.AspNetCore.Mvc;

namespace SimpleCalculator.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
