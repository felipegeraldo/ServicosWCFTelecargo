using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace ServicosWCFTelecargo.ModelConsultas
{
    [XmlType("DetalheEmbarque")]
    public class ConsultaEmbarqueDetalhe
    {
        [XmlIgnore]
        public int ID { get; set; }
        public string Remetente { get; set; }
        public string CidadeOrigem { get; set; }
        public string Destinatario { get; set; }
        public string CidadeDestino { get; set; }
        public string ClienteFaturado { get; set; }
        public DateTime? DataEmissao { get; set; }
        public string Conhecimento { get; set; }
        public string Minuta { get; set; }
        public string Referencia { get; set; }
        public decimal? PesoReal { get; set; }
        public decimal? PesoTaxado { get; set; }
        public string Natureza { get; set; }
        public string TipoMovimento { get; set; }
        public string FormaPagamento { get; set; }
        public string Comentario { get; set; }
        public string ComprovanteEntrega { get; set; }
        public string ComprovanteEntrega2 { get; set; }

        [XmlElementAttribute("NotaFiscal")]
        public List<ListaNotaFiscal> ListaNotaFiscal { get; set; }

        [XmlElementAttribute("Coleta")]
        public List<ListaColeta> ListaColeta { get; set; }

        [XmlElementAttribute("Manifesto")]
        public List<ListaManifesto> ListaManifesto { get; set; }

        [XmlElementAttribute("Voo")]
        public List<ListaDadosVoo> ListaDadosVoo { get; set; }

        [XmlElementAttribute("Consolidacao")]
        public List<ListaConsolidacao> ListaConsolidacao { get; set; }

        [XmlElementAttribute("Entrega")]
        public List<ListaEntrega> ListaEntrega { get; set; }

        [XmlElementAttribute("Ocorrencia")]
        public List<ListaOcorrencia> ListaOcorrencia { get; set; }

        [XmlElementAttribute("Tracking")]
        public List<ListaTracking> ListaTracking { get; set; }

        [XmlElementAttribute("ComposicaoFrete")]
        public List<ListaComposicaoFrete> ListaComposicaoFrete { get; set; }
    }
}