using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicosWCFTelecargo.ModelConsultas
{
    public class ListaManifesto
    {
        public int Manifesto { get; set; }
        public string Motorista { get; set; }
        public string Veiculo { get; set; }
        public string DataSaida { get; set; }
        public string DataChegada { get; set; }
        public string Origem { get; set; }
        public string Rota { get; set; }
        public string TipoManifesto { get; set; }
    }
}