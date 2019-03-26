using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace ServicosWCFTelecargo.ModelConsultas
{
    [XmlType("Ocorrencias")]
    public class ConsultaOcorrencia
    {

        public int IdOcorrencia { get; set; }
        public string NotaFiscal { get; set; }
        public string Serie { get; set; }
        public string NumeroPedido { get; set; }
        public string CodigoOcorrencia { get; set; }
        public string DescricaoOcorrencia { get; set; }
        public DateTime? DataOcorrencia { get; set; }
        public string HoraOcorrencia { get; set; }
        public string ComentarioOcorrencia { get; set; }
    }
}