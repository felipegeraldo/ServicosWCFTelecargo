using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicosWCFTelecargo.ModelConsultas
{
    public class ListaColeta
    {
        public string Numero { get; set; }
        public DateTime? Data { get; set; }
        public string Motorista { get; set; }
        public string Documento { get; set; }
        public string Comentario { get; set; }
    }
}