using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace ServicosWCFTelecargo.ModelConsultas
{
    [XmlType("ListaPessoa")]
    public class ConsultaListaPessoa
    {
        public int idPessoa { get; set; }
        public string Nome { get; set; }
        public string CGCCPF { get; set; }
        public string Endereco { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string CEP { get; set; }
    }
}