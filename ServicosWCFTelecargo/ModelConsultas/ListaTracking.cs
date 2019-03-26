using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicosWCFTelecargo.ModelConsultas
{
    public class ListaTracking
    {
        public DateTime? Data { get; set; }
        public string Hora { get; set; }
        public string Comentario { get; set; }
    }
}