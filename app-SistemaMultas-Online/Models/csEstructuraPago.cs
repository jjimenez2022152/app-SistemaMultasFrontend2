using System;

namespace app_SistemaMultas_Online.Models
{
        public class csEstructuraPago
        {
            public class requestPago
            {
                public int id_pago { get; set; }
                public DateTime fecha_pago { get; set; }
                public decimal monto { get; set; }
                public int id_infraccion { get; set; }
            }

            public class responsePago
            {
                public int respuesta { get; set; }
                public string descripcion_respuesta { get; set; }
            }

            public class requestEliminarPago
            {
                public int id_pago { get; set; }
            }
    
        }
}
