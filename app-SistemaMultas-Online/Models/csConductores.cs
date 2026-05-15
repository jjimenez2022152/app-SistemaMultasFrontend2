using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace app_SistemaMultas_Online.Models
{
    public class csConductores
    {
        public class requestConductor
        {
            public int id_conductor { get; set; }
            public string nombre { get; set; }
            public string dpi { get; set; }
        }

        public class responseConductor
        {
            public int respuesta { get; set; }
            public string descripcion_respuesta { get; set; }
        }

        public class requestEliminarConductor
        {
            public int id_conductor { get; set; }
        }
    }
}