using MVCPrueba.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace MVCPrueba.Controllers
{
    public class InscripcionesController : Controller
    {
        static HttpClient client = new HttpClient();

        // GET: Inscripciones
        public async Task<ActionResult> Formulario()
        {
            Catalogos objCatalogos = new Catalogos();

            HttpResponseMessage response1 = await client.GetAsync(
                "http://localhost:59763/api/Catalogos");

            objCatalogos = await response1.Content.ReadAsAsync<Catalogos>();
            if (objCatalogos != null)
            {
                this.Session["objCatalogos"] = objCatalogos;
            }
            else
            {
                this.Session["objCatalogos"] = null;
            }

            return View(objCatalogos);
        }

        // POST: Inscripciones
        [HttpPost]
        public async Task<ActionResult> Formulario(Inscripcion formulario)
        {
            bool respuesta = false;

            HttpResponseMessage response1 = await client.PostAsJsonAsync(
                "http://localhost:59763/api/Inscribir", formulario);

            respuesta = await response1.Content.ReadAsAsync<bool>();

            if (respuesta)
            {

                DateTime fechaParticipacion, fechaHoy = DateTime.Today;

                if (formulario.carnet.EndsWith("1") && formulario.idGeneroPoesia==3){
                    int contador = 1;
                    fechaParticipacion = fechaHoy;
                    while ( contador<=5)
                    {
                        fechaParticipacion = fechaParticipacion.AddDays(1);

                        if (fechaParticipacion.DayOfWeek != DayOfWeek.Saturday && fechaParticipacion.DayOfWeek != DayOfWeek.Sunday)
                        {
                            contador++;
                        }
                    }
                }
                else if(formulario.carnet.EndsWith("3") && formulario.idGeneroPoesia==2)
                {
                    fechaParticipacion = new DateTime(fechaHoy.Year, fechaHoy.Month, 1).AddMonths(1).AddDays(-1);

                    if(fechaParticipacion.DayOfWeek == DayOfWeek.Saturday)
                    {
                      fechaParticipacion = fechaParticipacion.AddDays(-1);
                    }
                    else if(fechaParticipacion.DayOfWeek == DayOfWeek.Sunday)
                    {
                        fechaParticipacion = fechaParticipacion.AddDays(-2);
                    }
                }
                else
                {
                    int dias = 0;
                    if (fechaHoy.DayOfWeek > DayOfWeek.Sunday && fechaHoy.DayOfWeek < DayOfWeek.Friday)
                    {
                        dias = 5-Convert.ToInt32(fechaHoy.DayOfWeek);
                    }
                    else if (fechaHoy.DayOfWeek == DayOfWeek.Saturday)
                    {
                        dias = -1;
                    }
                    else if (fechaHoy.DayOfWeek == DayOfWeek.Sunday)
                    {
                        dias = -2;
                    }
                    fechaParticipacion = fechaHoy.AddDays(dias);
                }

                this.ViewData["respuesta"] = String.Format("Tú fecha de particiación es: {0}",fechaParticipacion.ToString("dd/MM/yyyy"));

            }
            else
            {
                this.ViewData["respuesta"] = "Ocurrio un error, por favor intentalo nuevamente.";

            }

            return View("Error");
        }

        [HttpGet]
        public async Task<ActionResult> Reporte()
        {
            List<Inscripcion> listaInscripcion = new List<Inscripcion>();

            HttpResponseMessage response1 = await client.GetAsync(
                "http://localhost:59763/api/Reporte");

            listaInscripcion = await response1.Content.ReadAsAsync<List<Inscripcion>>();
            this.ViewData["Lista"] = listaInscripcion;
            Catalogos objCatalogos = new Catalogos();
            objCatalogos = (Catalogos)this.Session["objCatalogos"];
            return View(objCatalogos);
        }
    }
}
