using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Asesorias.Controllers
{
    [Authorize]
    public class AsesoriaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
