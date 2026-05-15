using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace app_SistemaMultas_Online.Models
{
    public class csEstructuraSancion
    {
        public class requestSancion
        {
            public int id_sancion { get; set; }
            public string descripcion { get; set; }
            public decimal monto { get; set; }
        }

        public class responseSancion
        {
            public int respuesta { get; set; }
            public string descripcion_respuesta { get; set; }
        }

        public class requestEliminarSancion
        {
            public int id_sancion { get; set; }
        }
    }
}