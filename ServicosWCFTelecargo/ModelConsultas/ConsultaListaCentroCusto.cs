using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace ServicosWCFTelecargo.ModelConsultas
{
    [XmlType("ListaCentroCusto")]
    public class ConsultaListaCentroCusto
    {
        public int idRemetenteCentroCusto { get; set; }
        public int idRemetente { get; set; }
        public string CentroCusto { get; set; }
        public int? idTransportadoraServico { get; set; }

    }
}