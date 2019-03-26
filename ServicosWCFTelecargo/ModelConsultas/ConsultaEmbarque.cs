using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace ServicosWCFTelecargo.ModelConsultas
{
    [XmlType("Embarque")]
    public class ConsultaEmbarque
    {
        public int IDMovimento { get; set; }
        public string Categoria { get; set; }
        public DateTime? DataColeta { get; set; }
        public string Conhecimento { get; set; }
        public string Minuta { get; set; }
        public string CGCCPFRemetente { get; set; }
        public string Remetente { get; set; }
        public string CGCCPFDestinatario { get; set; }
        public string Destinatario { get; set; }
        public string CidadeOrigem { get; set; }
        public string UFOrigem { get; set; }
        public string CidadeDestino { get; set; }
        public string UFDestino { get; set; }
        public string NotaFiscal { get; set; }
        public string ClienteFaturado { get; set; }
        public decimal? ValorNotaFiscal { get; set; }
        public int Volume { get; set; }
        public decimal? PesoReal { get; set; }
        public decimal? PesoTaxado { get; set; }
        public decimal? ValorFrete { get; set; }
        public decimal? ValorICMS { get; set; }
        public decimal? PorcentagemICMS { get; set; }
        public string TipoMovimento { get; set; }
        public string CGCCPFFaturado { get; set; }
        public string Faturado { get; set; }
        public string ChaveCTe { get; set; }
        public DateTime? PrevisaoEntrega { get; set; }
        public DateTime? DataEntrega { get; set; }
        public string UltimaOcorrencia { get; set; }
        public DateTime? DataUltimaOcorrencia { get; set; }
        public string NomeRecebedor { get; set; }
        public string CentroCusto { get; set; }
        public string ComprovanteEntrega { get; set; }
        public string ComprovanteEntrega2 { get; set; }

        [XmlElementAttribute("Comprovantes")]
        public List<ListaComprovante> ListaComprovante { get; set; }
    }
}