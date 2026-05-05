using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace app_SistemaMultas_Online.Models
{
    public class csEstructuraUsuario
    {
        public class requestUsuario
        {
            public int id_usuario { get; set; }
            public string username { get; set; }
            public string password { get; set; }
            public int id_agente { get; set; }
        }

        public class responseUsuario
        {
            public int respuesta { get; set; }
            public string descripcion_respuesta { get; set; }
        }

        public class requestEliminarUsuario
        {
            public int id_usuario { get; set; }
        }
    }
}