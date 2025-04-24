using Microsoft.AspNetCore.Mvc;

namespace VillanyszamlakoltsegApi.Controllers
{
    public class ElectricityBillController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
