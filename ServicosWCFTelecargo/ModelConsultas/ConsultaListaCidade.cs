using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace ServicosWCFTelecargo.ModelConsultas
{
    [XmlType("ListaCidade")]
    public class ConsultaListaCidade
    {
        public int idCidade { get; set; }
        public string Cidade { get; set; }
        public string CidadeSigla { get; set; }
        public int idEstado { get; set; }
        public string EstadoDescricao { get; set; }
        public string UF { get; set; }
    }
}