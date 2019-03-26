using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicosWCFTelecargo.ModelConsultas
{
    public class ListaDadosVoo
    {
        public string CiaAerea { get; set; }
        public string NumeroVoo { get; set; }
        public string DataSaida { get; set; }
        public string DataChegada { get; set; }
        public string Confirmado { get; set; }
    }
}