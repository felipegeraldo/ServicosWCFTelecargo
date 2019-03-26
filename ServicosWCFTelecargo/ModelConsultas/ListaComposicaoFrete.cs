using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicosWCFTelecargo.ModelConsultas
{
    public class ListaComposicaoFrete
    {
        public decimal? FretePeso { get; set; }
        public decimal? Coleta { get; set; }
        public decimal? Entrega { get; set; }
        public decimal? Redespacho { get; set; }
        public decimal? Emergencia { get; set; }
        public decimal? TDA { get; set; }
        public decimal? TTD { get; set; }
        public decimal? TaxasDiversas { get; set; }
        public decimal? ADValorem { get; set; }
        public decimal? Suframa { get; set; }
        public decimal? GRIS { get; set; }
        public decimal? ISS { get; set; }
        public decimal? ICMS { get; set; }
        public decimal? Acrescimo { get; set; }
        public decimal? Descontos { get; set; }
        public decimal? Pedagio { get; set; }
        public decimal? Despacho { get; set; }
        public decimal? SETCAT { get; set; }
        public decimal? ITR { get; set; }
        public decimal? Total { get; set; }
    }
}