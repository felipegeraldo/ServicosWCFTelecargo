using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicosWCFTelecargo.ModelConsultas
{
    public class ListaConsolidacao
    {
        public string Transportadora { get; set; }
        public string CTRC3 { get; set; }
        public string DataSaida { get; set; }
        public string DataChegada { get; set; }
        public string Confirmado { get; set; }
    }
}