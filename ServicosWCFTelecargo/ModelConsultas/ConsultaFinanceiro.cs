using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace ServicosWCFTelecargo.ModelConsultas
{
    [XmlType("ConsultarFinanceiro")]
    public class ConsultaFinanceiro
    {
        public int IDFinanceiro { get; set; }
	    public string Cliente  { get; set; }
	    public string Documento  { get; set; }
	    public DateTime? Emissao  { get; set; }
	    public DateTime? Vencimento  { get; set; }
	    public decimal? ValorPrincipal  { get; set; }
        public decimal? ValorJuros { get; set; }
        public decimal? ValorDesconto { get; set; }
        public decimal? ValorTitulo { get; set; }
        public decimal? ValorSaldo { get; set; }
	    public DateTime? VencimentoOriginal  { get; set; }
	    public string UltimaBaixa { get; set; }
	    public string Natureza  { get; set; }
        public string CaminhoBoleto { get; set; }
    }
}