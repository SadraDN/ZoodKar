using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoints.UI.Areas.Admin.Controllers
{
    [Authorize(Roles = "AdminRole")]
    [Area("Admin")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
