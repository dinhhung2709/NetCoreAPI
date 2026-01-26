using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
namespace FirstWebMVC.Controllers
{
    public class DemoController : Controller
    { 
        // GET: /HelloWorld/
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Demo()
        {
            return View();
        }
        // GET: /HelloWorld/Welcome/ 

        public string Welcome()
        {
            return "Hello Nguyễn Đình Hưng 2221050384 ";
        }
    }
}