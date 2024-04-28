using Modulo_Asesorias.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace Modulo_Asesorias.Controllers
{
    public class ProfesorController : Controller
    {
        public IActionResult Horario()
        {
            List<Agendum> listaAsesorias = new List<Agendum>();
            using (var db = new AsesoriaContext())
            {
                DateOnly fechaActual = DateOnly.FromDateTime(DateTime.Today);
                var a = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Actor).Value + "");

                listaAsesorias = db.Agenda.Include(x => x.FkIdEstudianteNavigation).ThenInclude(x => x.FkIdPersonaNavigation)
                                    .Where(x => x.FkIdProfesor == a && x.FechaAgenda>fechaActual).ToList();

            }
            return View(listaAsesorias);
        }

        public IActionResult crearHorario()
        {
            return View();
        }


        [HttpPost]
        public IActionResult crear(IFormCollection form)
        {
            DateOnly auxiliar = new DateOnly(2024, 03, 04);
            Dictionary<string, List<List<string>>> todo = new Dictionary<string, List<List<string>>>();
            for (int i = 0; i < 5; i++)
            {
                todo.Add(auxiliar.DayOfWeek.ToString(), new List<List<string>>()); 
                for (int j = 0; j < 2; j++)
                {
                    todo[auxiliar.DayOfWeek.ToString()].Add(new List<string>());
                }
                auxiliar=auxiliar.AddDays(1);
            }

            foreach (var key    in form.Keys)
            {
                if (key.Contains("L")){

                    if (key.Contains("L_input")){
                        todo["Monday"][0].Add(form[key]);
                    }
                    else if (key.Contains("L_select"))
                    {
                       todo["Monday"][1].Add(form[key]);
                    }

                }else if (key.Contains("M")){
                    if (key.Contains("M_input"))
                    {
                        todo["Tuesday"][0].Add(form[key]);
                    }
                    else if (key.Contains("M_select"))
                    {
                        todo["Tuesday"][1].Add(form[key]);
                    }   

                }else if (key.Contains("X")){
                    if (key.Contains("X_input"))
                    {
                        todo["Wednesday"][0].Add(form[key]);
                    }
                    else if(key.Contains("X_select"))
                    {
                        todo["Wednesday"][1].Add(form[key]);
                    }

                }else if (key.Contains("J")){
                    if (key.Contains("J_input"))
                    {
                        todo["Thursday"][0].Add(form[key]);
                    }
                    else if (key.Contains("J_select"))
                    {
                        todo["Thursday"][1].Add(form[key]);
                    }

                }
                else if(key.Contains("V"))
                {
                    if(key.Contains("V_input"))
                    {
                        todo["Friday"][0].Add(form[key]);
                    }
                    else if (key.Contains("V_select"))
                    {
                        todo["Friday"][1].Add(form[key]);
                    }
                }
            }

            var diasEspanol = new Dictionary<string, string>()
            {
                { "Monday", "Lunes" },
                { "Tuesday", "Martes" },
                { "Wednesday", "Miércoles" },
                { "Thursday", "Jueves" },
                { "Friday", "Viernes" },
                { "Saturday","Sabado"},
                { "Sunday","Domingo"}
            };

            List<Agendum> listaAsesorias = new List<Agendum>();

            var aux1 = (Request.Form["fechaInicio"]+"").Split("-");
            var aux2 = (Request.Form["fechaFin"] + "").Split("-");
            DateOnly fechaInicio = new DateOnly(Int32.Parse(aux1[0]), Int32.Parse(aux1[1]), Int32.Parse(aux1[2]));
            DateOnly fechafin = new DateOnly(Int32.Parse(aux2[0]), Int32.Parse(aux2[1]), Int32.Parse(aux2[2]));


            List<string> dias = new List<string>();
            if(Request.Form["L"] == "L")
            {
                dias.Add("Monday");
            }
            if (Request.Form["M"] == "M")
            {
                dias.Add("Tuesday");
            }
            if (Request.Form["X"] == "X")
            {
                dias.Add("Wednesday");
            }
            if (Request.Form["J"] == "J")
            {
                dias.Add("Thursday");
            }
            if (Request.Form["V"] == "V")
            {
                dias.Add("Friday");
            }
            if (Request.Form["S"] == "S")
            {
                dias.Add("Saturday");
            }
            if (Request.Form["D"] == "D")
            {
                dias.Add("Sunday");
            }

            int a = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Actor).Value+"");

            for (DateOnly fecha = fechaInicio; fecha <= fechafin; fecha = fecha.AddDays(1))
            {

                foreach (string d in dias)
                {
                        if (fecha.DayOfWeek.ToString() == d)
                    {
                        for (int i = 0; i < todo[d][0].Count; i++)
                        {
                            var horaactual = todo[d][0][i].Split(":");
                            var kkkk = todo[d][1][i];
                            for (var hora = new DateTime(fecha.Year, fecha.Month, fecha.Day, Int32.Parse(horaactual[0]), Int32.Parse(horaactual[1]), 0); hora < new DateTime(fecha.Year, fecha.Month, fecha.Day, Int32.Parse(horaactual[0]) + Int32.Parse(todo[d][1][i][0] + ""), Int32.Parse(horaactual[1]), 0); hora = hora.AddMinutes(20))
                            {
                                listaAsesorias.Add(new Agendum
                                {
                                    DiaAgenda = diasEspanol[fecha.DayOfWeek.ToString()],
                                    FechaAgenda = fecha,
                                    HorainicioAgenda = new TimeOnly(hora.Hour, hora.Minute),
                                    HorafinAgenda = (new TimeOnly(hora.Hour, hora.Minute)).AddMinutes(20),
                                    NumeroAgenda = 0,
                                    FkIdSala = 1,
                                    FkIdProfesor = a

                                });
                            }
                        }
                    }
                }

            }
            using (var db = new AsesoriaContext())
            {
                foreach (var ag in listaAsesorias)
                {
                    db.Agenda.Add(ag);
                }
                db.SaveChanges();
            }
            return RedirectToAction("Horario","Profesor");
        }
    }
}
