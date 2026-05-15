using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace app_SistemaMultas_Online.Models
{
    public class csEstructuraEstadoInfraccion
    {
        public class requestEstadoInfraccion
        {
            public int id_estado { get; set; }
            public string descripcion { get; set; }
        }

        public class responseEstadoInfraccion
        {
            public int respuesta { get; set; }
            public string descripcion_respuesta { get; set; }
        }

        public class requestEliminarEstadoInfraccion
        {
            public int id_estado { get; set; }
        }
    }
}