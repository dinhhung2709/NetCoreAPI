using Microsoft.AspNetCore.Mvc;

namespace DemoMvcApp.Controllers
{
    public class ViewBagController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Message = "Ví dụ truyền dữ liệu từ Controller sang View bằng ViewBag";
            return View();
        }
    }
}
