using Modulo_Asesorias.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;

namespace Modulo_Asesorias.Controllers { 

    public class EstudianteController : Controller
    {
        public IActionResult Reservar()
        {
            List<Usuario> profesores = new List<Usuario>();


            using (var db = new AsesoriaContext())
            {
                // var a = db.Database.ExecuteSqlInterpolated("SELECT persona.NOMBRE1_PERSONA , persona.APELLIDO1_PERSONA ,GROUP_CONCAT(DISTINCT  asignatura.NOMBRE_ASIGNATURA) From usuario INNER JOIN persona ON usuario.FK_ID_PERSONA = persona.ID_PERSONA INNER JOIN agenda ON agenda.FK_ID_PROFESOR = usuario.ID_USUARIO INNER JOIN impartir ON impartir.FK_ID_USUARIO= usuario.ID_USUARIO INNER JOIN asignatura ON impartir.FK_ID_ASIGNATURA=asignatura.ID_ASIGNATURA WHERE usuario.FK_ID_PERMISO=1 GROUP BY persona.NOMBRE1_PERSONA, persona.APELLIDO1_PERSONA;");

               
                 profesores = (from usuario in db.Usuarios
                                  join persona in db.Personas on usuario.FkIdPersona equals persona.IdPersona
                                  join agenda in db.Agenda on usuario.IdUsuario equals agenda.FkIdProfesor
                                  join impartir in db.Impartirs on usuario.IdUsuario equals impartir.FkIdUsuario
                                  group new { usuario, persona, impartir } by new { usuario.IdUsuario, usuario.FkIdPersona, persona.Nombre1Persona, persona.Apellido1Persona } into g
                                  select new Usuario
                                  {
                                      IdUsuario = g.Key.IdUsuario,
                                      FkIdPersona = g.Key.FkIdPersona,
                                      FkIdPersonaNavigation = new Persona
                                      {
                                          Nombre1Persona = g.Key.Nombre1Persona,
                                          Apellido1Persona = g.Key.Apellido1Persona,
                                      },
                                      Impartirs = g.Select( x => new Impartir
                                      {
                                          FkIdAsignatura = x.impartir.FkIdAsignatura,
                                          FkIdAsignaturaNavigation = new Asignatura
                                          {
                                              NombreAsignatura = x.impartir.FkIdAsignaturaNavigation.NombreAsignatura
                                          }
                                      }).Distinct().ToList()
                                  }).ToList();
                return View(profesores);
            }
        }

        [HttpPost]
        public  IActionResult obtenerFechas(int id_profesor)
        {
            using (var db = new AsesoriaContext())
            {
                var fechas = (from usuario in db.Usuarios
                              join agenda in db.Agenda on usuario.IdUsuario equals agenda.FkIdProfesor
                              where usuario.IdUsuario == id_profesor && agenda.FkIdEstudiante == null
                              select agenda.FechaAgenda.ToString()).Distinct().ToList();
                 return Json(fechas);             
            }
            
        }

        [HttpPost]
        public IActionResult obtenerHoras(DateOnly fecha, int idprofesor) {

            using (var db = new AsesoriaContext())
            {
                var horas = (from Agenda in db.Agenda
                              where Agenda.FechaAgenda == fecha && Agenda.FkIdProfesor == idprofesor && Agenda.FkIdEstudiante == null
                              select new Agendum {
                                IdAgenda = Agenda.IdAgenda,
                                HorainicioAgenda = Agenda.HorainicioAgenda,
                              }).ToList();
                return Json(horas);
            }
        }

        [HttpPost]
        public IActionResult crearReserva( int ida)
        {
            using (var db = new AsesoriaContext())
            {
                var a = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Actor).Value + "");

                var agenda = db.Agenda.Where(x => x.IdAgenda == ida).FirstOrDefault();

                agenda.FkIdEstudiante = a;
                
                db.SaveChanges();

                return RedirectToAction("Index","Home");
            }
        }

        public IActionResult verAsesorias()
        {

            List<Agendum> listaAsesorias = new List<Agendum>();
            using (var db = new AsesoriaContext())
            {
                DateOnly fechaActual = DateOnly.FromDateTime(DateTime.Today);
                var a = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Actor).Value + "");

                listaAsesorias = db.Agenda.Include(x => x.FkIdEstudianteNavigation).ThenInclude(x => x.FkIdPersonaNavigation)
                                    .Include(x => x.FkIdProfesorNavigation).ThenInclude(x => x.FkIdPersonaNavigation)
                                    .Where(x => x.FkIdEstudiante == a && x.FechaAgenda > fechaActual).ToList();

            }
            return View(listaAsesorias);

        }

    }
}
