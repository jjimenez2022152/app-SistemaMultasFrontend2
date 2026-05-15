using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace app_SistemaMultas_Online.Models
{
    public class csEstructuraLogin
    {
        public class requestLogin
        {
            public string username { get; set; }
            public string password { get; set; }
        }

        public class responseLogin
        {
            public int respuesta { get; set; }
            public string descripcion_respuesta { get; set; }
        }
    }
}