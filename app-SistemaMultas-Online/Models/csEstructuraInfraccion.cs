using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace app_SistemaMultas_Online.Models
{
    public class csEstructuraInfraccion
    {
        public class requestInfraccion
        {
            public int id_infraccion { get; set; }
            public DateTime fecha { get; set; }
            public string lugar { get; set; }
            public int id_vehiculo { get; set; }
            public int id_agente { get; set; }
            public int id_sancion { get; set; }
            public int id_estado { get; set; }
        }

        public class responseInfraccion
        {
            public int respuesta { get; set; }
            public string descripcion_respuesta { get; set; }
        }

        public class requestEliminarInfraccion
        {
            public int id_infraccion { get; set; }
        }
    }
}