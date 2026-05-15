using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace app_SistemaMultas_Online.Models
{
    public class csEstructuraAgente
    {
        public class requestAgente
        {
            public int id_agente { get; set; }
            public string nombre { get; set; }
        }

        public class responseAgente
        {
            public int respuesta { get; set; }
            public string descripcion_respuesta { get; set; }
        }

        public class requestEliminarAgente
        {
            public int id_agente { get; set; }
        }
    }
}