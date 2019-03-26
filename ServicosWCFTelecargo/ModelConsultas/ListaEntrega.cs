using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicosWCFTelecargo.ModelConsultas
{
    public class ListaEntrega
    {
        public string Data { get; set; }
        public string Recebedor { get; set; }
        public string DocumentoRecebedor { get; set; }
        public string Comentario { get; set; }
    }
}