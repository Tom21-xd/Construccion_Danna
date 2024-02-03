using Construccion_Danna.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using System.Linq;

namespace Asesorias.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Asesoria");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Usuario usua)
        {
            List<Usuario> lst = new List<Usuario>();
            Persona per = new Persona();
            using (var db = new BdAsesoriaContext())
            {
                lst = (from d in db.Usuarios
                       join p in db.Personas on d.Fkpersona equals p.PerCodigo
                       where (d.UsuCorreo == usua.UsuCorreo && d.UsuToken == usua.UsuToken)
                       select new Usuario
                       {
                           UsuId = d.UsuId,
                           Fkpermiso = d.Fkpermiso,
                           UsuCorreo = d.UsuCorreo,
                           UsuToken = d.UsuToken,
                           Persona = new Persona
                           {
                               PerCodigo = p.PerCodigo,
                               PerPrimerNombre = p.PerPrimerNombre,
                               PerPrimerApellido = p.PerPrimerApellido
                           }

                       }).ToList();
                if (lst.Count() > 0)
                {
                    var claims = new List<Claim>                {
                        new Claim(ClaimTypes.Name, lst[0].Persona.PerPrimerNombre+ " "+ lst[0].Persona.PerPrimerApellido),
                        new Claim(ClaimTypes.Actor, lst[0].Fkpermiso+""),
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    return RedirectToAction("Index", "Asesoria");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> salir()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

    }
}
