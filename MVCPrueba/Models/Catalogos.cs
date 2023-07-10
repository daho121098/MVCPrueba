using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCPrueba.Models
{
    public class Catalogos
    {
        public List<Genero> listaGenero { get; set; }
        public List<GeneroPoesia> listaGeneroPoesia { get; set; }

        public List<Carrera> listaCarrera { get; set; }

    }
}