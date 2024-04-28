using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using System.Linq;
using Modulo_Asesorias.Models;

namespace Modulo_Asesorias.Controllers
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
                if(User.IsInRole("1"))
                {
                    return RedirectToAction("Horario", "Profesor");
                }
                return RedirectToAction("Reservar", "Estudiante");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Usuario usua)
        {
            List<Usuario> lst = new List<Usuario>();
            Persona per = new Persona();
            using (var db = new AsesoriaContext())
            {
                lst = (from d in db.Usuarios
                       join p in db.Personas on d.FkIdPersona equals p.IdPersona
                       where (d.CorreoUsuario == usua.CorreoUsuario && d.ContraseniaUsuario == usua.ContraseniaUsuario)
                       select new Usuario
                       {
                           IdUsuario = d.IdUsuario,
                           FkIdPermiso = d.FkIdPermiso,
                           FkIdPersonaNavigation =  new Persona
                           {
                               IdPersona = p.IdPersona,
                               Nombre1Persona = p.Nombre1Persona,
                               Apellido1Persona =p.Apellido1Persona,
                           }

                       }).ToList();
                if (lst.Count() > 0)
                {
                    var claims = new List<Claim>                {
                        new Claim(ClaimTypes.Name, lst[0].FkIdPersonaNavigation.Nombre1Persona+ " "+ lst[0].FkIdPersonaNavigation.Apellido1Persona),
                        new Claim(ClaimTypes.Role, lst[0].FkIdPermiso+""),
                        new Claim(ClaimTypes.Actor, lst[0].IdUsuario+""),    
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    if (lst[0].FkIdPermiso == 1)
                    {
                        return RedirectToAction("Horario", "Profesor");

                    }
                    else
                    {
                        return RedirectToAction("Reservar", "Estudiante");

                    }

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
