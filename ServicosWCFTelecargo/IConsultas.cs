using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web.Services;

namespace ServicosWCFTelecargo
{ 

    [ServiceContract]

    [WebService(Namespace = "ServicosWCFTelecargo", Description = " <b> Serviços Web Telecargo </ b > ")] 
    public interface IConsultas
    {
        [OperationContract]
        string Rastreamento(string login, string senha, string dataInicial, string dataFinal, int codigoRemetente, int codigoDestinatario, string entregue, string numeroNota, string numeroConhecimento, int codigoCentroCusto, string ufOrigem, int cidadeOrigem, string ufDestino, int cidadeDestino, int codigoFinanceiro);

        [OperationContract]
        string RastreamentoComModal(string login, string senha, string dataInicial, string dataFinal, int codigoRemetente, int codigoDestinatario, string entregue, string numeroNota, string numeroConhecimento, int codigoCentroCusto, string ufOrigem, int cidadeOrigem, string ufDestino, int cidadeDestino, int codigoFinanceiro, string modal = "");

        [OperationContract]
        string Embarque(string login, string senha, string dataInicial, string dataFinal, int codigoRemetente, int codigoDestinatario, string entregue, string numeroNota, string numeroConhecimento, int codigoCentroCusto, string ufOrigem, int cidadeOrigem, string ufDestino, int cidadeDestino);

        [OperationContract]
        string EmbarqueComModal(string login, string senha, string dataInicial, string dataFinal, int codigoRemetente, int codigoDestinatario, string entregue, string numeroNota, string numeroConhecimento, int codigoCentroCusto, string ufOrigem, int cidadeOrigem, string ufDestino, int cidadeDestino, string modal = "");

        [OperationContract]
        string DetalheEmbarque(string login, string senha, int idMovimento);

        [OperationContract]
        string OcorrenciasNovo(string codigoMovimento);

        [OperationContract]
        string RetornarLoginCliente(string login, string senha);

        [OperationContract]
        string ListarCentroCusto(string login, string senha, int codigoRemetente);

        [OperationContract]
        string ListarCidade(string login, string senha, string estado, string cidade);

        [OperationContract]
        string ListarPessoa(string login, string senha, bool empresa, string cgcCPF, string nome, int codigoPessoa);

        [OperationContract]
        string ConsultarFinanceiro(string login, string senha, string tipoDocumento, string numeroDocumento, string dataVencimentoInicial, string dataVencimentoFinal, string dataEmissaoInicial, string dataEmissaoFinal, int codigoEmpresaEmissora);
    }
}
