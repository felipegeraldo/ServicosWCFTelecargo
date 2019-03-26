using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicosWCFTelecargo.ModelConsultas
{
    public class ListaNotaFiscal
    {
        public string Numero { get; set; }
        public string Serie { get; set; }
        public decimal? Valor { get; set; }
        public int Volume { get; set; }
        public decimal? Peso { get; set; }
        public DateTime? Data { get; set; }
        public string NumeroPedido { get; set; }
    }
}