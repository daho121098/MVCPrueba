using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosRestPrueba.Models
{
    public class Inscripcion
    {
        public string carnet { get; set; }
        public string nombre { get; set; }
        public string idGenero { get; set; }
        public string telefono { get; set; }
        public DateTime fechaNacimiento { get; set; }
        public int idCarrera { get; set; }
        public int idGeneroPoesia { get; set; }
        public DateTime fechaInscripcion { get; set; }

    }
}