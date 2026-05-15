using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace app_SistemaMultas_Online.Models
{
    public class csEstructuraVehiculo
    {
        public class requestVehiculo
        {
            public int id_vehiculo { get; set; }
            public string placa { get; set; }
            public string marca { get; set; }
            public int id_conductor { get; set; }
        }

        public class responseVehiculo
        {
            public int respuesta { get; set; }
            public string descripcion_respuesta { get; set; }
        }

        public class requestEliminarVehiculo
        {
            public int id_vehiculo { get; set; }
        }
    }
}