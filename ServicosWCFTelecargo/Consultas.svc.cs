using BibliotecasFramework;
using ServicosWCFTelecargo.ModelConsultas;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web.Configuration;
using System.Data;
using System.Globalization;
using System.Threading;
using System.Net;

namespace ServicosWCFTelecargo
{
    public class Consultas : IConsultas
    {
        #region Membros de Serviço
        public string Rastreamento(string login, string senha, string dataInicial, string dataFinal, int codigoRemetente, int codigoDestinatario, string entregue, string numeroNota, string numeroConhecimento, int codigoCentroCusto, string ufOrigem, int cidadeOrigem, string ufDestino, int cidadeDestino, int codigoFinanceiro)
        {
            var listaRastreamento = new List<ConsultaRastreamento>();
            try
            {
                var Validacao = ValidaRastreamento(login, senha, dataInicial, dataFinal, codigoRemetente, codigoDestinatario, entregue, numeroNota, numeroConhecimento, codigoCentroCusto, ufOrigem, cidadeOrigem, ufDestino, cidadeDestino, codigoFinanceiro);
                if (Validacao != "")
                    return Validacao;

                var selectRastreamento = RetornaSelectRastreamento(codigoFinanceiro);
                this.MontarWhereRastreamento(selectRastreamento, login, senha, dataInicial, dataFinal, codigoRemetente, codigoDestinatario, entregue, numeroNota, numeroConhecimento, codigoCentroCusto, ufOrigem, cidadeOrigem, ufDestino, cidadeDestino, codigoFinanceiro, "");

                using (var sqlConnection = new SqlConnection(this.GetConnectionString))
                {
                    sqlConnection.Open();

                    var rastreios = this.ObterRastreamentoDoBanco(selectRastreamento, sqlConnection, login, senha, dataInicial, dataFinal, codigoRemetente, codigoDestinatario, entregue, numeroNota, numeroConhecimento, codigoCentroCusto, ufOrigem, cidadeOrigem, ufDestino, cidadeDestino, codigoFinanceiro);
                    //this.carregarComprovanteRatreamento(sqlConnection, rastreios);

                    return ManipularXML.GerarXMLEntidade("", rastreios);
                }
            }
            catch (Exception ex)
            {
                return "Erro: Não foi possível obter os dados dos rastreamentos. " + ex.Message;
            }
        }

        public string RastreamentoComModal(string login, string senha, string dataInicial, string dataFinal, int codigoRemetente, int codigoDestinatario, string entregue, string numeroNota, string numeroConhecimento, int codigoCentroCusto, string ufOrigem, int cidadeOrigem, string ufDestino, int cidadeDestino, int codigoFinanceiro, string modal = "")
        {
            var listaRastreamento = new List<ConsultaRastreamento>();
            try
            {
                var Validacao = ValidaRastreamento(login, senha, dataInicial, dataFinal, codigoRemetente, codigoDestinatario, entregue, numeroNota, numeroConhecimento, codigoCentroCusto, ufOrigem, cidadeOrigem, ufDestino, cidadeDestino, codigoFinanceiro);
                if (Validacao != "")
                    return Validacao;

                var selectRastreamento = RetornaSelectRastreamento(codigoFinanceiro);
                this.MontarWhereRastreamento(selectRastreamento, login, senha, dataInicial, dataFinal, codigoRemetente, codigoDestinatario, entregue, numeroNota, numeroConhecimento, codigoCentroCusto, ufOrigem, cidadeOrigem, ufDestino, cidadeDestino, codigoFinanceiro, modal);

                using (var sqlConnection = new SqlConnection(this.GetConnectionString))
                {
                    sqlConnection.Open();

                    var rastreios = this.ObterRastreamentoDoBanco(selectRastreamento, sqlConnection, login, senha, dataInicial, dataFinal, codigoRemetente, codigoDestinatario, entregue, numeroNota, numeroConhecimento, codigoCentroCusto, ufOrigem, cidadeOrigem, ufDestino, cidadeDestino, codigoFinanceiro);
                    //this.carregarComprovanteRatreamento(sqlConnection, rastreios);

                    return ManipularXML.GerarXMLEntidade("", rastreios);
                }
            }
            catch (Exception ex)
            {
                return "Erro: Não foi possível obter os dados dos rastreamentos. " + ex.Message;
            }
        }

        public string Embarque(string login, string senha, string dataInicial, string dataFinal, int codigoRemetente, int codigoDestinatario, string entregue, string numeroNota, string numeroConhecimento, int codigoCentroCusto, string ufOrigem, int cidadeOrigem, string ufDestino, int cidadeDestino)
        {
            try
            {
                var Validacao = ValidaEmbarque(login, senha, dataInicial, dataFinal, codigoRemetente, codigoDestinatario, entregue, numeroNota, numeroConhecimento, codigoCentroCusto, ufOrigem, cidadeOrigem, ufDestino, cidadeDestino);
                if (Validacao != "")
                    return Validacao;

                var selectEmbarque = RetornaSelectEmbarque();
                this.MontarWhereEmbarque(selectEmbarque, login, senha, dataInicial, dataFinal, codigoRemetente, codigoDestinatario, entregue, numeroNota, numeroConhecimento, codigoCentroCusto, ufOrigem, cidadeOrigem, ufDestino, cidadeDestino, "");

                using (var sqlConnection = new SqlConnection(this.GetConnectionString))
                {
                    sqlConnection.Open();

                    var embarque = this.ObterEmbarqueDoBanco(selectEmbarque, sqlConnection, login, senha, dataInicial, dataFinal, codigoRemetente, codigoDestinatario, entregue, numeroNota, numeroConhecimento, codigoCentroCusto, ufOrigem, cidadeOrigem, ufDestino, cidadeDestino);
                    this.CarregarComprovanteEmbarque(sqlConnection, embarque);

                    return ManipularXML.GerarXMLEntidade("", embarque);
                }
            }
            catch (Exception ex)
            {
                return "Erro: Não foi possível obter os dados dos embarques." + ex.Message;
            }
        }

        public string EmbarqueComModal(string login, string senha, string dataInicial, string dataFinal, int codigoRemetente, int codigoDestinatario, string entregue, string numeroNota, string numeroConhecimento, int codigoCentroCusto, string ufOrigem, int cidadeOrigem, string ufDestino, int cidadeDestino, string modal = "")
        {
            try
            {
                var Validacao = ValidaEmbarque(login, senha, dataInicial, dataFinal, codigoRemetente, codigoDestinatario, entregue, numeroNota, numeroConhecimento, codigoCentroCusto, ufOrigem, cidadeOrigem, ufDestino, cidadeDestino);
                if (Validacao != "")
                    return Validacao;

                var selectEmbarque = RetornaSelectEmbarque();
                this.MontarWhereEmbarque(selectEmbarque, login, senha, dataInicial, dataFinal, codigoRemetente, codigoDestinatario, entregue, numeroNota, numeroConhecimento, codigoCentroCusto, ufOrigem, cidadeOrigem, ufDestino, cidadeDestino, modal);

                using (var sqlConnection = new SqlConnection(this.GetConnectionString))
                {
                    sqlConnection.Open();

                    var embarque = this.ObterEmbarqueDoBanco(selectEmbarque, sqlConnection, login, senha, dataInicial, dataFinal, codigoRemetente, codigoDestinatario, entregue, numeroNota, numeroConhecimento, codigoCentroCusto, ufOrigem, cidadeOrigem, ufDestino, cidadeDestino);
                    this.CarregarComprovanteEmbarque(sqlConnection, embarque);

                    return ManipularXML.GerarXMLEntidade("", embarque);
                }
            }
            catch (Exception ex)
            {
                return "Erro: Não foi possível obter os dados dos embarques." + ex.Message;
            }
        }

        public string DetalheEmbarque(string login, string senha, int idMovimento)
        {
            try
            {
                var Validacao = ValidaEmbarqueDetalhe(login, senha, idMovimento);
                if (Validacao != "")
                    return Validacao;

                var selectEmbarqueDetalhe = RetornaSelectEmbarqueDetalhe();
                this.MontarWhereEmbarqueDetalhe(selectEmbarqueDetalhe, login, senha, idMovimento);

                using (var sqlConnection = new SqlConnection(this.GetConnectionString))
                {
                    sqlConnection.Open();

                    var detalheEmbarque = this.ObterEmbarqueDetalheDoBanco(selectEmbarqueDetalhe, sqlConnection, login, senha, idMovimento);
                    this.CarregarNotaFiscal(sqlConnection, detalheEmbarque);
                    this.CarregarColeta(sqlConnection, detalheEmbarque);
                    this.CarregarManifesto(sqlConnection, detalheEmbarque);
                    this.CarregarVoo(sqlConnection, detalheEmbarque);
                    this.CarregarConsolidacao(sqlConnection, detalheEmbarque);
                    this.CarregarEntrega(sqlConnection, detalheEmbarque);
                    this.CarregarOcorrenciaDetalhe(sqlConnection, detalheEmbarque);
                    this.CarregarTracking(sqlConnection, detalheEmbarque);
                    this.CarregarComposicaoFrete(sqlConnection, detalheEmbarque);

                    return ManipularXML.GerarXMLEntidade("", detalheEmbarque);
                }
            }
            catch (Exception ex)
            {
                return "Erro: Não foi possível obter os dados de detalhe do embarque." + ex.Message;
            }
        }

        public string OcorrenciasNovo(string codigoMovimento)
        {
            return RegraOcorrencia(codigoMovimento);
        }

        public int ValidarLoginCliente(string login, string senha)
        {
            CultureInfo ci = new CultureInfo("pt-BR");
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;

            int idCliente = 0;

            if (login != "" && senha != "")
            {
                SqlConnection sqlConnection = new SqlConnection(this.GetConnectionString);

                idCliente = LoginPessoa(login, senha, sqlConnection);
                if (idCliente <= 0)
                {
                    idCliente = LoginPessoaContato(login, senha, sqlConnection);
                }
            }

            return idCliente;
        }

        public string RetornarLoginCliente(string login, string senha)
        {
            int idCliente = 0;

            SqlConnection sqlConnection = new SqlConnection(this.GetConnectionString);

            idCliente = LoginPessoa(login, senha, sqlConnection);
            if (idCliente <= 0)
            {
                idCliente = LoginPessoaContato(login, senha, sqlConnection);
            }

            if (idCliente > 0)
            {
                var cliente = new ConsultaListaPessoa() { idPessoa = idCliente };
                var listaCliente = new List<ConsultaListaPessoa>() { cliente };

                return ManipularXML.GerarXMLEntidade("", listaCliente);
            }
            else
            {
                return "Erro: Login e/ou Senha inválido(s)!";
            }
        }

        public string ListarCentroCusto(string login, string senha, int idRemetente)
        {
            var listaCentroCusto = new List<ConsultaListaCentroCusto>();

            try
            {
                var Validacao = ValidaCentroCusto(login, senha, idRemetente);
                if (Validacao != "")
                    return Validacao;

                using (var sqlConnection = new SqlConnection(this.GetConnectionString))
                {
                    sqlConnection.Open();

                    var centroCusto = this.ObterCentroCustoDoBanco(login, senha, idRemetente, sqlConnection);

                    return ManipularXML.GerarXMLEntidade("", centroCusto);
                }
            }
            catch (Exception ex)
            {
                return "Erro: Não foi possível obter os dados dos centros de custo." + ex.Message;
            }
        }

        public string ListarCidade(string login, string senha, string estado, string cidade)
        {
            try
            {
                var validar = ValidarConsultaCidade(login, senha, cidade);
                if (validar != "")
                    return validar;

                var selectCidade = RetornaSelectCidade();
                this.MontarWhereCidade(selectCidade, estado, cidade);

                using (var sqlConnection = new SqlConnection(this.GetConnectionString))
                {
                    sqlConnection.Open();

                    var cidades = this.ObterCidadeDoBanco(selectCidade, sqlConnection, cidade, estado);

                    return ManipularXML.GerarXMLEntidade("", cidades);
                }

            }
            catch (Exception ex)
            {
                return "Erro: Não foi possível obter os dados das cidades." + ex.Message;
            }
        }

        public string ListarPessoa(string login, string senha, bool empresa, string cgcCPF, string nome, int idPessoa)
        {
            try
            {
                var validar = ValidarConsultaPessoa(login, senha, empresa, cgcCPF, nome, idPessoa);
                if (validar != "")
                    return validar;

                var selectPessoa = RetornaSelectPessoa(empresa);
                this.MontarWherePessoa(selectPessoa, empresa, cgcCPF, nome, idPessoa);

                using (var sqlConnection = new SqlConnection(this.GetConnectionString))
                {
                    sqlConnection.Open();

                    var pessoas = this.ObterPessoaDoBanco(selectPessoa, sqlConnection, cgcCPF, nome, idPessoa);

                    return ManipularXML.GerarXMLEntidade("", pessoas);
                }

            }
            catch (Exception ex)
            {
                return "Erro: Não foi possível obter os dados das pessoas." + ex.Message;
            }
        }

        public string ConsultarFinanceiro(string login, string senha, string tipoDocumento, string numeroDocumento, string dataVencimentoInicial, string dataVencimentoFinal, string dataEmissaoInicial, string dataEmissaoFinal, int idEmpresaEmissora)
        {
            try
            {
                var validar = ValidarConsultaFinanceiro(login, senha, tipoDocumento, numeroDocumento, dataVencimentoInicial, dataVencimentoFinal, dataEmissaoInicial, dataEmissaoFinal, idEmpresaEmissora);
                if (validar != "")
                    return validar;

                var selectFinanceiro = RetornaSelectFinanceiro();
                this.MontarWhereFinanceiro(selectFinanceiro, login, senha, tipoDocumento, numeroDocumento, dataVencimentoInicial, dataVencimentoFinal, dataEmissaoInicial, dataEmissaoFinal, idEmpresaEmissora);

                using (var sqlConnection = new SqlConnection(this.GetConnectionString))
                {
                    sqlConnection.Open();

                    var pessoas = this.ObterFinanceiroDoBanco(selectFinanceiro, sqlConnection, login, senha, tipoDocumento, numeroDocumento, dataVencimentoInicial, dataVencimentoFinal, dataEmissaoInicial, dataEmissaoFinal, idEmpresaEmissora);

                    return ManipularXML.GerarXMLEntidade("", pessoas);
                }

            }
            catch (Exception ex)
            {
                return "Erro: Não foi possível obter os dados do financeiro." + ex.Message;
            }
        }

        #endregion

        #region Propriedades
        private string GetConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["ConexaoCliente"].ConnectionString;
            }
        }

        private string GetUrlBoleto
        {
            get
            {
                return WebConfigurationManager.AppSettings["urlBoleto"];
            }
        }

        private string GetUrlComprovante
        {
            get
            {
                return WebConfigurationManager.AppSettings["urlComprovante"];
            }
        }

        private string GetPathComprovanteLocal
        {
            get
            {
                return WebConfigurationManager.AppSettings["PathComprovanteLocal"];
            }
        }

        private string GetPathBoleto
        {
            get
            {
                return WebConfigurationManager.AppSettings["PathBoleto"];
            }
        }

        private string GetQuantidadeMesesAnteriores
        {
            get
            {
                return WebConfigurationManager.AppSettings["QuantidadeMesesAnteriores"];
            }
        }

        #endregion

        #region Funções Genericas
        private void MontarWhere(StringBuilder stringBuilder, List<string> where)
        {
            if (where.Count != 0)
            {
                stringBuilder.Append(" where ");
                stringBuilder.Append(string.Join(" and ", where));
            }
        }

        private string RetornaURLComprovanteDigitalizado(string id_Movimento)
        {
            string sURLComprovante = RetornaNomeArquivoAnexo(this.GetPathComprovanteLocal, this.GetUrlComprovante, id_Movimento, "a.jpg");
            if (string.IsNullOrEmpty(sURLComprovante))
            {
                sURLComprovante = RetornaNomeArquivoAnexo(this.GetPathComprovanteLocal, this.GetUrlComprovante, id_Movimento, ".pdf");
            }
            return sURLComprovante;
        }

        private string RetornaNomeArquivoAnexo(string ds_CaminhoArquivo, string ds_URLExterna, string id_Tabela, string ds_Extencao)
        {
            if (string.IsNullOrEmpty(id_Tabela))
                return "id Vazio";

            string[] nomeArquivo = new string[] { "", "a", "b", "c", "d", "e", "m" };

            foreach (var item in nomeArquivo)
            {
                if (VerificaArquivo(ds_CaminhoArquivo + id_Tabela + item.ToString() + ds_Extencao))
                {

                    return ds_URLExterna + id_Tabela + item.ToString() + ds_Extencao;
                }
            }

            return "Caminho Pesquisado: " + ds_CaminhoArquivo + " id_Tabela: " + id_Tabela.ToString() + " Extencao: " + ds_Extencao;
        }

        private bool VerificaArquivo(string PathArquivo)
        {
            return File.Exists(PathArquivo);
        }

        private DateTime DataInicialValida(DateTime DataFinal)
        {
            int QtdMesesAnteriores;

            QtdMesesAnteriores = Int32.Parse(this.GetQuantidadeMesesAnteriores) * -1;

            DateTime dataInicial = DataFinal.AddMonths(QtdMesesAnteriores);

            dataInicial = new DateTime(dataInicial.Year, dataInicial.Month, 1);

            return dataInicial;
        }

        private Erro RetornaErro(String sErro)
        {
            var erro = new Erro() { ds_Erro = sErro };
            return erro;
        }

        #endregion

        #region Funções Login

        private int LoginPessoaContato(string login, string senha, SqlConnection sqlConnection)
        {
            int idCliente = 0;

            if (login != "" && senha != "")
            {
                using (var sqlCommand = new SqlCommand("Select id_Pessoa from tbdPessoaContato (nolock) where ds_WebLogin = @login AND ds_WebSenha = @senha", sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@login", login);
                    sqlCommand.Parameters.AddWithValue("@senha", senha);

                    sqlConnection.Open();

                    using (var sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        if (sqlDataReader.Read())
                        {
                            idCliente = (int)sqlDataReader["id_Pessoa"];
                        }
                    }

                    sqlConnection.Close();
                }
            }

            return idCliente;
        }

        private int LoginPessoa(string login, string senha, SqlConnection sqlConnection)
        {
            int idCliente = 0;

            if (login != "" && senha != "")
            {
                using (var sqlCommand = new SqlCommand("Select id_Pessoa from tbdPessoa (nolock) where ds_WebLogin = @login AND ds_WebSenha = @senha", sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@login", login);
                    sqlCommand.Parameters.AddWithValue("@senha", senha);

                    sqlConnection.Open();

                    using (var sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        if (sqlDataReader.Read())
                        {
                            idCliente = (int)sqlDataReader["id_Pessoa"];
                        }
                    }

                    sqlConnection.Close();
                }
            }

            return idCliente;
        }

        #endregion

        #region Funções Consulta Ocorrencia

        private string RegraOcorrencia(string codigoMovimento)
        {
            try
            {
                var Validacao = ValidaOcorrencia(codigoMovimento);
                if (Validacao != "")
                    return Validacao;

                var selectOcorrencias = RetornaSelectOcorrencia();
                this.MontarWhereOcorrencia(selectOcorrencias, codigoMovimento);

                using (var sqlConnection = new SqlConnection(this.GetConnectionString))
                {
                    sqlConnection.Open();

                    var ocorrencias = this.ObterOcorrenciasDoBanco(selectOcorrencias, sqlConnection, codigoMovimento);

                    return ManipularXML.GerarXMLEntidade("", ocorrencias);
                }
            }
            catch (Exception ex)
            {
                return "Erro: " + ex.Message;
            }

        }

        private string ValidaOcorrencia(string codigoMovimento)
        {

            if (string.IsNullOrEmpty(codigoMovimento))
                return "Erro: Informe o código do movimento";

            return "";
        }

        private StringBuilder RetornaSelectOcorrencia()
        {
            var selectOcorrencias = new StringBuilder(@"select
                                        rtrim(MovNF.cd_NotaFiscal) as 'NotaFiscal',
                                        rtrim(MovNF.cd_Serie) as 'Serie',
                                        rtrim(MovNF.cd_ServicoNotaFiscal) as 'NumeroPedido',
                                        OcorrNota.id_OcorrenciaNota as 'idOcorrencia',
                                        rtrim(Ocorr.nr_Codigo) as 'CodigoOcorrencia',
                                        rtrim(Ocorr.ds_Ocorrencia) as 'DescricaoOcorrencia',
                                        rtrim(OcorrCli.cd_OcorrenciaCliente) as 'CodigoOcorrenciaCliente',
                                        rtrim(OcorrCli.ds_OcorrenciaCliente) as 'DescricaoOcorrenciaCliente',
                                        OcorrNota.dt_PrazoFechamento as 'DataOcorrencia',
                                        rtrim(OcorrNota.hr_PrazoFechamento) as 'HoraOcorrencia',
                                        rtrim(OcorrNota.cm_Fechamento) as 'ComentarioOcorrencia'
                                    from tbdOcorrenciaNota OcorrNota (nolock) 
                                    left join tbdMovimentoNotaFiscal MovNF (nolock) on MovNF.id_Movimento = OcorrNota.id_Movimento and OcorrNota.nr_NotaFiscal = MovNF.cd_Notafiscal
                                    left join tbdMovimento Mov (nolock) on MovNF.id_Movimento = Mov.id_Movimento 
                                    left join tbdOcorrencia Ocorr (nolock) on OcorrNota.id_Ocorrencia = Ocorr.id_Ocorrencia
                                    left join tbdOcorrenciaCliente OcorrCli (nolock) on Ocorr.id_Ocorrencia = OcorrCli.id_Ocorrencia and OcorrCli.id_Pessoa = Mov.id_Remetente");
            return selectOcorrencias;
        }

        private void MontarWhereOcorrencia(StringBuilder selectOcorrencias, string codigoMovimento)
        {
            var where = new List<string>();

            if (!string.IsNullOrEmpty(codigoMovimento))
                where.Add("Mov.id_Movimento = @codigoMovimento");

            MontarWhere(selectOcorrencias, where);
        }

        private ConsultaOcorrencia RetornaOcorrencia(SqlDataReader sqlDataReader)
        {
            var ocorrencia = new ConsultaOcorrencia();

            var cd_Ocorrencia = sqlDataReader["CodigoOcorrencia"] as string;
            var ds_Ocorrencia = sqlDataReader["DescricaoOcorrencia"] as string;
            var cd_OcorrenciaCliente = sqlDataReader["CodigoOcorrenciaCliente"] as string;
            var ds_OcorrenciaCliente = sqlDataReader["DescricaoOcorrenciaCliente"] as string;

            ocorrencia.NotaFiscal = sqlDataReader["NotaFiscal"] as string;
            ocorrencia.Serie = sqlDataReader["Serie"] as string;
            ocorrencia.NumeroPedido = sqlDataReader["NumeroPedido"] as string;
            ocorrencia.IdOcorrencia = (int)sqlDataReader["IdOcorrencia"];

            if (string.IsNullOrEmpty(cd_OcorrenciaCliente))
            {
                ocorrencia.CodigoOcorrencia = cd_Ocorrencia;
            }
            else
            {
                ocorrencia.CodigoOcorrencia = cd_OcorrenciaCliente;
            }

            if (string.IsNullOrEmpty(ds_OcorrenciaCliente))
            {
                ocorrencia.DescricaoOcorrencia = ds_Ocorrencia;
            }
            else
            {
                ocorrencia.DescricaoOcorrencia = ds_OcorrenciaCliente;
            }

            ocorrencia.DataOcorrencia = sqlDataReader["DataOcorrencia"] as DateTime?;
            ocorrencia.HoraOcorrencia = sqlDataReader["HoraOcorrencia"] as string;
            ocorrencia.ComentarioOcorrencia = sqlDataReader["ComentarioOcorrencia"] as string;

            return ocorrencia;
        }

        private void AdicionarParametrosOcorrencia(SqlCommand sqlCommand, string codigoMovimento)
        {

            sqlCommand.Parameters.Add(new SqlParameter("codigoMovimento", codigoMovimento));

        }

        private List<ConsultaOcorrencia> ObterOcorrenciasDoBanco(StringBuilder selectOcorrencias, SqlConnection sqlConnection, string codigoMovimento)
        {
            var ocorrencias = new List<ConsultaOcorrencia>();

            using (var sqlCommand = new SqlCommand(selectOcorrencias.ToString(), sqlConnection))
            {
                this.AdicionarParametrosOcorrencia(sqlCommand, codigoMovimento);

                using (var sqlDataReader = sqlCommand.ExecuteReader())
                {
                    while (sqlDataReader.Read())
                    {
                        var ocorrencia = RetornaOcorrencia(sqlDataReader);
                        ocorrencias.Add(ocorrencia);
                    }
                }
            }

            return ocorrencias;
        }
        #endregion

        #region Funções Produto
        //private void carregarProdutos(SqlConnection sqlConnection, List<ConsultaOcorrencia> ocorrencias)
        //{
        //    foreach (var ocorrencia in ocorrencias)
        //        this.carregarProdutos(sqlConnection, ocorrencia);
        //}

        //private void carregarProdutos(SqlConnection sqlConnection, ConsultaOcorrencia ocorrencia)
        //{
        //    ocorrencia.ListaProduto = new List<ConsultaListaProduto>();
        //    var selectProdutos = retornaSelectProduto();

        //    using (var sqlCommand = new SqlCommand(selectProdutos.ToString(), sqlConnection))
        //    {
        //        AcionaParametroProduto(ocorrencia, sqlCommand);

        //        using (var sqlDataReader = sqlCommand.ExecuteReader())
        //        {
        //            while (sqlDataReader.Read())
        //            {
        //                var produto = this.retornaProduto(sqlDataReader);
        //                ocorrencia.ListaProduto.Add(produto);
        //            }
        //        }
        //    }
        //}

        //private static void AcionaParametroProduto(ConsultaOcorrencia ocorrencia, SqlCommand sqlCommand)
        //{
        //    sqlCommand.Parameters.AddWithValue("Id", ocorrencia.Id);
        //    sqlCommand.Parameters.AddWithValue("NotaFiscal", string.IsNullOrEmpty(ocorrencia.NotaFiscal) ? string.Empty : ocorrencia.NotaFiscal.TrimStart('0'));
        //}

        //private StringBuilder retornaSelectProduto()
        //{
        //    var selectProduto = new StringBuilder(@"select
        //                                                Prod.id_Produto as 'id_Produto',
        //                                             rtrim(Prod.cd_Produto) as 'CodigoProduto', 
        //                                             rtrim(Prod.ds_Produto) as 'DescricaoProduto',
        //                                             NFItem.qt_Produto as 'Quantidade', '" +
        //                                                this.urlBoleto + @"' + convert(varchar(50), Prod.id_Produto) + '.jpg' as 'URLFoto'
        //                                            from tbdImportacaoNFEstoque Import (nolock)
        //                                            left join tbdNotaFiscal NF (nolock) on Import.id_NotaFiscal = NF.id_NotaFiscal
        //                                            left join tbdNotaFiscalItem NFItem (nolock) on NF.id_NotaFiscal = NFItem.id_NotaFiscal
        //                                            left join tbdProduto Prod (nolock) on NFItem.id_Produto = Prod.id_Produto
        //                                            where Import.id_Movimento = @Id and NF.cd_NotaFiscal = @NotaFiscal");
        //    return selectProduto;
        //}

        //private ConsultaListaProduto retornaProduto(SqlDataReader sqlDataReader)
        //{
        //    var produto = new ConsultaListaProduto();

        //    int? id_Produto = sqlDataReader["id_Produto"] as int?;

        //    produto.CodigoProduto = sqlDataReader["CodigoProduto"] as string;
        //    produto.DescricaoProduto = sqlDataReader["DescricaoProduto"] as string;
        //    produto.Quantidade = sqlDataReader["Quantidade"] as decimal?;
        //    produto.URLFoto = retornaURLFotoProduto(id_Produto.ToString());

        //    return produto;
        //}
        #endregion

        #region Funções Centro Custo
        private string ValidaCentroCusto(string login, string senha, int idRemetente)
        {
            if (ValidarLoginCliente(login, senha) <= 0)
            {
                return "Erro: Login e/ou Senha inválido(s)!";
            }

            if (idRemetente <= 0)
                return "Erro: Informe o ID do Remetente!";

            return "";
        }

        private List<ConsultaListaCentroCusto> ObterCentroCustoDoBanco(string login, string senha, int idRemetente, SqlConnection sqlConnection)
        {
            var listaCentroCusto = new List<ConsultaListaCentroCusto>();

            using (var sqlCommand = new SqlCommand("Select * from tbdRemetenteCentroCusto (nolock) where id_Remetente = @idRemetente", sqlConnection))
            {
                sqlCommand.Parameters.AddWithValue("@idRemetente", idRemetente);

                using (var sqlDataReader = sqlCommand.ExecuteReader())
                {
                    while (sqlDataReader.Read())
                    {
                        var centroCusto = RetornaCentroCusto(sqlDataReader);
                        listaCentroCusto.Add(centroCusto);
                    }
                }
            }

            return listaCentroCusto;
        }

        private ConsultaListaCentroCusto RetornaCentroCusto(SqlDataReader sqlDataReader)
        {
            var centroCusto = new ConsultaListaCentroCusto();

            var id_TransportadoraServico = sqlDataReader["id_TransportadoraServico"] as int?;

            centroCusto.idRemetenteCentroCusto = (int)sqlDataReader["id_RemetenteCentroCusto"];
            centroCusto.idRemetente = (int)sqlDataReader["id_Remetente"];
            centroCusto.CentroCusto = sqlDataReader["ds_CentroCusto"] as string;
            if (id_TransportadoraServico == null)
            {
                centroCusto.idTransportadoraServico = 0;
            }
            else
            {
                centroCusto.idTransportadoraServico = (int)id_TransportadoraServico;
            }

            return centroCusto;
        }

        #endregion

        #region Funções Cidade

        private string ValidarConsultaCidade(string login, string senha, string cidade)
        {
            if (ValidarLoginCliente(login, senha) <= 0)
            {
                return "Erro: Login e/ou Senha inválido(s)!";
            }

            if (cidade.Length < 3 && cidade.Length > 0)
                return "Erro: Informe ao menos 3 caracteres para o nome da cidade.";

            return "";
        }

        private StringBuilder RetornaSelectCidade()
        {
            var retornaSelectCidade = new StringBuilder(@"SELECT c.id_Cidade,e.id_Estado,rtrim(c.ds_Cidade) as ds_Cidade,coalesce(rtrim(c.cd_Sigla),'') as cd_Sigla,rtrim(e.cd_Estado) as cd_Estado,rtrim(e.ds_Estado) as ds_Estado FROM (tbdcidade c (nolock) LEFT JOIN tbdEstado e (nolock) ON c.id_Estado = e.id_estado)");
            return retornaSelectCidade;
        }

        private void MontarWhereCidade(StringBuilder retornaSelectCidade, string estado, string cidade)
        {
            var where = new List<string>();

            if (!string.IsNullOrEmpty(estado))
                where.Add("e.cd_Estado = @estado");

            if (!string.IsNullOrEmpty(cidade))
                where.Add("c.ds_Cidade like @cidade");

            MontarWhere(retornaSelectCidade, where);
        }

        private List<ConsultaListaCidade> ObterCidadeDoBanco(StringBuilder selectCidade, SqlConnection sqlConnection, string cidade, string estado)
        {
            var ListaCidade = new List<ConsultaListaCidade>();

            using (var sqlCommand = new SqlCommand(selectCidade.ToString(), sqlConnection))
            {
                this.AdicionarParametrosCidade(sqlCommand, cidade, estado);

                using (var sqlDataReader = sqlCommand.ExecuteReader())
                {
                    while (sqlDataReader.Read())
                    {
                        var cidades = RetornaCidade(sqlDataReader);
                        ListaCidade.Add(cidades);
                    }
                }
            }

            return ListaCidade;
        }

        private void AdicionarParametrosCidade(SqlCommand sqlCommand, string cidade, string estado)
        {
            sqlCommand.Parameters.Add(new SqlParameter("cidade", "%" + cidade + "%"));
            sqlCommand.Parameters.Add(new SqlParameter("estado", estado));
        }

        private ConsultaListaCidade RetornaCidade(SqlDataReader sqlDataReader)
        {
            var cidade = new ConsultaListaCidade();
            var cd_Sigla = (string)sqlDataReader["cd_Sigla"];

            cidade.idCidade = (int)sqlDataReader["id_Cidade"];
            cidade.Cidade = sqlDataReader["ds_Cidade"] as string;
            cidade.CidadeSigla = !String.IsNullOrWhiteSpace((string)sqlDataReader["cd_Sigla"]) ? (string)sqlDataReader["cd_Sigla"] : string.Empty;
            cidade.idEstado = (int)sqlDataReader["id_Estado"];
            cidade.UF = sqlDataReader["cd_Estado"] as string;
            cidade.EstadoDescricao = sqlDataReader["ds_Estado"] as string;

            return cidade;
        }

        #endregion

        #region Funções Pessoa e Empresa

        private ConsultaListaPessoa RetornaCliente(int idCliente)
        {
            var cliente = new ConsultaListaPessoa() { idPessoa = idCliente };
            return cliente;
        }
        private string ValidarConsultaPessoa(string login, string senha, bool empresa, string cgcCPF, string nome, int idPessoa)
        {
            if (ValidarLoginCliente(login, senha) <= 0)
                return "Erro: Login e/ou Senha inválido(s)!";

            //if (string.IsNullOrEmpty(cgcCPF) && string.IsNullOrEmpty(nome) && idPessoa <= 0)
            //    return "Erro: Informe ao menos um filtro para pesquisa.";

            if (!string.IsNullOrEmpty(cgcCPF))
            {
                if (cgcCPF.Length != 11 && cgcCPF.Length != 14)
                    return "Erro: Informe um CPF/CNPJ válido.";
            }
            if (!string.IsNullOrEmpty(nome))
            {
                if (nome.Length > 0 && nome.Length < 3)
                    return "Erro: Informe ao menos 3 caracteres para o nome.";
            }

            return "";
        }

        private StringBuilder RetornaSelectPessoa(bool empresa)
        {
            StringBuilder retornaSelectPessoa;
            if (empresa)
            {
                retornaSelectPessoa = new StringBuilder(@"SELECT a.id_Pessoa, rtrim(a.ds_Pessoa) as ds_Pessoa, rtrim(a.cd_CGCCPF) as cd_CGCCPF, rtrim(a.ds_Endereco) as ds_Endereco, rtrim(c.ds_Cidade) as ds_Cidade, d.cd_Estado, a.cd_CEP FROM (((tbdPessoa a (nolock) inner join tbdTransportadora b (nolock) on a.id_Pessoa = b.id_Transportadora) left join tbdCidade c (nolock) on a.id_Cidade = c.id_Cidade) left join tbdEstado d (nolock) on c.id_Estado = d.id_Estado)");
            }
            else
            {
                retornaSelectPessoa = new StringBuilder(@"SELECT a.id_Pessoa, rtrim(a.ds_Pessoa) as ds_Pessoa, rtrim(a.cd_CGCCPF) as cd_CGCCPF, rtrim(a.ds_Endereco) as ds_Endereco, rtrim(c.ds_Cidade) as ds_Cidade, d.cd_Estado, a.cd_CEP FROM ((tbdPessoa a (nolock) left join tbdCidade c (nolock) on a.id_Cidade = c.id_Cidade) left join tbdEstado d (nolock) on c.id_Estado = d.id_Estado)");
            }
            return retornaSelectPessoa;
        }

        private void MontarWherePessoa(StringBuilder retornaSelectPessoa, bool empresa, string cgcCPF, string nome, int idPessoa)
        {
            var where = new List<string>();

            if (!string.IsNullOrEmpty(cgcCPF))
                where.Add("a.cd_CGCCPF = @cgcCPF");

            if (!string.IsNullOrEmpty(nome))
                where.Add("a.ds_Pessoa like @nome");

            if (idPessoa > 0)
                where.Add("a.id_pessoa = @idPessoa");

            MontarWhere(retornaSelectPessoa, where);
        }

        private void AdicionarParametrosPessoa(SqlCommand sqlCommand, string cgcCPF, string nome, int idPessoa)
        {
            sqlCommand.Parameters.Add(new SqlParameter("cgcCPF", cgcCPF));
            sqlCommand.Parameters.Add(new SqlParameter("nome", "%" + nome + "%"));
            sqlCommand.Parameters.Add(new SqlParameter("idPessoa", idPessoa));
        }
        private List<ConsultaListaPessoa> ObterPessoaDoBanco(StringBuilder selectPessoa, SqlConnection sqlConnection, string cgcCPF, string nome, int idPessoa)
        {
            var listaPessoa = new List<ConsultaListaPessoa>();

            using (var sqlCommand = new SqlCommand(selectPessoa.ToString(), sqlConnection))
            {
                this.AdicionarParametrosPessoa(sqlCommand, cgcCPF, nome, idPessoa);
                using (var sqlDataReader = sqlCommand.ExecuteReader())
                {
                    while (sqlDataReader.Read())
                    {
                        var pessoas = RetornaPessoa(sqlDataReader);
                        listaPessoa.Add(pessoas);
                    }
                }
            }

            return listaPessoa;
        }

        private ConsultaListaPessoa RetornaPessoa(SqlDataReader sqlDataReader)
        {
            var pessoa = new ConsultaListaPessoa()
            {
                idPessoa = (int)sqlDataReader["id_Pessoa"],
                Nome = sqlDataReader["ds_Pessoa"] as string,
                CGCCPF = sqlDataReader["cd_CGCCPF"] as string,
                Endereco = sqlDataReader["ds_Endereco"] as string,
                Cidade = sqlDataReader["ds_Cidade"] as string,
                Estado = sqlDataReader["cd_Estado"] as string,
                CEP = sqlDataReader["cd_CEP"] as string
            };

            return pessoa;
        }

        #endregion

        #region Financeiro

        private string ValidarConsultaFinanceiro(string login, string senha, string tipoDocumento, string numeroDocumento, string dataVencimentoInicial, string dataVencimentoFinal, string dataEmissaoInicial, string dataEmissaoFinal, int idEmpresaEmissora)
        {
            if (ValidarLoginCliente(login, senha) <= 0)
                return "Erro: Login e/ou Senha inválido(s)!";

            if (string.IsNullOrEmpty(tipoDocumento) && string.IsNullOrEmpty(numeroDocumento) && string.IsNullOrEmpty(dataVencimentoInicial) && string.IsNullOrEmpty(dataVencimentoFinal) && string.IsNullOrEmpty(dataEmissaoInicial) && string.IsNullOrEmpty(dataEmissaoFinal) && idEmpresaEmissora <= 0)
                return "Erro: Informe ao menos um filtro para pesquisa.";

            DateTime data;
            if (!string.IsNullOrEmpty(dataVencimentoInicial))
            {
                DateTime.TryParse(dataVencimentoInicial, out data);
                if (data == DateTime.MinValue)
                    return "Erro: Data inicial de Vencimento Inválida";
            }
            if (!string.IsNullOrEmpty(dataVencimentoFinal))
            {
                DateTime.TryParse(dataVencimentoFinal, out data);
                if (data == DateTime.MinValue)
                    return "Erro: Data Final de Vencimento Inválida";
            }

            if (!string.IsNullOrEmpty(dataEmissaoInicial))
            {
                DateTime.TryParse(dataEmissaoInicial, out data);
                if (data == DateTime.MinValue)
                    return "Erro: Data inicial de Emissão Inválida";
            }
            if (!string.IsNullOrEmpty(dataEmissaoFinal))
            {
                DateTime.TryParse(dataEmissaoFinal, out data);
                if (data == DateTime.MinValue)
                    return "Erro: Data Final de Emissão Inválida";
            }

            return "";
        }

        private void MontarWhereFinanceiro(StringBuilder retornaSelectFinanceiro, string login, string senha, string tipoDocumento, string numeroDocumento, string dataVencimentoInicial, string dataVencimentoFinal, string dataEmissaoInicial, string dataEmissaoFinal, int idEmpresaEmissora)
        {
            var where = new List<string>();
            string dataAtual = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
            int idPessoa = ValidarLoginCliente(login, senha);
            if (idPessoa > 0)
            {
                where.Add("a.id_Pessoa = @idPessoa");
            }

            switch (tipoDocumento.ToUpper())
            {
                case "PAGO":
                    where.Add("a.vl_Saldo = 0");
                    break;
                case "ABERTO":
                    where.Add("a.vl_Saldo > 0 and a.dt_Vencimento >= " + dataAtual);
                    break;
                case "ATRASADO":
                    where.Add("a.vl_Saldo > 0 and a.dt_Vencimento < " + dataAtual);
                    break;
            }
            if (!string.IsNullOrEmpty(numeroDocumento))
                where.Add("a.cd_Documento = @numeroDocumento");

            if (!string.IsNullOrEmpty(dataVencimentoInicial))
            {
                DateTime.TryParse(dataVencimentoInicial, out DateTime data);

                if (data != DateTime.MinValue)
                    where.Add("a.dt_Vencimento >= @dataVencimentoInicial");
            }

            if (!string.IsNullOrEmpty(dataVencimentoFinal))
            {
                DateTime.TryParse(dataVencimentoFinal, out DateTime data);

                if (data != DateTime.MinValue)
                    where.Add("a.dt_Vencimento <= @dataVencimentoFinal");
            }

            if (!string.IsNullOrEmpty(dataEmissaoInicial))
            {
                DateTime.TryParse(dataEmissaoInicial, out DateTime data);

                if (data != DateTime.MinValue)
                    where.Add("a.dt_Emissao >= @dataEmissaoInicial");
            }

            if (!string.IsNullOrEmpty(dataEmissaoFinal))
            {
                DateTime.TryParse(dataEmissaoFinal, out DateTime data);

                if (data != DateTime.MinValue)
                    where.Add("a.dt_Emissao <= @dataEmissaoFinal");
            }

            if (idEmpresaEmissora > 0)
                where.Add("a.id_Cliente = @idEmpresaEmissora");

            where.Add("a.tp_Financeiro = 'R'");

            MontarWhere(retornaSelectFinanceiro, where);
        }

        private StringBuilder RetornaSelectFinanceiro()
        {
            StringBuilder retornaSelectFinanceiro;

            retornaSelectFinanceiro = new StringBuilder(@"SELECT a.id_Financeiro,rtrim(b.ds_Pessoa) as ds_Pessoa,rtrim(a.cd_Documento) as cd_Documento,a.dt_Emissao,a.dt_Vencimento,a.vl_Principal,a.vl_Juros,a.vl_Desconto,a.vl_Titulo,a.vl_Saldo,a.dt_VencimentoOriginal,a.dt_BaixaFinal,rtrim(c.ds_Natureza) as ds_Natureza FROM (tbdFinanceiro a(NOLOCK) INNER JOIN tbdPessoa b(NOLOCK) ON a.id_Pessoa = b.id_Pessoa)INNER JOIN tbdNatureza c(NOLOCK) ON a.id_Natureza = c.id_Natureza");
            return retornaSelectFinanceiro;
        }

        private List<ConsultaFinanceiro> ObterFinanceiroDoBanco(StringBuilder selectFinanceiro, SqlConnection sqlConnection, string login, string senha, string tipoDocumento, string numeroDocumento, string dataVencimentoInicial, string dataVencimentoFinal, string dataEmissaoInicial, string dataEmissaoFinal, int idEmpresaEmissora)
        {
            var listaFinanceiro = new List<ConsultaFinanceiro>();

            using (var sqlCommand = new SqlCommand(selectFinanceiro.ToString(), sqlConnection))
            {
                this.AdicionarParametrosFinanceiro(sqlCommand, login, senha, tipoDocumento, numeroDocumento, dataVencimentoInicial, dataVencimentoFinal, dataEmissaoInicial, dataEmissaoFinal, idEmpresaEmissora);
                using (var sqlDataReader = sqlCommand.ExecuteReader())
                {
                    while (sqlDataReader.Read())
                    {
                        var financeiro = RetornaFinanceiro(sqlDataReader, sqlConnection);
                        listaFinanceiro.Add(financeiro);
                    }
                }
            }

            return listaFinanceiro;
        }

        private void AdicionarParametrosFinanceiro(SqlCommand sqlCommand, string login, string senha, string tipoDocumento, string numeroDocumento, string dataVencimentoInicial, string dataVencimentoFinal, string dataEmissaoInicial, string dataEmissaoFinal, int idEmpresaEmissora)
        {
            int idPessoa = ValidarLoginCliente(login, senha);
            if (idPessoa > 0)
            {
                sqlCommand.Parameters.Add(new SqlParameter("idPessoa", idPessoa));
            }

            sqlCommand.Parameters.Add(new SqlParameter("tipoDocumento", tipoDocumento));
            sqlCommand.Parameters.Add(new SqlParameter("numeroDocumento", numeroDocumento));
            sqlCommand.Parameters.Add(new SqlParameter("idEmpresaEmissora", idEmpresaEmissora));

            DateTime.TryParse(dataVencimentoInicial, out DateTime data);
            if (data != DateTime.MinValue)
                //data = DateTime.Parse(data.ToString("dd/MM/yyyy").Replace(cinf, "/"));
                sqlCommand.Parameters.Add(new SqlParameter("dataVencimentoInicial", data));

            DateTime.TryParse(dataVencimentoFinal, out data);
            if (data != DateTime.MinValue)
                //data = DateTime.Parse(data.ToString("dd/MM/yyyy").Replace(cinf, "/"));
                sqlCommand.Parameters.Add(new SqlParameter("dataVencimentoFinal", data));

            DateTime.TryParse(dataEmissaoInicial, out data);
            if (data != DateTime.MinValue)
                //data = DateTime.Parse(data.ToString("dd/MM/yyyy").Replace(cinf, "/"));
                sqlCommand.Parameters.Add(new SqlParameter("dataEmissaoInicial", data));

            DateTime.TryParse(dataEmissaoFinal, out data);
            if (data != DateTime.MinValue)
                //data = DateTime.Parse(data.ToString("dd/MM/yyyy").Replace(cinf, "/"));
                sqlCommand.Parameters.Add(new SqlParameter("dataEmissaoFinal", data));
        }

        private ConsultaFinanceiro RetornaFinanceiro(SqlDataReader sqlDataReader, SqlConnection sqlConnection)
        {
            var financeiro = new ConsultaFinanceiro();
            string dtUltimaBaixa = sqlDataReader["dt_BaixaFinal"] as string;

            financeiro.IDFinanceiro = (int)sqlDataReader["id_Financeiro"];
            financeiro.Cliente = sqlDataReader["ds_Pessoa"] as string;
            financeiro.Documento = sqlDataReader["cd_Documento"] as string;
            financeiro.Emissao = sqlDataReader["dt_Emissao"] as DateTime?;
            financeiro.Vencimento = sqlDataReader["dt_Vencimento"] as DateTime?;
            financeiro.ValorPrincipal = sqlDataReader["vl_Principal"] as decimal?;
            financeiro.ValorJuros = sqlDataReader["vl_Juros"] as decimal?;
            financeiro.ValorDesconto = sqlDataReader["vl_Desconto"] as decimal?;
            financeiro.ValorTitulo = sqlDataReader["vl_Titulo"] as decimal?;
            financeiro.ValorSaldo = sqlDataReader["vl_Saldo"] as decimal?;
            financeiro.VencimentoOriginal = sqlDataReader["dt_VencimentoOriginal"] as DateTime?;

            if (string.IsNullOrEmpty(dtUltimaBaixa))
                financeiro.UltimaBaixa = "";
            else
                financeiro.UltimaBaixa = sqlDataReader["dt_BaixaFinal"] as string;

            financeiro.Natureza = sqlDataReader["ds_Natureza"] as string;
            //financeiro.CaminhoBoleto = this.retornaURLFotoPosto(financeiro.IDFinanceiro.ToString());

            financeiro.CaminhoBoleto = this.GetUrlBoleto + financeiro.IDFinanceiro.ToString() + ".pdf";

            return financeiro;
        }

        #endregion

        #region Rastreamento

        private string ValidaRastreamento(string login, string senha, string dataInicial, string dataFinal, int codigoRemetente, int codigoDestinatario, string entregue, string numeroNota, string numeroConhecimento, int codigoCentroCusto, string ufOrigem, int cidadeOrigem, string ufDestino, int cidadeDestino, int codigoFinanceiro)
        {
            if (ValidarLoginCliente(login, senha) <= 0)
            {
                return "Erro: Login e/ou Senha inválido(s)!";
            }

            if (string.IsNullOrEmpty(dataInicial) && string.IsNullOrEmpty(dataFinal) && codigoRemetente <= 0 && codigoDestinatario <= 0 && string.IsNullOrEmpty(entregue) && string.IsNullOrEmpty(numeroNota) && string.IsNullOrEmpty(numeroConhecimento) && codigoCentroCusto <= 0 && string.IsNullOrEmpty(ufOrigem) && cidadeOrigem <= 0 && string.IsNullOrEmpty(ufDestino) && cidadeDestino <= 0 && codigoFinanceiro <= 0)
                return "Erro: Informe ao menos um filtro para pesquisa!";

            if (codigoFinanceiro == 0)
            {
                if (!string.IsNullOrEmpty(numeroNota) || !string.IsNullOrEmpty(numeroConhecimento))
                {
                    return "";
                }

                if (dataInicial == "")
                {
                    return "Erro: Preencha a Data inicial!";
                }

                DateTime.TryParse(dataInicial, out DateTime data);
                DateTime.TryParse(dataFinal, out DateTime dataFim);

                if (dataFinal == "")
                {
                    dataFim = DateTime.Now;
                }

                DateTime dataInicialValida = DataInicialValida(dataFim);
                if (data < dataInicialValida)
                {
                    return "Erro: Data inicial não pode ser menor que " + String.Format("{0:dd/MM/yyyy}", dataInicialValida) + ".";
                }
            }

            return "";
        }

        private StringBuilder RetornaSelectRastreamento(int codigoFinanceiro)
        {
            StringBuilder retornaSelectRastreamento;
            string sTabela = "";

            sTabela = "tbdMovimento a (nolock)";
            sTabela = "(" + sTabela + "INNER JOIN tbdTipoMovimento b (nolock) ON a.id_TipoMovimento = b.id_TipoMovimento) ";
            sTabela = "(" + sTabela + "LEFT JOIN tbdPessoa (nolock) ON a.id_Remetente = tbdPessoa.id_Pessoa) ";
            sTabela = "(" + sTabela + "LEFT JOIN tbdCidade cidDest (nolock) ON a.id_CidadeDestinatario = cidDest.id_Cidade) ";
            sTabela = "(" + sTabela + "LEFT JOIN tbdEstado estDest (nolock) ON cidDest.id_Estado = estDest.id_Estado) ";
            sTabela = "(" + sTabela + "LEFT JOIN tbdPessoa fat (nolock) ON a.id_ClienteFaturamento = fat.id_Pessoa) ";
            sTabela = "(" + sTabela + "LEFT JOIN tbdCidade cidOri (nolock) ON a.id_CidadeOrigem = cidOri.id_Cidade) ";
            sTabela = "(" + sTabela + "LEFT JOIN tbdEstado estOri (nolock) ON cidOri.id_Estado = estOri.id_Estado) ";
            sTabela = "(" + sTabela + "LEFT JOIN tbdRemetenteCentroCusto CC (nolock) ON a.id_RemetenteCentroCusto = CC.id_RemetenteCentroCusto) ";

            if (codigoFinanceiro > 0)
            {
                sTabela = "(" + sTabela + "LEFT JOIN tbdMovimentoFinanceiro movfin (nolock) ON a.id_movimento = movfin.id_movimento) ";
            }
            retornaSelectRastreamento = new StringBuilder(@"SELECT a.id_Movimento, a.dt_Coleta,RTRIM(a.nr_Conhecimento) AS nr_Conhecimento, rtrim(a.nr_Minuta) nr_Minuta, a.dt_ImpressaoConhecimento AS dt_ImpressaoConhecimento, a.dt_PrazoEntrega, RTRIM(tbdPessoa.ds_Pessoa) AS ds_Remetente,RTRIM(tbdPessoa.cd_CGCCPF) AS cd_CGCCPFRemetente, RTRIM(a.cd_CGCCPF) AS cd_CGCCPFDestinatario,RTRIM(a.ds_Cliente) AS ds_Destinatario,RTRIM(cidDest.ds_Cidade) AS ds_CidadeDestinatario, RTRIM(a.nr_NotaFiscal) AS nr_NotaFiscal, a.vl_Frete, a.dt_Recepcao AS dt_Entrega, (SELECT TOP 1 e.ds_Ocorrencia FROM tbdOcorrenciaNota d (NOLOCK) LEFT JOIN tbdOcorrencia e (NOLOCK) ON d.id_Ocorrencia = e.id_Ocorrencia LEFT JOIN tbdOcorrenciaManifesto f (NOLOCK) ON d.id_Ocorrencia = f.id_OcorrenciaManifesto WHERE d.id_Movimento = a.id_Movimento AND isnull(f.tp_naovisualizarsislognetcli,'N') <> 'S' ORDER BY d.dt_PrazoFechamento DESC,d.hr_PrazoFechamento DESC) AS ds_UltimaOcorrencia, (SELECT TOP 1 d.dt_PrazoFechamento FROM tbdOcorrenciaNota d (NOLOCK) left JOIN tbdOcorrencia e (NOLOCK) ON d.id_Ocorrencia = e.id_Ocorrencia left JOIN tbdOcorrenciaManifesto f (NOLOCK) ON d.id_Ocorrencia = f.id_OcorrenciaManifesto WHERE d.id_Movimento = a.id_Movimento and isnull(f.tp_naovisualizarsislognetcli,'N') <> 'S' ORDER BY d.dt_PrazoFechamento DESC,d.hr_PrazoFechamento DESC) AS dt_UltimaOcorrencia, RTRIM(fat.ds_Pessoa) as ds_Faturado, RTRIM(a.ds_Receptor) AS ds_Receptor, rtrim(cidOri.ds_Cidade) as ds_CidadeOrigem, estOri.cd_Estado as cd_EstadoOrigem, estDest.cd_Estado as cd_EstadoDestinatario, rtrim(CC.ds_CentroCusto) ds_CentroCusto, case when rtrim(a.tp_Categoria)='A' then 'Aéreo' else 'Rodoviário' end modal From " + sTabela);

            return retornaSelectRastreamento;
        }

        private void MontarWhereRastreamento(StringBuilder retornaSelectRastreamento, string login, string senha, string dataInicial, string dataFinal, int codigoRemetente, int codigoDestinatario, string entregue, string numeroNota, string numeroConhecimento, int codigoCentroCusto, string ufOrigem, int cidadeOrigem, string ufDestino, int cidadeDestino, int codigoFinanceiro, string modal = "")
        {
            var where = new List<string>();

            if (codigoFinanceiro > 0)
            {
                int idPessoa = ValidarLoginCliente(login, senha);
                if (idPessoa > 0)
                {
                    where.Add("(a.id_Remetente = @idPessoa OR a.id_ClienteFaturamento = @idPessoa OR a.id_Destinatario = @idPessoa)");
                }

                where.Add("movfin.id_Financeiro = @codigoFinanceiro");
            }
            else
            {
                where.Add("ISNULL(b.tp_NaoVisualizaSislognet, 'N') <> 'S'");

                switch (entregue.ToUpper())
                {
                    case "SIM":
                        where.Add("a.dt_Recepcao is not null");
                        break;
                    case "NAO":
                        where.Add("a.dt_Recepcao is null");
                        break;
                }

                if (!string.IsNullOrEmpty(modal.ToString()))
                {
                    if (modal.ToString() == "A" || modal.ToString() == "a")
                    {

                        where.Add("isnull(a.tp_categoria,'') = 'A'");
                    }
                    else
                    {
                        where.Add("isnull(a.tp_categoria,'') = 'T'");
                    }
                }

                int idPessoa = ValidarLoginCliente(login, senha);
                if (idPessoa > 0)
                {
                    where.Add("(a.id_Remetente = @idPessoa OR a.id_ClienteFaturamento = @idPessoa OR a.id_Destinatario = @idPessoa)");
                }

                if (!string.IsNullOrEmpty(dataInicial))
                {
                    DateTime.TryParse(dataInicial, out DateTime data);

                    if (data != DateTime.MinValue)
                        where.Add("a.dt_ImpressaoConhecimento >= @dataInicial");
                }

                if (!string.IsNullOrEmpty(dataFinal))
                {
                    DateTime.TryParse(dataFinal, out DateTime data);

                    if (data != DateTime.MinValue)
                        where.Add("a.dt_ImpressaoConhecimento <= @dataFinal");
                }

                if (codigoRemetente > 0)
                    where.Add("a.id_Remetente = @codigoRemetente");

                if (codigoDestinatario > 0)
                    where.Add("a.id_Cliente = @codigoDestinatario");

                if (!string.IsNullOrEmpty(numeroNota))
                    where.Add("a.id_Movimento IN (SELECT id_Movimento FROM tbdMovimentoNotaFiscal (nolock) WHERE cd_NotaFiscal = @numeroNota)");

                if (!string.IsNullOrEmpty(numeroConhecimento))
                    where.Add("a.nr_Conhecimento = @numeroConhecimento");

                if (codigoCentroCusto > 0)
                    where.Add("a.id_RemetenteCentroCusto = @codigoCentroCusto");

                if (!string.IsNullOrEmpty(ufOrigem))
                    where.Add("estOri.cd_Estado = @ufOrigem");

                if (cidadeOrigem > 0)
                    where.Add("cidOri.id_Cidade = @cidadeOrigem");

                if (!string.IsNullOrEmpty(ufDestino))
                    where.Add("estDest.cd_Estado = @ufDestino");

                if (cidadeDestino > 0)
                    where.Add("cidDest.id_Cidade = @cidadeDestino");
            }

            MontarWhere(retornaSelectRastreamento, where);
        }

        private List<ConsultaRastreamento> ObterRastreamentoDoBanco(StringBuilder selectRastreamento, SqlConnection sqlConnection, string login, string senha, string dataInicial, string dataFinal, int codigoRemetente, int codigoDestinatario, string entregue, string numeroNota, string numeroConhecimento, int codigoCentroCusto, string ufOrigem, int cidadeOrigem, string ufDestino, int cidadeDestino, int codigoFinanceiro)
        {
            var listaRastreamento = new List<ConsultaRastreamento>();

            using (var sqlCommand = new SqlCommand(selectRastreamento.ToString(), sqlConnection))
            {
                this.AdicionarParametrosRastreamento(sqlCommand, login, senha, dataInicial, dataFinal, codigoRemetente, codigoDestinatario, entregue, numeroNota, numeroConhecimento, codigoCentroCusto, ufOrigem, cidadeOrigem, ufDestino, cidadeDestino, codigoFinanceiro);
                using (var sqlDataReader = sqlCommand.ExecuteReader())
                {
                    while (sqlDataReader.Read())
                    {
                        var rastreamento = RetornaRastreamento(sqlDataReader);
                        listaRastreamento.Add(rastreamento);
                    }
                }
            }

            return listaRastreamento;
        }

        private void AdicionarParametrosRastreamento(SqlCommand sqlCommand, string login, string senha, string dataInicial, string dataFinal, int codigoRemetente, int codigoDestinatario, string entregue, string numeroNota, string numeroConhecimento, int codigoCentroCusto, string ufOrigem, int cidadeOrigem, string ufDestino, int cidadeDestino, int codigoFinanceiro)
        {
            int idPessoa = ValidarLoginCliente(login, senha);
            if (idPessoa > 0)
            {
                sqlCommand.Parameters.Add(new SqlParameter("idPessoa", idPessoa));
            }

            DateTime.TryParse(dataInicial, out DateTime data);
            if (data != DateTime.MinValue)
                //data = DateTime.Parse(data.ToString("dd/MM/yyyy").Replace(cinf, "/"));
                sqlCommand.Parameters.Add(new SqlParameter("dataInicial", data));

            DateTime.TryParse(dataFinal, out data);
            if (data != DateTime.MinValue)
                //data = DateTime.Parse(data.ToString("dd/MM/yyyy").Replace(cinf, "/"));
                sqlCommand.Parameters.Add(new SqlParameter("dataFinal", data));

            sqlCommand.Parameters.Add(new SqlParameter("codigoRemetente", codigoRemetente));
            sqlCommand.Parameters.Add(new SqlParameter("codigoDestinatario", codigoDestinatario));
            sqlCommand.Parameters.Add(new SqlParameter("numeroNota", numeroNota));
            sqlCommand.Parameters.Add(new SqlParameter("numeroConhecimento", numeroConhecimento));
            sqlCommand.Parameters.Add(new SqlParameter("codigoCentroCusto", codigoCentroCusto));
            sqlCommand.Parameters.Add(new SqlParameter("ufOrigem", ufOrigem));
            sqlCommand.Parameters.Add(new SqlParameter("cidadeOrigem", cidadeOrigem));
            sqlCommand.Parameters.Add(new SqlParameter("ufDestino", ufDestino));
            sqlCommand.Parameters.Add(new SqlParameter("cidadeDestino", cidadeDestino));
            sqlCommand.Parameters.Add(new SqlParameter("codigoFinanceiro", codigoFinanceiro));
        }

        private ConsultaRastreamento RetornaRastreamento(SqlDataReader sqlDataReader)
        {
            var rastreamento = new ConsultaRastreamento();
            var ultimaOcorrencia = sqlDataReader["ds_UltimaOcorrencia"] as string;
            var centroCusto = sqlDataReader["ds_CentroCusto"] as string;

            rastreamento.IDMovimento = (int)sqlDataReader["id_Movimento"];

            DateTime.TryParse(String.Format("{0:dd/MM/yyyy}", sqlDataReader["dt_ImpressaoConhecimento"]), out DateTime data);
            if (data != DateTime.MinValue)
                rastreamento.DataColeta = sqlDataReader["dt_ImpressaoConhecimento"] as DateTime?;
            else
                rastreamento.DataColeta = sqlDataReader["dt_Coleta"] as DateTime?;
            rastreamento.Conhecimento = sqlDataReader["nr_Conhecimento"] as string;
            rastreamento.Minuta = sqlDataReader["nr_Minuta"] as string;
            rastreamento.CGCCPFRemetente = sqlDataReader["cd_CGCCPFRemetente"] as string;
            rastreamento.Remetente = sqlDataReader["ds_Remetente"] as string;
            rastreamento.CGCCPFDestinatario = sqlDataReader["cd_CGCCPFDestinatario"] as string;
            rastreamento.Destinatario = sqlDataReader["ds_Destinatario"] as string;
            rastreamento.CidadeOrigem = sqlDataReader["ds_CidadeOrigem"] as string;
            rastreamento.UFOrigem = sqlDataReader["cd_EstadoOrigem"] as string;
            rastreamento.CidadeDestino = sqlDataReader["ds_CidadeDestinatario"] as string;
            rastreamento.UFDestino = sqlDataReader["cd_EstadoDestinatario"] as string;
            rastreamento.ClienteFaturado = sqlDataReader["ds_Faturado"] as string;
            rastreamento.NotaFiscal = sqlDataReader["nr_NotaFiscal"] as string;
            rastreamento.ValorFrete = sqlDataReader["vl_Frete"] as decimal?;
            rastreamento.PrevisaoEntrega = sqlDataReader["dt_PrazoEntrega"] as DateTime?;
            rastreamento.DataEntrega = sqlDataReader["dt_Entrega"] as DateTime?;

            if (ultimaOcorrencia != "" && !string.IsNullOrEmpty(ultimaOcorrencia))
                rastreamento.UltimaOcorrencia = sqlDataReader["ds_UltimaOcorrencia"] as string;
            else
                rastreamento.UltimaOcorrencia = "";

            if (centroCusto != "" && !string.IsNullOrEmpty(centroCusto))
                rastreamento.CentroCusto = sqlDataReader["ds_CentroCusto"] as string;
            else
                rastreamento.CentroCusto = "";

            rastreamento.DataUltimaOcorrencia = sqlDataReader["dt_UltimaOcorrencia"] as DateTime?;
            rastreamento.NomeRecebedor = sqlDataReader["ds_Receptor"] as string;

            DateTime.TryParse(String.Format("{0:dd/MM/yyyy}", sqlDataReader["dt_Entrega"]), out DateTime dataEntrega);
            if (dataEntrega != DateTime.MinValue)
                rastreamento.ComprovanteEntrega = this.GetUrlComprovante + rastreamento.IDMovimento.ToString() + "m.jpg";
            else
                rastreamento.ComprovanteEntrega = "";

            rastreamento.ComprovanteEntrega2 = "";
            rastreamento.Modal = sqlDataReader["modal"] as string;
            return rastreamento;
        }

        #endregion

        #region Embarque

        private string ValidaEmbarque(string login, string senha, string dataInicial, string dataFinal, int codigoRemetente, int codigoDestinatario, string entregue, string numeroNota, string numeroConhecimento, int codigoCentroCusto, string ufOrigem, int cidadeOrigem, string ufDestino, int cidadeDestino)
        {
            if (ValidarLoginCliente(login, senha) <= 0)
            {
                return "Erro: Login e/ou Senha inválido(s)!";
            }

            if (string.IsNullOrEmpty(dataInicial) && string.IsNullOrEmpty(dataFinal) && codigoRemetente > 0 && codigoDestinatario > 0 && string.IsNullOrEmpty(entregue) && string.IsNullOrEmpty(numeroNota) && string.IsNullOrEmpty(numeroConhecimento) && codigoCentroCusto > 0 && string.IsNullOrEmpty(ufOrigem) && cidadeOrigem > 0 && string.IsNullOrEmpty(ufDestino) && cidadeDestino > 0)
                return "Erro: Informe ao menos um filtro para pesquisa!";

            if (!string.IsNullOrEmpty(numeroNota) || !string.IsNullOrEmpty(numeroConhecimento))
            {
                return "";
            }

            if (dataInicial == "")
            {
                return "Erro: Preencha a Data inicial!";
            }

            DateTime.TryParse(dataInicial, out DateTime data);
            DateTime.TryParse(dataFinal, out DateTime dataFim);
            if (dataFinal == "")
            {
                dataFim = DateTime.Now;
            }

            DateTime dataInicialValida = DataInicialValida(dataFim);
            if (data < dataInicialValida)
            {
                return "Erro: Data inicial não pode ser menor que " + String.Format("{0:dd/MM/yyyy}", dataInicialValida) + ".";
            }

            return "";
        }

        private StringBuilder RetornaSelectEmbarque()
        {
            StringBuilder retornaSelectEmbarque;

            retornaSelectEmbarque = new StringBuilder(@"SELECT a.id_Movimento, case when isnull(a.tp_Categoria,'') = 'T' then 'Rodoviário' else 'Aéreo' end as ds_Categoria,a.dt_Coleta,RTRIM(a.nr_Conhecimento) AS nr_Conhecimento,RTRIM(a.nr_Minuta) AS nr_Minuta, a.dt_ImpressaoConhecimento AS dt_ImpressaoConhecimento, RTRIM(tbdPessoa.cd_CGCCPF) AS cd_CGCCPFRemetente,RTRIM(tbdPessoa.ds_Pessoa) AS ds_Remetente,RTRIM(a.cd_CGCCPF) AS cd_CGCCPFDestinatario,RTRIM(a.ds_Cliente) AS ds_Destinatario,RTRIM(cidOri.ds_Cidade) AS ds_CidadeOrigem,RTRIM(estOri.cd_Estado) AS cd_EstadoOrigem,RTRIM(cidDest.ds_Cidade) AS ds_CidadeDestino,RTRIM(estDest.cd_Estado) AS cd_EstadoDestino,RTRIM(movnf.cd_NotaFiscal) AS cd_NotaFiscal,movnf.vl_NotaFiscal,movnf.qt_Volume,a.kg_MercadoriaReal,a.kg_Mercadoria,a.vl_Frete,a.vl_ICMS,a.pc_ICMS,b.ds_TipoMovimento,RTRIM(fat.cd_CGCCPF) AS cd_CGCCPFFaturado,RTRIM(fat.ds_Pessoa) AS ds_Faturado,loteCTeM.ds_ChaveCTe,a.dt_PrazoEntrega,a.dt_Recepcao AS dt_Entrega,(SELECT TOP 1 e.ds_Ocorrencia FROM tbdOcorrenciaNota d(NOLOCK) LEFT JOIN tbdOcorrencia e(NOLOCK) ON d.id_Ocorrencia = e.id_Ocorrencia LEFT JOIN tbdOcorrenciaManifesto f(NOLOCK) ON d.id_Ocorrencia = f.id_OcorrenciaManifesto WHERE d.id_Movimento = a.id_Movimento and isnull(f.tp_naovisualizarsislognetcli,'N') <> 'S' ORDER BY d.dt_PrazoFechamento DESC,d.hr_PrazoFechamento DESC) AS ds_UltimaOcorrencia, (SELECT TOP 1 d.dt_PrazoFechamento FROM tbdOcorrenciaNota d(NOLOCK) LEFT JOIN tbdOcorrencia e(NOLOCK) ON d.id_Ocorrencia = e.id_Ocorrencia LEFT JOIN tbdOcorrenciaManifesto f(NOLOCK) ON d.id_Ocorrencia = f.id_OcorrenciaManifesto WHERE d.id_Movimento = a.id_Movimento and isnull(f.tp_naovisualizarsislognetcli,'N') <> 'S' ORDER BY d.dt_PrazoFechamento DESC,d.hr_PrazoFechamento DESC) AS dt_UltimaOcorrencia, rtrim(cc.ds_CentroCusto) AS ds_CentroCusto, RTRIM(a.ds_Receptor) AS ds_Receptor FROM (((((((((((((tbdMovimentoNotaFiscal movnf (nolock) LEFT JOIN tbdMovimento a (nolock) ON movnf.id_Movimento = a.id_Movimento) INNER JOIN tbdTipoMovimento b (nolock) ON a.id_TipoMovimento = b.id_TipoMovimento) LEFT JOIN tbdPessoa (nolock) ON a.id_Remetente = tbdPessoa.id_Pessoa) LEFT JOIN tbdPessoa fat (NOLOCK) ON a.id_ClienteFaturamento = fat.id_Pessoa) LEFT JOIN tbdPendenciaMovimento c (nolock) ON a.id_Movimento = c.id_Movimento) LEFT JOIN tbdRemetenteCentroCusto cc (nolock) ON a.id_RemetenteCentroCusto = cc.id_RemetenteCentroCusto) LEFT JOIN tbdCidade cidOri (nolock) ON a.id_CidadeOrigem = cidOri.id_Cidade) LEFT JOIN tbdEstado estOri (nolock) ON cidOri.id_Estado = estOri.id_Estado) LEFT JOIN tbdCidade cidDest (nolock) ON a.id_CidadeDestinatario = cidDest.id_Cidade) LEFT JOIN tbdMovimentoDados MD (NOLOCK) ON a.id_Movimento = MD.id_Movimento) LEFT JOIN tbdEstado estDest (nolock) ON cidDest.id_Estado = estDest.id_Estado) LEFT JOIN tbdLoteCTe loteCTe (NOLOCK) ON MD.id_LoteCTe = loteCTe.id_LoteCTe) LEFT JOIN tbdLoteCTeMovimento loteCTeM (NOLOCK) ON loteCTe.id_LoteCTe = loteCTeM.id_LoteCTe AND a.id_Movimento = loteCTeM.id_Movimento)");
            return retornaSelectEmbarque;
        }

        private void MontarWhereEmbarque(StringBuilder retornaSelectEmbarque, string login, string senha, string dataInicial, string dataFinal, int codigoRemetente, int codigoDestinatario, string entregue, string numeroNota, string numeroConhecimento, int codigoCentroCusto, string ufOrigem, int cidadeOrigem, string ufDestino, int cidadeDestino, string modal = "")
        {
            var where = new List<string>();

            switch (entregue.ToUpper())
            {
                case "SIM":
                    where.Add("a.dt_Recepcao is not null");
                    break;
                case "NAO":
                    where.Add("a.dt_Recepcao is null");
                    break;
            }

            if (!string.IsNullOrEmpty(modal.ToString()))
            {
                if (modal.ToString() == "A" || modal.ToString() == "a")
                {

                    where.Add("isnull(a.tp_categoria,'') = 'A'");
                }
                else
                {
                    where.Add("isnull(a.tp_categoria,'') = 'T'");
                }
            }

            //where.Add("ISNULL(b.tp_NaoVisualizaSislognet,'N') <> 'S'");
            int idPessoa = ValidarLoginCliente(login, senha);
            if (idPessoa > 0)
            {
                where.Add("(a.id_Remetente = @idPessoa OR a.id_ClienteFaturamento = @idPessoa OR a.id_Destinatario = @idPessoa)");
            }

            if (!string.IsNullOrEmpty(dataInicial))
            {
                DateTime.TryParse(dataInicial, out DateTime data);

                if (data != DateTime.MinValue)
                    where.Add("a.dt_ImpressaoConhecimento >= @dataInicial");
            }

            if (!string.IsNullOrEmpty(dataFinal))
            {
                DateTime.TryParse(dataFinal, out DateTime data);

                if (data != DateTime.MinValue)
                    where.Add("a.dt_ImpressaoConhecimento <= @dataFinal");
            }

            if (codigoRemetente > 0)
                where.Add("a.id_Remetente = @codigoRemetente");

            if (codigoDestinatario > 0)
                where.Add("a.id_Cliente = @codigoDestinatario");

            if (!string.IsNullOrEmpty(numeroNota))
                where.Add("a.id_Movimento IN (SELECT id_Movimento FROM tbdMovimentoNotaFiscal (nolock) WHERE cd_NotaFiscal = @numeroNota)");

            if (!string.IsNullOrEmpty(numeroConhecimento))
                where.Add("a.nr_Conhecimento = @numeroConhecimento");

            if (codigoCentroCusto > 0)
                where.Add("a.id_RemetenteCentroCusto = @codigoCentroCusto");

            if (!string.IsNullOrEmpty(ufOrigem))
                where.Add("estOri.cd_Estado = @ufOrigem");

            if (cidadeOrigem > 0)
                where.Add("cidOri.id_Cidade = @cidadeOrigem");

            if (!string.IsNullOrEmpty(ufDestino))
                where.Add("estDest.cd_Estado = @ufDestino");

            if (cidadeDestino > 0)
                where.Add("cidDest.id_Cidade = @cidadeDestino");

            MontarWhere(retornaSelectEmbarque, where);
        }

        private List<ConsultaEmbarque> ObterEmbarqueDoBanco(StringBuilder selectEmbarque, SqlConnection sqlConnection, string login, string senha, string dataInicial, string dataFinal, int codigoRemetente, int codigoDestinatario, string entregue, string numeroNota, string numeroConhecimento, int codigoCentroCusto, string ufOrigem, int cidadeOrigem, string ufDestino, int cidadeDestino)
        {
            var listaEmbarque = new List<ConsultaEmbarque>();

            using (var sqlCommand = new SqlCommand(selectEmbarque.ToString(), sqlConnection))
            {
                this.AdicionarParametrosEmbarque(sqlCommand, login, senha, dataInicial, dataFinal, codigoRemetente, codigoDestinatario, entregue, numeroNota, numeroConhecimento, codigoCentroCusto, ufOrigem, cidadeOrigem, ufDestino, cidadeDestino);
                using (var sqlDataReader = sqlCommand.ExecuteReader())
                {
                    while (sqlDataReader.Read())
                    {
                        var embarque = RetornaEmbarque(sqlDataReader);
                        listaEmbarque.Add(embarque);
                    }
                }
            }

            return listaEmbarque;
        }

        private void AdicionarParametrosEmbarque(SqlCommand sqlCommand, string login, string senha, string dataInicial, string dataFinal, int codigoRemetente, int codigoDestinatario, string entregue, string numeroNota, string numeroConhecimento, int codigoCentroCusto, string ufOrigem, int cidadeOrigem, string ufDestino, int cidadeDestino)
        {
            int idPessoa = ValidarLoginCliente(login, senha);
            if (idPessoa > 0)
            {
                sqlCommand.Parameters.Add(new SqlParameter("idPessoa", idPessoa));
            }

            DateTime.TryParse(dataInicial, out DateTime data);
            if (data != DateTime.MinValue)
                //data = DateTime.Parse(data.ToString("dd/MM/yyyy").Replace(cinf, "/"));
                sqlCommand.Parameters.Add(new SqlParameter("dataInicial", data));

            DateTime.TryParse(dataFinal, out data);
            if (data != DateTime.MinValue)
                //data = DateTime.Parse(data.ToString("dd/MM/yyyy").Replace(cinf, "/"));
                sqlCommand.Parameters.Add(new SqlParameter("dataFinal", data));

            sqlCommand.Parameters.Add(new SqlParameter("codigoRemetente", codigoRemetente));
            sqlCommand.Parameters.Add(new SqlParameter("codigoDestinatario", codigoDestinatario));
            sqlCommand.Parameters.Add(new SqlParameter("numeroNota", numeroNota));
            sqlCommand.Parameters.Add(new SqlParameter("numeroConhecimento", numeroConhecimento));
            sqlCommand.Parameters.Add(new SqlParameter("codigoCentroCusto", codigoCentroCusto));
            sqlCommand.Parameters.Add(new SqlParameter("ufOrigem", ufOrigem));
            sqlCommand.Parameters.Add(new SqlParameter("cidadeOrigem", cidadeOrigem));
            sqlCommand.Parameters.Add(new SqlParameter("ufDestino", ufDestino));
            sqlCommand.Parameters.Add(new SqlParameter("cidadeDestino", cidadeDestino));
        }

        private ConsultaEmbarque RetornaEmbarque(SqlDataReader sqlDataReader)
        {
            var embarque = new ConsultaEmbarque();
            var ultimaOcorrencia = sqlDataReader["ds_UltimaOcorrencia"] as string;
            var centroCusto = sqlDataReader["ds_CentroCusto"] as string;

            embarque.IDMovimento = (int)sqlDataReader["id_Movimento"];
            embarque.Categoria = sqlDataReader["ds_Categoria"] as string;
            DateTime.TryParse(String.Format("{0:dd/MM/yyyy}", sqlDataReader["dt_ImpressaoConhecimento"]), out DateTime data);
            if (data != DateTime.MinValue)
                embarque.DataColeta = sqlDataReader["dt_ImpressaoConhecimento"] as DateTime?;
            else
                embarque.DataColeta = sqlDataReader["dt_Coleta"] as DateTime?;
            embarque.Conhecimento = sqlDataReader["nr_Conhecimento"] as string;
            embarque.Minuta = sqlDataReader["nr_Minuta"] as string;
            embarque.CGCCPFRemetente = sqlDataReader["cd_CGCCPFRemetente"] as string;
            embarque.Remetente = sqlDataReader["ds_Remetente"] as string;
            embarque.CGCCPFDestinatario = sqlDataReader["cd_CGCCPFDestinatario"] as string;
            embarque.Destinatario = sqlDataReader["ds_Destinatario"] as string;
            embarque.CidadeOrigem = sqlDataReader["ds_CidadeOrigem"] as string;
            embarque.UFOrigem = sqlDataReader["cd_EstadoOrigem"] as string;
            embarque.CidadeDestino = sqlDataReader["ds_CidadeDestino"] as string;
            embarque.UFDestino = sqlDataReader["cd_EstadoDestino"] as string;
            embarque.ClienteFaturado = sqlDataReader["ds_Faturado"] as string;
            embarque.NotaFiscal = sqlDataReader["cd_NotaFiscal"] as string;
            embarque.ValorNotaFiscal = sqlDataReader["vl_NotaFiscal"] as decimal?;
            embarque.Volume = (int)sqlDataReader["qt_Volume"];
            embarque.PesoReal = sqlDataReader["kg_MercadoriaReal"] as decimal?;
            embarque.PesoTaxado = sqlDataReader["kg_Mercadoria"] as decimal?;
            embarque.ValorFrete = sqlDataReader["vl_Frete"] as decimal?;
            embarque.ValorICMS = sqlDataReader["vl_ICMS"] as decimal?;
            embarque.PorcentagemICMS = sqlDataReader["pc_ICMS"] as decimal?;
            embarque.TipoMovimento = sqlDataReader["ds_TipoMovimento"] as string;
            embarque.CGCCPFFaturado = sqlDataReader["cd_CGCCPFFaturado"] as string;
            embarque.Faturado = sqlDataReader["ds_Faturado"] as string;
            embarque.ChaveCTe = sqlDataReader["ds_ChaveCTe"] as string;
            embarque.PrevisaoEntrega = sqlDataReader["dt_PrazoEntrega"] as DateTime?;
            embarque.DataEntrega = sqlDataReader["dt_Entrega"] as DateTime?;

            if (ultimaOcorrencia != "" && !string.IsNullOrEmpty(ultimaOcorrencia))
                embarque.UltimaOcorrencia = sqlDataReader["ds_UltimaOcorrencia"] as string;
            else
                embarque.UltimaOcorrencia = "";

            embarque.DataUltimaOcorrencia = sqlDataReader["dt_UltimaOcorrencia"] as DateTime?;
            embarque.NomeRecebedor = sqlDataReader["ds_Receptor"] as string;

            if (centroCusto != "" && !string.IsNullOrEmpty(centroCusto))
                embarque.CentroCusto = sqlDataReader["ds_CentroCusto"] as string;
            else
                embarque.CentroCusto = "";

            DateTime.TryParse(String.Format("{0:dd/MM/yyyy}", sqlDataReader["dt_Entrega"]), out DateTime dataEntrega);
            if (dataEntrega != DateTime.MinValue)
                embarque.ComprovanteEntrega = this.GetUrlComprovante + embarque.IDMovimento.ToString() + ".jpg";
            else
                embarque.ComprovanteEntrega = "";

            embarque.ComprovanteEntrega2 = ""; //Alterar

            return embarque;
        }

        #endregion

        #region DetalheEmbarque

        private string ValidaEmbarqueDetalhe(string login, string senha, int idMovimento)
        {
            if (ValidarLoginCliente(login, senha) <= 0)
            {
                return "Erro: Login e/ou Senha inválido(s)!";
            }

            if (idMovimento <= 0)
            {
                return "Erro: Informe o ID do Movimento.";
            }

            return "";
        }

        private StringBuilder RetornaSelectEmbarqueDetalhe()
        {
            StringBuilder retornaSelectEmbarqueDetalhe;

            retornaSelectEmbarqueDetalhe = new StringBuilder(@"SELECT a.id_Movimento,a.dt_Coleta AS dt_Coleta,a.kg_MercadoriaReal AS kg_MercadoriaReal,a.dt_ImpressaoConhecimento AS dt_ImpressaoConhecimento,RTRIM(a.ds_Remetente) AS ds_Remetente,RTRIM(a.ds_Cliente) AS ds_Cliente,RTRIM(a.nr_Minuta) AS nr_Minuta,RTRIM(a.nr_Conhecimento) AS nr_Conhecimento,RTRIM(a.cm_MovimentoMinuta) + ' ' + RTRIM(a.cm_Movimento) AS cm_MovimentoMinuta,RTRIM(a.ds_NaturezaMercadoria) AS ds_NaturezaMercadoria,RTRIM(b.ds_Cidade) AS ds_CidadeDestinatario,RTRIM(c.ds_Cidade) AS ds_CidadeOrigem, RTRIM(fat.ds_Pessoa) as ds_Faturado, a.kg_Mercadoria AS kg_Mercadoria,RTRIM(tipoMov.ds_TipoMovimento) as ds_TipoMovimento,a.nr_Referencia,rtrim(FormaPag.ds_FormaPagamento) as ds_FormaPagamento, a.dt_Recepcao as dt_Entrega FROM (((((tbdMovimento a (nolock) LEFT JOIN tbdCidade b (nolock) ON a.id_CidadeDestinatario = b.id_Cidade) LEFT JOIN tbdCidade c (nolock) ON a.id_CidadeOrigem = c.id_Cidade) LEFT JOIN tbdTipoMovimento tipoMov (nolock) ON a.id_TipoMovimento = tipoMov.id_TipoMovimento) LEFT JOIN tbdFormaPagamento FormaPag (nolock) ON a.id_FormaPagamento = FormaPag.id_FormaPagamento) LEFT JOIN tbdPessoa fat (NOLOCK) ON a.id_ClienteFaturamento = fat.id_Pessoa)");
            return retornaSelectEmbarqueDetalhe;
        }

        private void MontarWhereEmbarqueDetalhe(StringBuilder retornaSelectEmbarqueDetalhe, string login, string senha, int idMovimento)
        {
            var where = new List<string>();

            if (idMovimento > 0)
                where.Add("a.id_Movimento = @idMovimento");

            MontarWhere(retornaSelectEmbarqueDetalhe, where);
        }

        private List<ConsultaEmbarqueDetalhe> ObterEmbarqueDetalheDoBanco(StringBuilder selectEmbarqueDetalhe, SqlConnection sqlConnection, string login, string senha, int idMovimento)
        {
            var listaEmbarqueDetalhe = new List<ConsultaEmbarqueDetalhe>();

            using (var sqlCommand = new SqlCommand(selectEmbarqueDetalhe.ToString(), sqlConnection))
            {
                this.AdicionarParametrosEmbarqueDetalhe(sqlCommand, login, senha, idMovimento);
                using (var sqlDataReader = sqlCommand.ExecuteReader())
                {
                    while (sqlDataReader.Read())
                    {
                        var embarque = RetornaEmbarqueDetalhe(sqlDataReader);
                        listaEmbarqueDetalhe.Add(embarque);
                    }
                }
            }

            return listaEmbarqueDetalhe;
        }

        private void AdicionarParametrosEmbarqueDetalhe(SqlCommand sqlCommand, string login, string senha, int idMovimento)
        {
            sqlCommand.Parameters.Add(new SqlParameter("idMovimento", idMovimento));
        }

        private ConsultaEmbarqueDetalhe RetornaEmbarqueDetalhe(SqlDataReader sqlDataReader)
        {
            var embarqueDetalhe = new ConsultaEmbarqueDetalhe()
            {
                ID = (int)sqlDataReader["id_Movimento"],
                Remetente = sqlDataReader["ds_Remetente"] as string,
                CidadeOrigem = sqlDataReader["ds_CidadeOrigem"] as string,
                Destinatario = sqlDataReader["ds_Cliente"] as string,
                CidadeDestino = sqlDataReader["ds_CidadeDestinatario"] as string,
                ClienteFaturado = sqlDataReader["ds_Faturado"] as string,
                Conhecimento = sqlDataReader["nr_Conhecimento"] as string,
                Minuta = sqlDataReader["nr_Minuta"] as string,
                Referencia = sqlDataReader["nr_Referencia"] as string,
                PesoReal = sqlDataReader["kg_MercadoriaReal"] as decimal?,
                PesoTaxado = sqlDataReader["kg_Mercadoria"] as decimal?,
                Natureza = sqlDataReader["ds_NaturezaMercadoria"] as string,
                TipoMovimento = sqlDataReader["ds_TipoMovimento"] as string,
                FormaPagamento = sqlDataReader["ds_FormaPagamento"] as string,
                Comentario = sqlDataReader["cm_MovimentoMinuta"] as string,
                ComprovanteEntrega2 = ""
            };

            DateTime.TryParse(String.Format("{0:dd/MM/yyyy}", sqlDataReader["dt_ImpressaoConhecimento"]), out DateTime data);
            if (data != DateTime.MinValue)
                embarqueDetalhe.DataEmissao = sqlDataReader["dt_ImpressaoConhecimento"] as DateTime?;
            else
                embarqueDetalhe.DataEmissao = sqlDataReader["dt_Coleta"] as DateTime?;

            DateTime.TryParse(String.Format("{0:dd/MM/yyyy}", sqlDataReader["dt_Entrega"]), out DateTime dataEntrega);
            if (dataEntrega != DateTime.MinValue)
                embarqueDetalhe.ComprovanteEntrega = this.GetUrlComprovante + embarqueDetalhe.ID.ToString() + ".jpg";
            else
                embarqueDetalhe.ComprovanteEntrega = "";

            return embarqueDetalhe;
        }

        #endregion

        #region Funções NF
        private void CarregarNotaFiscal(SqlConnection sqlConnection, List<ConsultaEmbarqueDetalhe> detalheEmbarque)
        {
            foreach (var embarque in detalheEmbarque)
                this.CarregarNF(sqlConnection, embarque);
        }

        private void CarregarNF(SqlConnection sqlConnection, ConsultaEmbarqueDetalhe embarque)
        {
            embarque.ListaNotaFiscal = new List<ListaNotaFiscal>();
            var selectNF = RetornaSelectNF();

            using (var sqlCommand = new SqlCommand(selectNF.ToString(), sqlConnection))
            {
                AdicionaParametroNF(embarque, sqlCommand);

                using (var sqlDataReader = sqlCommand.ExecuteReader())
                {
                    while (sqlDataReader.Read())
                    {
                        var nfs = this.RetornaNF(sqlDataReader);
                        embarque.ListaNotaFiscal.Add(nfs);
                    }
                }
            }
        }

        private static void AdicionaParametroNF(ConsultaEmbarqueDetalhe embarque, SqlCommand sqlCommand)
        {
            sqlCommand.Parameters.AddWithValue("ID", embarque.ID);
        }

        private StringBuilder RetornaSelectNF()
        {
            var selectNF = new StringBuilder(@"SELECT rtrim(a.cd_NotaFiscal) as cd_NotaFiscal,a.qt_Volume,rtrim(a.cd_Serie) as cd_Serie,a.kg_Mercadoria,a.vl_NotaFiscal,rtrim(b.ds_NaturezaMercadoria) as ds_NaturezaMercadoria,a.dt_Emissao,rtrim(a.nr_PedidoCliente) as nr_PedidoCliente FROM tbdMovimentoNotaFiscal a (nolock) LEFT JOIN tbdNaturezaMercadoria b (nolock) ON a.id_NaturezaMercadoria = b.id_NaturezaMercadoria where a.id_Movimento = @ID");
            return selectNF;
        }

        private ListaNotaFiscal RetornaNF(SqlDataReader sqlDataReader)
        {
            var nf = new ListaNotaFiscal()
            {
                Numero = sqlDataReader["cd_NotaFiscal"] as string,
                Serie = sqlDataReader["cd_Serie"] as string,
                Valor = sqlDataReader["vl_NotaFiscal"] as decimal?,
                Volume = (int)sqlDataReader["qt_Volume"],
                Peso = sqlDataReader["kg_Mercadoria"] as decimal?,
                Data = sqlDataReader["dt_Emissao"] as DateTime?,
                NumeroPedido = sqlDataReader["nr_PedidoCliente"] as string
            };



            return nf;
        }
        #endregion

        #region Funções Coleta
        private void CarregarColeta(SqlConnection sqlConnection, List<ConsultaEmbarqueDetalhe> detalheEmbarque)
        {
            foreach (var embarque in detalheEmbarque)
                this.CarregarColetas(sqlConnection, embarque);
        }

        private void CarregarColetas(SqlConnection sqlConnection, ConsultaEmbarqueDetalhe embarque)
        {
            embarque.ListaColeta = new List<ListaColeta>();
            var selectColeta = RetornaSelectColeta();

            using (var sqlCommand = new SqlCommand(selectColeta.ToString(), sqlConnection))
            {
                AdicionaParametroColeta(embarque, sqlCommand);

                using (var sqlDataReader = sqlCommand.ExecuteReader())
                {
                    while (sqlDataReader.Read())
                    {
                        var coletas = this.RetornaColeta(sqlDataReader);
                        embarque.ListaColeta.Add(coletas);
                    }
                }
            }
        }

        private static void AdicionaParametroColeta(ConsultaEmbarqueDetalhe embarque, SqlCommand sqlCommand)
        {
            sqlCommand.Parameters.AddWithValue("ID", embarque.ID);
        }

        private StringBuilder RetornaSelectColeta()
        {
            var selectColeta = new StringBuilder(@"SELECT RTRIM(a.nr_ColetaManual) AS nr_ColetaManual,a.id_PedidoColeta AS nr_Coleta,a.dt_Coleta AS dt_Coleta,RTRIM(a.ds_Coletor) AS ds_Coletor,RTRIM(a.nr_DocumentoColetor) AS nr_DocumentoColetor,RTRIM(a.cm_coleta) AS cm_Coleta FROM tbdMovimento a (nolock) where a.id_Movimento = @ID");
            return selectColeta;
        }

        private ListaColeta RetornaColeta(SqlDataReader sqlDataReader)
        {
            var coleta = new ListaColeta()
            {
                Numero = sqlDataReader["nr_Coleta"] as string + " " + sqlDataReader["nr_ColetaManual"] as string,
                Data = sqlDataReader["dt_Coleta"] as DateTime?,
                Motorista = sqlDataReader["ds_Coletor"] as string,
                Documento = sqlDataReader["nr_DocumentoColetor"] as string,
                Comentario = sqlDataReader["cm_Coleta"] as string
            };

            return coleta;
        }
        #endregion

        #region Funções Manifesto
        private void CarregarManifesto(SqlConnection sqlConnection, List<ConsultaEmbarqueDetalhe> detalheEmbarque)
        {
            foreach (var embarque in detalheEmbarque)
                this.CarregarManifestos(sqlConnection, embarque);
        }

        private void CarregarManifestos(SqlConnection sqlConnection, ConsultaEmbarqueDetalhe embarque)
        {
            embarque.ListaManifesto = new List<ListaManifesto>();
            for (int i = 1; i <= 5; i++)
            {
                var selectManifesto = RetornaSelectManifesto(i);

                using (var sqlCommand = new SqlCommand(selectManifesto.ToString(), sqlConnection))
                {
                    AdicionaParametroManifesto(embarque, sqlCommand);

                    using (var sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        while (sqlDataReader.Read())
                        {
                            var manifestos = this.RetornaManifesto(sqlDataReader);
                            embarque.ListaManifesto.Add(manifestos);
                        }
                    }
                }
            }
        }

        private static void AdicionaParametroManifesto(ConsultaEmbarqueDetalhe embarque, SqlCommand sqlCommand)
        {
            sqlCommand.Parameters.AddWithValue("ID", embarque.ID);
        }

        private StringBuilder RetornaSelectManifesto(int indiceQuery)
        {
            StringBuilder selectManifesto = new StringBuilder();

            switch (indiceQuery)
            {
                case 1:
                    selectManifesto = new StringBuilder(@"select b.id_Manifesto, RTRIM(d.ds_Pessoa) as ds_Pessoa, RTRIM(c.cd_Placa) as cd_Placa, b.dt_Saida, RTRIM(b.hr_Saida) as hr_Saida, b.dt_Chegada, RTRIM(b.hr_Chegada) as hr_Chegada, RTRIM(i.ds_Cliente) as ds_Cliente, RTRIM(h.ds_Rota) as ds_Rota, RTRIM(a.ds_Tipo) as ds_Tipo from (((((tbdMovimento g (nolock) inner join tbdManifestoMovimento a (nolock) on g.id_PedidoColeta = a.id_PedidoColeta) inner join tbdManifesto b (nolock) on a.id_Manifesto = b.id_Manifesto) inner join tbdVeiculo c (nolock) on b.id_Veiculo = c.id_Veiculo) inner join tbdPessoa d on b.id_Motorista = d.id_Pessoa) left join tbdRota h (nolock) on b.id_Rota = h.id_Rota) left join tbdClienteSistema i (nolock) on b.id_Cliente = i.id_Cliente where g.id_Movimento = @ID order by b.id_Manifesto");
                    break;
                case 2:
                    selectManifesto = new StringBuilder(@"select b.id_Manifesto, RTRIM(d.ds_Pessoa) as ds_Pessoa, RTRIM(c.cd_Placa) as cd_Placa, b.dt_Saida, RTRIM(b.hr_Saida) as hr_Saida, b.dt_Chegada, RTRIM(b.hr_Chegada) as hr_Chegada, RTRIM(i.ds_Cliente) as ds_Cliente, RTRIM(h.ds_Rota) as ds_Rota, RTRIM(a.ds_Tipo) as ds_Tipo from ((((tbdManifestoMovimento a (nolock) inner join tbdManifesto b (nolock) on a.id_Manifesto = b.id_Manifesto) inner join tbdVeiculo c (nolock) on b.id_Veiculo = c.id_Veiculo) inner join tbdPessoa d (nolock) on b.id_Motorista = d.id_Pessoa) left join tbdRota h (nolock) on b.id_Rota = h.id_Rota) left join tbdClienteSistema i (nolock) on b.id_Cliente = i.id_Cliente where a.id_Movimento = @ID order by b.id_Manifesto");
                    break;
                case 3:
                    selectManifesto = new StringBuilder(@"select b.id_Manifesto, RTRIM(d.ds_Pessoa) as ds_Pessoa, RTRIM(c.cd_Placa) as cd_Placa, b.dt_Saida, RTRIM(b.hr_Saida) as hr_Saida, b.dt_Chegada, RTRIM(b.hr_Chegada) as hr_Chegada, RTRIM(l.ds_Cliente) as ds_Cliente, RTRIM(j.ds_Rota) as ds_Rota, 'Retirada' as ds_Tipo from (((((((tbdMovimento g (nolock) inner join tbdFechamentoCiaAereaMovimento h (nolock) on g.id_Movimento = h.id_Movimento) inner join tbdFechamentoCiaAerea i (nolock) on h.id_FechamentoCiaAerea = i.id_FechamentoCiaAerea) inner join tbdManifestoAWB a (nolock) on i.id_FechamentoCiaAerea = a.id_Fechamento) inner join tbdManifesto b (nolock) on a.id_Manifesto = b.id_Manifesto) inner join tbdVeiculo c (nolock) on b.id_Veiculo = c.id_Veiculo) inner join tbdPessoa d (nolock) on b.id_Motorista = d.id_Pessoa) left join tbdRota j (nolock) on b.id_Rota = j.id_Rota) left join tbdClienteSistema l (nolock) on b.id_Cliente = l.id_Cliente where g.id_Movimento = @ID order by b.id_Manifesto");
                    break;
                case 4:
                    selectManifesto = new StringBuilder(@"select b.id_Manifesto, RTRIM(d.ds_Pessoa) as ds_Pessoa, RTRIM(c.cd_Placa) as cd_Placa, b.dt_Saida, RTRIM(b.hr_Saida) as hr_Saida, b.dt_Chegada, RTRIM(b.hr_Chegada) as hr_Chegada, RTRIM(l.ds_Cliente) as ds_Cliente, RTRIM(j.ds_Rota) as ds_Rota, 'Despacho' as ds_Tipo from (((((tbdMovimento g (nolock) inner join tbdManifestoTG30 a (nolock) on g.id_TG30 = a.id_TG30) inner join tbdManifesto b (nolock) on a.id_Manifesto = b.id_Manifesto) inner join tbdVeiculo c (nolock) on b.id_Veiculo = c.id_Veiculo) inner join tbdPessoa d (nolock) on b.id_Motorista = d.id_Pessoa) left join tbdRota j (nolock) on b.id_Rota = j.id_Rota) left join tbdClienteSistema l (nolock) on b.id_Cliente = l.id_Cliente where g.id_Movimento = @ID order by b.id_Manifesto");
                    break;
                case 5:
                    selectManifesto = new StringBuilder(@"select b.id_Manifesto, RTRIM(d.ds_Pessoa) as ds_Pessoa, RTRIM(c.cd_Placa) as cd_Placa, b.dt_Saida, RTRIM(b.hr_Saida) as hr_Saida, b.dt_Chegada, RTRIM(b.hr_Chegada) as hr_Chegada, RTRIM(l.ds_Cliente) as ds_Cliente, RTRIM(j.ds_Rota) as ds_Rota, 'Despacho' as ds_Tipo from (((((tbdMovimento g (nolock) inner join tbdManifestoRelacaoCarga a (nolock) on g.id_RelacaoCarga = a.id_RelacaoCarga) inner join tbdManifesto b (nolock) on a.id_Manifesto = b.id_Manifesto) inner join tbdVeiculo c (nolock) on b.id_Veiculo = c.id_Veiculo) inner join tbdPessoa d (nolock) on b.id_Motorista = d.id_Pessoa) left join tbdRota j (nolock) on b.id_Rota = j.id_Rota) left join tbdClienteSistema l (nolock) on b.id_Cliente = l.id_Cliente where g.id_Movimento = @ID order by b.id_Manifesto");
                    break;
            }
            return selectManifesto;
        }

        private ListaManifesto RetornaManifesto(SqlDataReader sqlDataReader)
        {
            var manifesto = new ListaManifesto();
            string DataSaida, HoraSaida, DataChegada, HoraChegada;

            DataSaida = String.Format("{0:dd/MM/yyyy}", sqlDataReader["dt_Saida"]);
            HoraSaida = sqlDataReader["hr_Saida"] as string;
            manifesto.DataSaida = DataSaida + ' ' + HoraSaida;

            DataChegada = String.Format("{0:dd/MM/yyyy}", sqlDataReader["dt_Chegada"]);
            HoraChegada = sqlDataReader["hr_Chegada"] as string;
            manifesto.DataChegada = DataChegada + ' ' + HoraChegada;

            manifesto.Manifesto = (int)sqlDataReader["id_Manifesto"];
            manifesto.Motorista = sqlDataReader["ds_Pessoa"] as string;
            manifesto.Veiculo = sqlDataReader["cd_Placa"] as string;
            manifesto.Origem = sqlDataReader["ds_Cliente"] as string;
            manifesto.Rota = sqlDataReader["ds_Rota"] as string;
            manifesto.TipoManifesto = sqlDataReader["ds_Tipo"] as string;

            return manifesto;
        }
        #endregion

        #region Funções Dados Voo
        private void CarregarVoo(SqlConnection sqlConnection, List<ConsultaEmbarqueDetalhe> detalheEmbarque)
        {
            foreach (var embarque in detalheEmbarque)
                this.CarregarDadosVoo(sqlConnection, embarque);
        }

        private void CarregarDadosVoo(SqlConnection sqlConnection, ConsultaEmbarqueDetalhe embarque)
        {
            embarque.ListaDadosVoo = new List<ListaDadosVoo>();
            var selectDadosVoo = RetornaSelectDadosVoo();

            using (var sqlCommand = new SqlCommand(selectDadosVoo.ToString(), sqlConnection))
            {
                AdicionaParametroDadosVoo(embarque, sqlCommand);

                using (var sqlDataReader = sqlCommand.ExecuteReader())
                {
                    while (sqlDataReader.Read())
                    {
                        var voo = this.RetornaDadosVoo(sqlDataReader);
                        embarque.ListaDadosVoo.Add(voo);
                    }
                }
            }
        }

        private static void AdicionaParametroDadosVoo(ConsultaEmbarqueDetalhe embarque, SqlCommand sqlCommand)
        {
            sqlCommand.Parameters.AddWithValue("ID", embarque.ID);
        }

        private StringBuilder RetornaSelectDadosVoo()
        {
            var selectDadosVoo = new StringBuilder(@"select b.tp_VooConfirmado,RTRIM(b.nr_AWB) AS nr_AWB,RTRIM(c.ds_Pessoa) AS ds_Pessoa,RTRIM(b.nr_Voo) as nr_Voo,b.dt_Saida,b.hr_Voo,b.dt_Chegada,b.hr_Chegada from tbdFechamentoCiaAereaMovimento a (nolock) INNER JOIN tbdFechamentoCiaAerea b (nolock) ON a.id_FechamentoCiaAerea=b.id_FechamentoCiaAerea INNER JOIN tbdPessoa c (nolock) ON b.id_CiaAerea=c.id_Pessoa where a.id_Movimento = @ID order by dt_Chegada DESC,hr_Chegada ASC,dt_Saida DESC,hr_Voo ASC");
            return selectDadosVoo;
        }

        private ListaDadosVoo RetornaDadosVoo(SqlDataReader sqlDataReader)
        {
            var voo = new ListaDadosVoo();
            string DataSaida, HoraSaida, DataChegada, HoraChegada;

            DataSaida = String.Format("{0:dd/MM/yyyy}", sqlDataReader["dt_Saida"]);
            HoraSaida = sqlDataReader["hr_Voo"] as string;
            voo.DataSaida = DataSaida + ' ' + HoraSaida;

            DataChegada = String.Format("{0:dd/MM/yyyy}", sqlDataReader["dt_Chegada"]);
            HoraChegada = sqlDataReader["hr_Chegada"] as string;
            voo.DataChegada = DataChegada + ' ' + HoraChegada;

            voo.CiaAerea = sqlDataReader["ds_Pessoa"] as string;
            voo.NumeroVoo = sqlDataReader["nr_Voo"] as string;
            if (sqlDataReader["tp_VooConfirmado"] as string != "S")
                voo.Confirmado = "Não";
            else
                voo.Confirmado = "Sim";

            return voo;
        }
        #endregion

        #region Funções Consolidação
        private void CarregarConsolidacao(SqlConnection sqlConnection, List<ConsultaEmbarqueDetalhe> detalheEmbarque)
        {
            foreach (var embarque in detalheEmbarque)
                this.CarregaConsolidacao(sqlConnection, embarque);
        }

        private void CarregaConsolidacao(SqlConnection sqlConnection, ConsultaEmbarqueDetalhe embarque)
        {
            embarque.ListaConsolidacao = new List<ListaConsolidacao>();
            var selectConsolidacao = RetornaSelectConsolidacao();

            using (var sqlCommand = new SqlCommand(selectConsolidacao.ToString(), sqlConnection))
            {
                AdicionaParametroConsolidacao(embarque, sqlCommand);

                using (var sqlDataReader = sqlCommand.ExecuteReader())
                {
                    while (sqlDataReader.Read())
                    {
                        var consolidacao = this.RetornaConsolidacao(sqlDataReader);
                        embarque.ListaConsolidacao.Add(consolidacao);
                    }
                }
            }
        }

        private static void AdicionaParametroConsolidacao(ConsultaEmbarqueDetalhe embarque, SqlCommand sqlCommand)
        {
            sqlCommand.Parameters.AddWithValue("ID", embarque.ID);
        }

        private StringBuilder RetornaSelectConsolidacao()
        {
            var selectConsolidacao = new StringBuilder(@"select RTRIM(b.nr_Conhecimento) AS nr_Conhecimento,RTRIM(c.ds_Pessoa) AS ds_Pessoa,b.dt_Fechamento,b.hr_Voo,b.dt_Chegada,b.hr_Chegada,b.tp_NaoExibirSislogNet, tp_Confirmado from tbdfechamentoTranspMovimento a (nolock) INNER JOIN tbdFechamentoTransportadora b (nolock) ON a.id_FechamentoTransportadora=b.id_FechamentoTransportadora INNER JOIN tbdPessoa c (nolock) ON b.id_Transportadora=c.id_Pessoa where a.id_Movimento = @ID order by dt_Chegada DESC,hr_Chegada ASC,dt_Fechamento DESC,hr_Voo ASC");
            return selectConsolidacao;
        }

        private ListaConsolidacao RetornaConsolidacao(SqlDataReader sqlDataReader)
        {
            var consolidacao = new ListaConsolidacao();
            string DataSaida, HoraSaida, DataChegada, HoraChegada;

            DataSaida = String.Format("{0:dd/MM/yyyy}", sqlDataReader["dt_Fechamento"]);
            HoraSaida = sqlDataReader["hr_Voo"] as string;
            consolidacao.DataSaida = DataSaida + ' ' + HoraSaida;

            DataChegada = String.Format("{0:dd/MM/yyyy}", sqlDataReader["dt_Chegada"]);
            HoraChegada = sqlDataReader["hr_Chegada"] as string;
            consolidacao.DataChegada = DataChegada + ' ' + HoraChegada;

            consolidacao.Transportadora = sqlDataReader["ds_Pessoa"] as string;
            consolidacao.CTRC3 = sqlDataReader["nr_Conhecimento"] as string;
            if (sqlDataReader["tp_Confirmado"] as string != "S")
                consolidacao.Confirmado = "Não";
            else
                consolidacao.Confirmado = "Sim";

            return consolidacao;
        }
        #endregion

        #region Funções Entrega
        private void CarregarEntrega(SqlConnection sqlConnection, List<ConsultaEmbarqueDetalhe> detalheEmbarque)
        {
            foreach (var embarque in detalheEmbarque)
                this.CarregarEntregas(sqlConnection, embarque);
        }

        private void CarregarEntregas(SqlConnection sqlConnection, ConsultaEmbarqueDetalhe embarque)
        {
            embarque.ListaEntrega = new List<ListaEntrega>();
            var selectEntrega = RetornaSelectEntrega();

            using (var sqlCommand = new SqlCommand(selectEntrega.ToString(), sqlConnection))
            {
                AdicionaParametroEntrega(embarque, sqlCommand);

                using (var sqlDataReader = sqlCommand.ExecuteReader())
                {
                    while (sqlDataReader.Read())
                    {
                        var entrega = this.RetornaEntrega(sqlDataReader);
                        embarque.ListaEntrega.Add(entrega);
                    }
                }
            }
        }

        private static void AdicionaParametroEntrega(ConsultaEmbarqueDetalhe embarque, SqlCommand sqlCommand)
        {
            sqlCommand.Parameters.AddWithValue("ID", embarque.ID);
        }

        private StringBuilder RetornaSelectEntrega()
        {
            var selectEntrega = new StringBuilder(@"select a.dt_Recepcao AS dt_Recepcao,a.hr_recepcao,RTRIM(a.ds_Receptor) AS ds_Receptor,RTRIM(a.nr_DocumentoReceptor) AS nr_DocumentoReceptor,RTRIM(a.cm_Recepcao) AS cm_Recepcao from tbdMovimento a (nolock) where a.id_Movimento = @ID");
            return selectEntrega;
        }

        private ListaEntrega RetornaEntrega(SqlDataReader sqlDataReader)
        {
            var entrega = new ListaEntrega();
            string data, hora;

            data = String.Format("{0:dd/MM/yyyy}", sqlDataReader["dt_Recepcao"]);
            hora = sqlDataReader["hr_Recepcao"] as string;
            entrega.Data = data + ' ' + hora;
            entrega.Recebedor = sqlDataReader["ds_Receptor"] as string;
            entrega.DocumentoRecebedor = sqlDataReader["nr_DocumentoReceptor"] as string;
            entrega.Comentario = sqlDataReader["cm_Recepcao"] as string;

            return entrega;
        }
        #endregion

        #region Funções OcorrenciasDetalhe
        private void CarregarOcorrenciaDetalhe(SqlConnection sqlConnection, List<ConsultaEmbarqueDetalhe> detalheEmbarque)
        {
            foreach (var embarque in detalheEmbarque)
                this.CarregarOcorrenciasDetalhe(sqlConnection, embarque);
        }

        private void CarregarOcorrenciasDetalhe(SqlConnection sqlConnection, ConsultaEmbarqueDetalhe embarque)
        {
            embarque.ListaOcorrencia = new List<ListaOcorrencia>();
            var selectOcorrenciaDetalhe = RetornaSelectOcorrenciaDetalhe();

            using (var sqlCommand = new SqlCommand(selectOcorrenciaDetalhe.ToString(), sqlConnection))
            {
                AdicionaParametroOcorrenciaDetalhe(embarque, sqlCommand);

                using (var sqlDataReader = sqlCommand.ExecuteReader())
                {
                    while (sqlDataReader.Read())
                    {
                        var ocorrencia = this.RetornaOcorrenciaDetalhe(sqlDataReader);
                        embarque.ListaOcorrencia.Add(ocorrencia);
                    }
                }
            }
        }

        private static void AdicionaParametroOcorrenciaDetalhe(ConsultaEmbarqueDetalhe embarque, SqlCommand sqlCommand)
        {
            sqlCommand.Parameters.AddWithValue("ID", embarque.ID);
        }

        private StringBuilder RetornaSelectOcorrenciaDetalhe()
        {
            var selectOcorrenciaDetalhe = new StringBuilder(@"Select RTRIM(a.ds_Ocorrencia) as cm_Ocorrencia, RTRIM(b.ds_Ocorrencia) as ds_Ocorrencia, a.dt_PrazoFechamento as dt_Ocorrencia, RTRIM(nr_NotaFiscal) as cd_Nota, RTRIM(a.hr_PrazoFechamento) as hr_Ocorrencia FROM tbdOcorrenciaNota a (nolock) LEFT JOIN tbdOcorrencia b (nolock) ON a.id_Ocorrencia = b.id_Ocorrencia LEFT JOIN tbdOcorrenciaManifesto c (nolock) on c.id_OcorrenciaManifesto=a.id_Ocorrencia where a.id_Movimento = @ID order by a.dt_PrazoFechamento, a.hr_PrazoFechamento, b.ds_Ocorrencia,nr_NotaFiscal");
            return selectOcorrenciaDetalhe;
        }

        private ListaOcorrencia RetornaOcorrenciaDetalhe(SqlDataReader sqlDataReader)
        {
            var ocorrencia = new ListaOcorrencia();
            string data, hora;

            data = String.Format("{0:dd/MM/yyyy}", sqlDataReader["dt_Ocorrencia"]);
            hora = sqlDataReader["hr_Ocorrencia"] as string;
            ocorrencia.Data = data + ' ' + hora;

            ocorrencia.Nota = sqlDataReader["cd_Nota"] as string;
            ocorrencia.Ocorrencia = sqlDataReader["ds_Ocorrencia"] as string;
            ocorrencia.Comentario = sqlDataReader["cm_Ocorrencia"] as string;

            return ocorrencia;
        }
        #endregion

        #region Funções Tracking
        private void CarregarTracking(SqlConnection sqlConnection, List<ConsultaEmbarqueDetalhe> detalheEmbarque)
        {
            foreach (var embarque in detalheEmbarque)
                this.CarregarTrackings(sqlConnection, embarque);
        }

        private void CarregarTrackings(SqlConnection sqlConnection, ConsultaEmbarqueDetalhe embarque)
        {
            embarque.ListaTracking = new List<ListaTracking>();
            var selectTracking = RetornaSelectTracking();

            using (var sqlCommand = new SqlCommand(selectTracking.ToString(), sqlConnection))
            {
                AdicionaParametroTracking(embarque, sqlCommand);

                using (var sqlDataReader = sqlCommand.ExecuteReader())
                {
                    while (sqlDataReader.Read())
                    {
                        var tracking = this.RetornaTracking(sqlDataReader);
                        embarque.ListaTracking.Add(tracking);
                    }
                }
            }
        }

        private static void AdicionaParametroTracking(ConsultaEmbarqueDetalhe embarque, SqlCommand sqlCommand)
        {
            sqlCommand.Parameters.AddWithValue("ID", embarque.ID);
        }

        private StringBuilder RetornaSelectTracking()
        {
            var selectTracking = new StringBuilder(@"Select dt_Tracking, hr_Tracking, cm_Tracking FROM tbdMovimentoTracking a (nolock) where a.id_Movimento = @ID order by dt_Tracking,hr_Tracking");
            return selectTracking;
        }

        private ListaTracking RetornaTracking(SqlDataReader sqlDataReader)
        {
            var tracking = new ListaTracking()
            {
                Data = sqlDataReader["dt_Tracking"] as DateTime?,
                Hora = sqlDataReader["hr_Tracking"] as string,
                Comentario = sqlDataReader["cm_Tracking"] as string

            };

            return tracking;
        }
        #endregion

        #region Funções ComposicaoFrete
        private void CarregarComposicaoFrete(SqlConnection sqlConnection, List<ConsultaEmbarqueDetalhe> detalheEmbarque)
        {
            foreach (var embarque in detalheEmbarque)
                this.CarregaComposicaoFrete(sqlConnection, embarque);
        }

        private void CarregaComposicaoFrete(SqlConnection sqlConnection, ConsultaEmbarqueDetalhe embarque)
        {
            embarque.ListaComposicaoFrete = new List<ListaComposicaoFrete>();
            var selectComposicaoFrete = RetornaSelectComposicaoFrete();

            using (var sqlCommand = new SqlCommand(selectComposicaoFrete.ToString(), sqlConnection))
            {
                AdicionaParametroComposicaoFrete(embarque, sqlCommand);

                using (var sqlDataReader = sqlCommand.ExecuteReader())
                {
                    while (sqlDataReader.Read())
                    {
                        var frete = this.RetornaComposicaoFrete(sqlDataReader);
                        embarque.ListaComposicaoFrete.Add(frete);
                    }
                }
            }
        }

        private static void AdicionaParametroComposicaoFrete(ConsultaEmbarqueDetalhe embarque, SqlCommand sqlCommand)
        {
            sqlCommand.Parameters.AddWithValue("ID", embarque.ID);
        }

        private StringBuilder RetornaSelectComposicaoFrete()
        {
            var selectComposicaoFrete = new StringBuilder(@"Select a.vl_Frete,a.vl_Pedagio,a.vl_Despacho,a.vl_SETCAT,a.vl_ITR,a.vl_ExcedenteColeta + a.vl_Coleta AS vl_Coleta,a.vl_ExcedenteEntrega + a.vl_Entrega AS vl_Entrega,a.vl_TaxasDiversas,a.vl_Redespacho,a.vl_Suframa,a.vl_advalorem,a.vl_Ademe,a.vl_tda,a.vl_Desconto,a.vl_Acrescimo,a.vl_Emergencia,a.vl_ttd,a.vl_Peso,a.vl_icms,a.vl_iss FROM tbdMovimento a (nolock) where a.id_Movimento = @ID");
            return selectComposicaoFrete;
        }

        private ListaComposicaoFrete RetornaComposicaoFrete(SqlDataReader sqlDataReader)
        {
            var frete = new ListaComposicaoFrete()
            {
                FretePeso = sqlDataReader["vl_Peso"] as decimal?,
                Coleta = sqlDataReader["vl_Coleta"] as decimal?,
                Entrega = sqlDataReader["vl_Entrega"] as decimal?,
                Redespacho = sqlDataReader["vl_Redespacho"] as decimal?,
                Emergencia = sqlDataReader["vl_Emergencia"] as decimal?,
                TDA = sqlDataReader["vl_TDA"] as decimal?,
                TTD = sqlDataReader["vl_TTD"] as decimal?,
                TaxasDiversas = sqlDataReader["vl_TaxasDiversas"] as decimal?,
                ADValorem = sqlDataReader["vl_AdValorem"] as decimal?,
                Suframa = sqlDataReader["vl_Suframa"] as decimal?,
                GRIS = sqlDataReader["vl_Ademe"] as decimal?,
                ISS = sqlDataReader["vl_ISS"] as decimal?,
                ICMS = sqlDataReader["vl_ICMS"] as decimal?,
                Acrescimo = sqlDataReader["vl_Acrescimo"] as decimal?,
                Descontos = sqlDataReader["vl_Desconto"] as decimal?,
                Pedagio = sqlDataReader["vl_Pedagio"] as decimal?,
                Despacho = sqlDataReader["vl_Despacho"] as decimal?,
                SETCAT = sqlDataReader["vl_SETCAT"] as decimal?,
                ITR = sqlDataReader["vl_ITR"] as decimal?,
                Total = sqlDataReader["vl_Frete"] as decimal?
            };

            return frete;
        }
        #endregion

        #region Funções Comprovante
        private void CarregarComprovanteRatreamento(SqlConnection sqlConnection, List<ConsultaRastreamento> rastreamentos)
        {
            foreach (var rastreamento in rastreamentos)
                this.CarregarComprovantesRatreamento(sqlConnection, rastreamento);
        }

        private void CarregarComprovanteEmbarque(SqlConnection sqlConnection, List<ConsultaEmbarque> embarques)
        {
            foreach (var embarque in embarques)
                this.CarregarComprovantesEmbarque(sqlConnection, embarque);
        }

        private void CarregarComprovantesRatreamento(SqlConnection sqlConnection, ConsultaRastreamento rastreamento)
        {
            rastreamento.ListaComprovante = new List<ListaComprovante>();
            //string URLNovoComprovante = "";
            var lista = new ListaComprovante() { URLComprovante = "" };

            rastreamento.ListaComprovante.Add(lista);

            ////Validar a URL
            //string[] nomeArquivo = new string[] { "", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };

            //foreach (var item in nomeArquivo)
            //{
            //    URLNovoComprovante = this.urlComprovante + rastreamento.IDMovimento.ToString() + item.ToString() + ".jpg";
            //    if (ValidarArquivo(URLNovoComprovante))
            //    {
            //        var lista = new ListaComprovante();
            //        lista.URLComprovante = URLNovoComprovante;
            //        rastreamento.ListaComprovante.Add(lista);
            //    }
            //}
        }

        private void CarregarComprovantesEmbarque(SqlConnection sqlConnection, ConsultaEmbarque embarque)
        {
            embarque.ListaComprovante = new List<ListaComprovante>();
            //string URLNovoComprovante = "";

            var lista = new ListaComprovante() { URLComprovante = "" };
            embarque.ListaComprovante.Add(lista);

            ////Validar a URL
            //string[] nomeArquivo = new string[] { "", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };

            //foreach (var item in nomeArquivo)
            //{
            //    URLNovoComprovante = this.urlComprovante + embarque.IDMovimento.ToString() + item.ToString() + ".jpg";
            //    if (ValidarArquivo(URLNovoComprovante))
            //    {
            //        var lista = new ListaComprovante();
            //        lista.URLComprovante = URLNovoComprovante;
            //        embarque.ListaComprovante.Add(lista);
            //    }
            //}
        }

        private bool ValidarArquivo(string PathArquivo)
        {
            var url = PathArquivo;
            HttpWebResponse response = null;
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "HEAD";

            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException)
            {
                /* A WebException will be thrown if the status of the response is not `200 OK` */
                return false;
            }
            finally
            {
                // Don't forget to close your response.
                if (response != null)
                {
                    response.Close();
                }
            }
            return true;
        }

        #endregion
    }
}
