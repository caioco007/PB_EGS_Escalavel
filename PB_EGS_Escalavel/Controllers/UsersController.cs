using Microsoft.AspNetCore.Mvc;

namespace PB_EGS_Escalavel.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
