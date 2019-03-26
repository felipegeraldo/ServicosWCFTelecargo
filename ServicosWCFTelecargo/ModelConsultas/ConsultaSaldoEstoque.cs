using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace ServicosWCFTelecargo.ModelConsultas
{
    [XmlType("SaldoEstoque")]
    public class ConsultaSaldoEstoque
    {
        [XmlIgnore]
        public int id_Produto { get; set; }
        public string CodigoProduto { get; set; }
        public string DescricaoProduto { get; set; }
        public decimal? QuantidadeEstoque { get; set; }
        public string Campanha { get; set; }
        public string URLFoto { get; set; }
    }
}