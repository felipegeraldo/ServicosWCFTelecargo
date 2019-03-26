using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Teste
{
    public partial class Form1 : Form
    {
        string usuario;
        string senha;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtEstado.Text = "SP";
            txtCidade.Text = "";
            txtDtVencInicio.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtDtVencFim.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtDtEmisInicio.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtDtEmisFim.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtInicioRastr.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtFinalRastr.Text = DateTime.Now.ToString("dd/MM/yyyy");
            usuario = txtUsuario.Text;
            senha = txtSenha.Text;
            
        }

        private void CarregaUsuario()
        {
            usuario = txtUsuario.Text;
            senha = txtSenha.Text;
        }

        private void button5_Click(object sender, EventArgs e) //Login
        {
            //var servico = new ServiceConsultaTelecargo.ConsultasClient(); //Producao
            var servico = new ServiceLocal.ConsultasClient(); //Consulta Local
            CarregaUsuario();

            servico.RetornarLoginClienteCompleted += (sender2, args) =>
            {
                txtRetorno.Text = args.Result;
                webBrowser1.DocumentText = args.Result;
            };
            
            servico.RetornarLoginClienteAsync(usuario, senha);
        }

        private void button6_Click(object sender, EventArgs e) //Centro Custo
        {
            //var servico = new ServiceConsultaTelecargo.ConsultasClient(); //Producao
            var servico = new ServiceLocal.ConsultasClient(); //Consulta Local
            CarregaUsuario();

            servico.ListarCentroCustoCompleted += (sender2, args) =>
            {
                txtRetorno.Text = args.Result;
                webBrowser1.DocumentText = args.Result;
            };
            int id_Remetente = 0;
            if (txtRemetente.Text != "") id_Remetente = Convert.ToInt32(txtRemetente.Text);
            servico.ListarCentroCustoAsync(usuario, senha, id_Remetente);
        }

        private void button7_Click(object sender, EventArgs e) //Cidades
        {
            //var servico = new ServiceConsultaTelecargo.ConsultasClient(); //Producao
            var servico = new ServiceLocal.ConsultasClient(); //Consulta Local
            CarregaUsuario();

            servico.ListarCidadeCompleted += (sender2, args) =>
            {
                txtRetorno.Text = args.Result;
                webBrowser1.DocumentText = args.Result;
            };

            servico.ListarCidadeAsync(usuario, senha, txtEstado.Text, txtCidade.Text);
        }

        private void button8_Click(object sender, EventArgs e) // Pessoa
        {
            //var servico = new ServiceConsultaTelecargo.ConsultasClient(); //Producao
            var servico = new ServiceLocal.ConsultasClient(); //Consulta Local
            CarregaUsuario();

            servico.ListarPessoaCompleted += (sender2, args) =>
            {
                txtRetorno.Text = args.Result;
                webBrowser1.DocumentText = args.Result;
            };
            int idPessoa = 0;
            if (txtIDPessoa.Text != "")
                idPessoa = Convert.ToInt32(txtIDPessoa.Text);

            Boolean empresa = chkEmpresa.Checked;
            servico.ListarPessoaAsync(usuario, senha, empresa, txtCGC.Text, txtNome.Text, idPessoa);
        }

        private void button9_Click(object sender, EventArgs e) // Financeiro
        {
            //var servico = new ServiceConsultaTelecargo.ConsultasClient(); //Producao
            var servico = new ServiceLocal.ConsultasClient(); //Consulta Local
            CarregaUsuario();

            servico.ConsultarFinanceiroCompleted += (sender2, args) =>
            {
                txtRetorno.Text = args.Result;
                webBrowser1.DocumentText = args.Result;
            };

            int idEmpresa = 0;
            if (txtIDEmpresa.Text != "")
                idEmpresa = Convert.ToInt32(txtIDEmpresa.Text);

            string tipoDocumento = "";
            if (optPago.Checked) { tipoDocumento = "PAGO"; } else { if (optAberto.Checked) { tipoDocumento = "ABERTO"; } else { if (optAtrasado.Checked) { tipoDocumento = "Atrasado"; } else { tipoDocumento = "Todos"; } } }
            servico.ConsultarFinanceiroAsync(usuario, senha, tipoDocumento, nroDocumento.Text, txtDtVencInicio.Text, txtDtVencFim.Text, txtDtEmisInicio.Text, txtDtEmisFim.Text, idEmpresa);
        }

        private void button11_Click(object sender, EventArgs e) //Embarque
        {
            //var servico = new ServiceConsultaTelecargo.ConsultasClient(); //Producao
            var servico = new ServiceLocal.ConsultasClient(); //Consulta Local
            CarregaUsuario();

            servico.EmbarqueCompleted += (sender2, args) =>
            {
                txtRetorno.Text = args.Result;
                webBrowser1.DocumentText = args.Result;
            };

            servico.EmbarqueComModalCompleted += (sender2, args) =>
            {
                txtRetorno.Text = args.Result;
                webBrowser1.DocumentText = args.Result;
            };

            int idRemetenteRastr = 0;
            if (txtRemetente.Text != "")
                idRemetenteRastr = Convert.ToInt32(txtRemetente.Text);
            int idDestinatarioRastr = 0;
            if (txtIDDest.Text != "")
                idDestinatarioRastr = Convert.ToInt32(txtIDDest.Text);
            int idCC = 0;
            if (txtIDCC.Text != "")
                idCC = Convert.ToInt32(txtIDCC.Text);
            int idCidadeOri = 0;
            if (txtCidadeOri.Text != "")
                idCidadeOri = Convert.ToInt32(txtCidadeOri.Text);
            int idCidadeDest = 0;
            if (txtCidadeDest.Text != "")
                idCidadeDest = Convert.ToInt32(txtCidadeDest.Text);

            string tipoRastreamento = "";
            if (optSim.Checked) { tipoRastreamento = "SIM"; }
            else
            {
                if (optNao.Checked) { tipoRastreamento = "NAO"; }
                else { tipoRastreamento = "Ambos"; }
            }

            string modal = "T";
            if (rdbModalAmbos.Checked != true)
            {
                if (rdbModalTerrestre.Checked == true)
                {
                    modal = "T";
                }
                else
                {
                    modal = "A";
                }
            }

            if (rdbModalAmbos.Checked)
            {
                servico.EmbarqueAsync(usuario, senha, txtInicioRastr.Text, txtFinalRastr.Text, idRemetenteRastr, idDestinatarioRastr, tipoRastreamento, txtNF.Text, txtConhecimento.Text, idCC, txtUFOri.Text, idCidadeOri, txtUFDest.Text, idCidadeDest);
            }
            else
            {
                servico.EmbarqueComModalAsync(usuario, senha, txtInicioRastr.Text, txtFinalRastr.Text, idRemetenteRastr, idDestinatarioRastr, tipoRastreamento, txtNF.Text, txtConhecimento.Text, idCC, txtUFOri.Text, idCidadeOri, txtUFDest.Text, idCidadeDest, modal);
            }
        }

        private void button12_Click(object sender, EventArgs e) //Detalhe embarque
        {
            //var servico = new ServiceConsultaTelecargo.ConsultasClient(); //Producao
            var servico = new ServiceLocal.ConsultasClient(); //Consulta Local
            CarregaUsuario();

            servico.DetalheEmbarqueCompleted += (sender2, args) =>
            {
                txtRetorno.Text = args.Result;
                webBrowser1.DocumentText = args.Result;
            };
            int id_Movimento = 0;
            if (txtMovimento.Text != "") id_Movimento = Convert.ToInt32(txtMovimento.Text);
            servico.DetalheEmbarqueAsync(usuario, senha, id_Movimento);
        }

        private void btnRastreamentoLocal_Click(object sender, EventArgs e)
        {
            //var servico = new ServiceTelecargo.ConsultasClient(); //Teste
            //var servico = new ServiceConsultaTelecargo.ConsultasClient(); //Producao
            var servico = new ServiceLocal.ConsultasClient(); //Consulta Local
            CarregaUsuario();

            servico.RastreamentoCompleted += (sender2, args) =>
            {
                txtRetorno.Text = args.Result;
                webBrowser1.DocumentText = args.Result;
            };

            servico.RastreamentoComModalCompleted += (sender2, args) =>
            {
                txtRetorno.Text = args.Result;
                webBrowser1.DocumentText = args.Result;
            };

            int idRemetenteRastr = 0;
            if (txtIdRemet.Text != "")
                idRemetenteRastr = Convert.ToInt32(txtIdRemet.Text);
            int idDestinatarioRastr = 0;
            if (txtIDDest.Text != "")
                idDestinatarioRastr = Convert.ToInt32(txtIDDest.Text);
            int idCC = 0;
            if (txtIDCC.Text != "")
                idCC = Convert.ToInt32(txtIDCC.Text);
            int idCidadeOri = 0;
            if (txtCidadeOri.Text != "")
                idCidadeOri = Convert.ToInt32(txtCidadeOri.Text);
            int idCidadeDest = 0;
            if (txtCidadeDest.Text != "")
                idCidadeDest = Convert.ToInt32(txtCidadeDest.Text);
            int idFinanceiro = 0;
            if (txtIDFInanc.Text != "")
                idFinanceiro = Convert.ToInt32(txtIDFInanc.Text);

            string tipoRastreamento = "";
            if (optSim.Checked) { tipoRastreamento = "SIM"; }
            else
            {
                if (optNao.Checked) { tipoRastreamento = "NAO"; }
                else { tipoRastreamento = "Ambos"; }
            }

            string modal = "T";
            if (rdbModalAmbos.Checked != true)
            {                
                if (rdbModalTerrestre.Checked == true)
                {
                    modal = "T";
                }
                else
                {
                    modal = "A";
                }
            }

            if (rdbModalAmbos.Checked)
            {
                servico.RastreamentoAsync(usuario, senha, txtInicioRastr.Text, txtFinalRastr.Text, idRemetenteRastr, idDestinatarioRastr, tipoRastreamento, txtNF.Text, txtConhecimento.Text, idCC, txtUFOri.Text, idCidadeOri, txtUFDest.Text, idCidadeDest, idFinanceiro);
            }
            else
            {
                servico.RastreamentoComModalAsync(usuario, senha, txtInicioRastr.Text, txtFinalRastr.Text, idRemetenteRastr, idDestinatarioRastr, tipoRastreamento, txtNF.Text, txtConhecimento.Text, idCC, txtUFOri.Text, idCidadeOri, txtUFDest.Text, idCidadeDest, idFinanceiro,modal);
            }
        }

        private void btnOcorr_Click(object sender, EventArgs e)
        {
            var servico = new ServiceLocal.ConsultasClient(); //Consulta Local
            CarregaUsuario();

            servico.OcorrenciasNovoCompleted += (sender2, args) =>
            {
                txtRetorno.Text = args.Result;
                webBrowser1.DocumentText = args.Result;
            };

            servico.OcorrenciasNovoAsync(txtMovimento.Text);
        }
    }
}
