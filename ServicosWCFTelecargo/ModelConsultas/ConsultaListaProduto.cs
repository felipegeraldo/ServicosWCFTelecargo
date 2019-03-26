using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicosWCFTelecargo.ModelConsultas
{
    public class ConsultaListaProduto
    {
        public string CodigoProduto { get; set; }
        public string DescricaoProduto { get; set; }
        public decimal? Quantidade { get; set; }
        public string URLFoto { get; set; }
    }
}