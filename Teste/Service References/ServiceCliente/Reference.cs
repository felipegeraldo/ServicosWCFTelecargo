﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Teste.ServiceCliente {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceCliente.IConsultas")]
    public interface IConsultas {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IConsultas/Ocorrencias", ReplyAction="http://tempuri.org/IConsultas/OcorrenciasResponse")]
        string Ocorrencias(string cnpjCliente, string codigoDestinatario, string numeroNota, string dataInicial, string dataFinal, string senha);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://tempuri.org/IConsultas/Ocorrencias", ReplyAction="http://tempuri.org/IConsultas/OcorrenciasResponse")]
        System.IAsyncResult BeginOcorrencias(string cnpjCliente, string codigoDestinatario, string numeroNota, string dataInicial, string dataFinal, string senha, System.AsyncCallback callback, object asyncState);
        
        string EndOcorrencias(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IConsultas/OcorrenciasNovo", ReplyAction="http://tempuri.org/IConsultas/OcorrenciasNovoResponse")]
        string OcorrenciasNovo(string cnpjCliente, string codigoDestinatario, string numeroNota, string dataInicial, string dataFinal, string senha, string numeroReferencia, string numeroPedido);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://tempuri.org/IConsultas/OcorrenciasNovo", ReplyAction="http://tempuri.org/IConsultas/OcorrenciasNovoResponse")]
        System.IAsyncResult BeginOcorrenciasNovo(string cnpjCliente, string codigoDestinatario, string numeroNota, string dataInicial, string dataFinal, string senha, string numeroReferencia, string numeroPedido, System.AsyncCallback callback, object asyncState);
        
        string EndOcorrenciasNovo(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IConsultas/ConsultarEstoque", ReplyAction="http://tempuri.org/IConsultas/ConsultarEstoqueResponse")]
        string ConsultarEstoque(string cnpjCliente, string codigoProduto, string descricaoProduto, string senha);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://tempuri.org/IConsultas/ConsultarEstoque", ReplyAction="http://tempuri.org/IConsultas/ConsultarEstoqueResponse")]
        System.IAsyncResult BeginConsultarEstoque(string cnpjCliente, string codigoProduto, string descricaoProduto, string senha, System.AsyncCallback callback, object asyncState);
        
        string EndConsultarEstoque(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IConsultas/ConsultarCTe", ReplyAction="http://tempuri.org/IConsultas/ConsultarCTeResponse")]
        string ConsultarCTe(string cnpjCliente, string codigoDestinatario, string numeroNota, string dataInicial, string dataFinal, string senha, string numeroReferencia, string numeroPedido, string numeroCTe);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://tempuri.org/IConsultas/ConsultarCTe", ReplyAction="http://tempuri.org/IConsultas/ConsultarCTeResponse")]
        System.IAsyncResult BeginConsultarCTe(string cnpjCliente, string codigoDestinatario, string numeroNota, string dataInicial, string dataFinal, string senha, string numeroReferencia, string numeroPedido, string numeroCTe, System.AsyncCallback callback, object asyncState);
        
        string EndConsultarCTe(System.IAsyncResult result);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IConsultasChannel : Teste.ServiceCliente.IConsultas, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class OcorrenciasCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public OcorrenciasCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public string Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class OcorrenciasNovoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public OcorrenciasNovoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public string Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ConsultarEstoqueCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public ConsultarEstoqueCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public string Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ConsultarCTeCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public ConsultarCTeCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public string Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ConsultasClient : System.ServiceModel.ClientBase<Teste.ServiceCliente.IConsultas>, Teste.ServiceCliente.IConsultas {
        
        private BeginOperationDelegate onBeginOcorrenciasDelegate;
        
        private EndOperationDelegate onEndOcorrenciasDelegate;
        
        private System.Threading.SendOrPostCallback onOcorrenciasCompletedDelegate;
        
        private BeginOperationDelegate onBeginOcorrenciasNovoDelegate;
        
        private EndOperationDelegate onEndOcorrenciasNovoDelegate;
        
        private System.Threading.SendOrPostCallback onOcorrenciasNovoCompletedDelegate;
        
        private BeginOperationDelegate onBeginConsultarEstoqueDelegate;
        
        private EndOperationDelegate onEndConsultarEstoqueDelegate;
        
        private System.Threading.SendOrPostCallback onConsultarEstoqueCompletedDelegate;
        
        private BeginOperationDelegate onBeginConsultarCTeDelegate;
        
        private EndOperationDelegate onEndConsultarCTeDelegate;
        
        private System.Threading.SendOrPostCallback onConsultarCTeCompletedDelegate;
        
        public ConsultasClient() {
        }
        
        public ConsultasClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ConsultasClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ConsultasClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ConsultasClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public event System.EventHandler<OcorrenciasCompletedEventArgs> OcorrenciasCompleted;
        
        public event System.EventHandler<OcorrenciasNovoCompletedEventArgs> OcorrenciasNovoCompleted;
        
        public event System.EventHandler<ConsultarEstoqueCompletedEventArgs> ConsultarEstoqueCompleted;
        
        public event System.EventHandler<ConsultarCTeCompletedEventArgs> ConsultarCTeCompleted;
        
        public string Ocorrencias(string cnpjCliente, string codigoDestinatario, string numeroNota, string dataInicial, string dataFinal, string senha) {
            return base.Channel.Ocorrencias(cnpjCliente, codigoDestinatario, numeroNota, dataInicial, dataFinal, senha);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult BeginOcorrencias(string cnpjCliente, string codigoDestinatario, string numeroNota, string dataInicial, string dataFinal, string senha, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginOcorrencias(cnpjCliente, codigoDestinatario, numeroNota, dataInicial, dataFinal, senha, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public string EndOcorrencias(System.IAsyncResult result) {
            return base.Channel.EndOcorrencias(result);
        }
        
        private System.IAsyncResult OnBeginOcorrencias(object[] inValues, System.AsyncCallback callback, object asyncState) {
            string cnpjCliente = ((string)(inValues[0]));
            string codigoDestinatario = ((string)(inValues[1]));
            string numeroNota = ((string)(inValues[2]));
            string dataInicial = ((string)(inValues[3]));
            string dataFinal = ((string)(inValues[4]));
            string senha = ((string)(inValues[5]));
            return this.BeginOcorrencias(cnpjCliente, codigoDestinatario, numeroNota, dataInicial, dataFinal, senha, callback, asyncState);
        }
        
        private object[] OnEndOcorrencias(System.IAsyncResult result) {
            string retVal = this.EndOcorrencias(result);
            return new object[] {
                    retVal};
        }
        
        private void OnOcorrenciasCompleted(object state) {
            if ((this.OcorrenciasCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.OcorrenciasCompleted(this, new OcorrenciasCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void OcorrenciasAsync(string cnpjCliente, string codigoDestinatario, string numeroNota, string dataInicial, string dataFinal, string senha) {
            this.OcorrenciasAsync(cnpjCliente, codigoDestinatario, numeroNota, dataInicial, dataFinal, senha, null);
        }
        
        public void OcorrenciasAsync(string cnpjCliente, string codigoDestinatario, string numeroNota, string dataInicial, string dataFinal, string senha, object userState) {
            if ((this.onBeginOcorrenciasDelegate == null)) {
                this.onBeginOcorrenciasDelegate = new BeginOperationDelegate(this.OnBeginOcorrencias);
            }
            if ((this.onEndOcorrenciasDelegate == null)) {
                this.onEndOcorrenciasDelegate = new EndOperationDelegate(this.OnEndOcorrencias);
            }
            if ((this.onOcorrenciasCompletedDelegate == null)) {
                this.onOcorrenciasCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnOcorrenciasCompleted);
            }
            base.InvokeAsync(this.onBeginOcorrenciasDelegate, new object[] {
                        cnpjCliente,
                        codigoDestinatario,
                        numeroNota,
                        dataInicial,
                        dataFinal,
                        senha}, this.onEndOcorrenciasDelegate, this.onOcorrenciasCompletedDelegate, userState);
        }
        
        public string OcorrenciasNovo(string cnpjCliente, string codigoDestinatario, string numeroNota, string dataInicial, string dataFinal, string senha, string numeroReferencia, string numeroPedido) {
            return base.Channel.OcorrenciasNovo(cnpjCliente, codigoDestinatario, numeroNota, dataInicial, dataFinal, senha, numeroReferencia, numeroPedido);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult BeginOcorrenciasNovo(string cnpjCliente, string codigoDestinatario, string numeroNota, string dataInicial, string dataFinal, string senha, string numeroReferencia, string numeroPedido, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginOcorrenciasNovo(cnpjCliente, codigoDestinatario, numeroNota, dataInicial, dataFinal, senha, numeroReferencia, numeroPedido, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public string EndOcorrenciasNovo(System.IAsyncResult result) {
            return base.Channel.EndOcorrenciasNovo(result);
        }
        
        private System.IAsyncResult OnBeginOcorrenciasNovo(object[] inValues, System.AsyncCallback callback, object asyncState) {
            string cnpjCliente = ((string)(inValues[0]));
            string codigoDestinatario = ((string)(inValues[1]));
            string numeroNota = ((string)(inValues[2]));
            string dataInicial = ((string)(inValues[3]));
            string dataFinal = ((string)(inValues[4]));
            string senha = ((string)(inValues[5]));
            string numeroReferencia = ((string)(inValues[6]));
            string numeroPedido = ((string)(inValues[7]));
            return this.BeginOcorrenciasNovo(cnpjCliente, codigoDestinatario, numeroNota, dataInicial, dataFinal, senha, numeroReferencia, numeroPedido, callback, asyncState);
        }
        
        private object[] OnEndOcorrenciasNovo(System.IAsyncResult result) {
            string retVal = this.EndOcorrenciasNovo(result);
            return new object[] {
                    retVal};
        }
        
        private void OnOcorrenciasNovoCompleted(object state) {
            if ((this.OcorrenciasNovoCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.OcorrenciasNovoCompleted(this, new OcorrenciasNovoCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void OcorrenciasNovoAsync(string cnpjCliente, string codigoDestinatario, string numeroNota, string dataInicial, string dataFinal, string senha, string numeroReferencia, string numeroPedido) {
            this.OcorrenciasNovoAsync(cnpjCliente, codigoDestinatario, numeroNota, dataInicial, dataFinal, senha, numeroReferencia, numeroPedido, null);
        }
        
        public void OcorrenciasNovoAsync(string cnpjCliente, string codigoDestinatario, string numeroNota, string dataInicial, string dataFinal, string senha, string numeroReferencia, string numeroPedido, object userState) {
            if ((this.onBeginOcorrenciasNovoDelegate == null)) {
                this.onBeginOcorrenciasNovoDelegate = new BeginOperationDelegate(this.OnBeginOcorrenciasNovo);
            }
            if ((this.onEndOcorrenciasNovoDelegate == null)) {
                this.onEndOcorrenciasNovoDelegate = new EndOperationDelegate(this.OnEndOcorrenciasNovo);
            }
            if ((this.onOcorrenciasNovoCompletedDelegate == null)) {
                this.onOcorrenciasNovoCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnOcorrenciasNovoCompleted);
            }
            base.InvokeAsync(this.onBeginOcorrenciasNovoDelegate, new object[] {
                        cnpjCliente,
                        codigoDestinatario,
                        numeroNota,
                        dataInicial,
                        dataFinal,
                        senha,
                        numeroReferencia,
                        numeroPedido}, this.onEndOcorrenciasNovoDelegate, this.onOcorrenciasNovoCompletedDelegate, userState);
        }
        
        public string ConsultarEstoque(string cnpjCliente, string codigoProduto, string descricaoProduto, string senha) {
            return base.Channel.ConsultarEstoque(cnpjCliente, codigoProduto, descricaoProduto, senha);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult BeginConsultarEstoque(string cnpjCliente, string codigoProduto, string descricaoProduto, string senha, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginConsultarEstoque(cnpjCliente, codigoProduto, descricaoProduto, senha, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public string EndConsultarEstoque(System.IAsyncResult result) {
            return base.Channel.EndConsultarEstoque(result);
        }
        
        private System.IAsyncResult OnBeginConsultarEstoque(object[] inValues, System.AsyncCallback callback, object asyncState) {
            string cnpjCliente = ((string)(inValues[0]));
            string codigoProduto = ((string)(inValues[1]));
            string descricaoProduto = ((string)(inValues[2]));
            string senha = ((string)(inValues[3]));
            return this.BeginConsultarEstoque(cnpjCliente, codigoProduto, descricaoProduto, senha, callback, asyncState);
        }
        
        private object[] OnEndConsultarEstoque(System.IAsyncResult result) {
            string retVal = this.EndConsultarEstoque(result);
            return new object[] {
                    retVal};
        }
        
        private void OnConsultarEstoqueCompleted(object state) {
            if ((this.ConsultarEstoqueCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.ConsultarEstoqueCompleted(this, new ConsultarEstoqueCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void ConsultarEstoqueAsync(string cnpjCliente, string codigoProduto, string descricaoProduto, string senha) {
            this.ConsultarEstoqueAsync(cnpjCliente, codigoProduto, descricaoProduto, senha, null);
        }
        
        public void ConsultarEstoqueAsync(string cnpjCliente, string codigoProduto, string descricaoProduto, string senha, object userState) {
            if ((this.onBeginConsultarEstoqueDelegate == null)) {
                this.onBeginConsultarEstoqueDelegate = new BeginOperationDelegate(this.OnBeginConsultarEstoque);
            }
            if ((this.onEndConsultarEstoqueDelegate == null)) {
                this.onEndConsultarEstoqueDelegate = new EndOperationDelegate(this.OnEndConsultarEstoque);
            }
            if ((this.onConsultarEstoqueCompletedDelegate == null)) {
                this.onConsultarEstoqueCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnConsultarEstoqueCompleted);
            }
            base.InvokeAsync(this.onBeginConsultarEstoqueDelegate, new object[] {
                        cnpjCliente,
                        codigoProduto,
                        descricaoProduto,
                        senha}, this.onEndConsultarEstoqueDelegate, this.onConsultarEstoqueCompletedDelegate, userState);
        }
        
        public string ConsultarCTe(string cnpjCliente, string codigoDestinatario, string numeroNota, string dataInicial, string dataFinal, string senha, string numeroReferencia, string numeroPedido, string numeroCTe) {
            return base.Channel.ConsultarCTe(cnpjCliente, codigoDestinatario, numeroNota, dataInicial, dataFinal, senha, numeroReferencia, numeroPedido, numeroCTe);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult BeginConsultarCTe(string cnpjCliente, string codigoDestinatario, string numeroNota, string dataInicial, string dataFinal, string senha, string numeroReferencia, string numeroPedido, string numeroCTe, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginConsultarCTe(cnpjCliente, codigoDestinatario, numeroNota, dataInicial, dataFinal, senha, numeroReferencia, numeroPedido, numeroCTe, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public string EndConsultarCTe(System.IAsyncResult result) {
            return base.Channel.EndConsultarCTe(result);
        }
        
        private System.IAsyncResult OnBeginConsultarCTe(object[] inValues, System.AsyncCallback callback, object asyncState) {
            string cnpjCliente = ((string)(inValues[0]));
            string codigoDestinatario = ((string)(inValues[1]));
            string numeroNota = ((string)(inValues[2]));
            string dataInicial = ((string)(inValues[3]));
            string dataFinal = ((string)(inValues[4]));
            string senha = ((string)(inValues[5]));
            string numeroReferencia = ((string)(inValues[6]));
            string numeroPedido = ((string)(inValues[7]));
            string numeroCTe = ((string)(inValues[8]));
            return this.BeginConsultarCTe(cnpjCliente, codigoDestinatario, numeroNota, dataInicial, dataFinal, senha, numeroReferencia, numeroPedido, numeroCTe, callback, asyncState);
        }
        
        private object[] OnEndConsultarCTe(System.IAsyncResult result) {
            string retVal = this.EndConsultarCTe(result);
            return new object[] {
                    retVal};
        }
        
        private void OnConsultarCTeCompleted(object state) {
            if ((this.ConsultarCTeCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.ConsultarCTeCompleted(this, new ConsultarCTeCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void ConsultarCTeAsync(string cnpjCliente, string codigoDestinatario, string numeroNota, string dataInicial, string dataFinal, string senha, string numeroReferencia, string numeroPedido, string numeroCTe) {
            this.ConsultarCTeAsync(cnpjCliente, codigoDestinatario, numeroNota, dataInicial, dataFinal, senha, numeroReferencia, numeroPedido, numeroCTe, null);
        }
        
        public void ConsultarCTeAsync(string cnpjCliente, string codigoDestinatario, string numeroNota, string dataInicial, string dataFinal, string senha, string numeroReferencia, string numeroPedido, string numeroCTe, object userState) {
            if ((this.onBeginConsultarCTeDelegate == null)) {
                this.onBeginConsultarCTeDelegate = new BeginOperationDelegate(this.OnBeginConsultarCTe);
            }
            if ((this.onEndConsultarCTeDelegate == null)) {
                this.onEndConsultarCTeDelegate = new EndOperationDelegate(this.OnEndConsultarCTe);
            }
            if ((this.onConsultarCTeCompletedDelegate == null)) {
                this.onConsultarCTeCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnConsultarCTeCompleted);
            }
            base.InvokeAsync(this.onBeginConsultarCTeDelegate, new object[] {
                        cnpjCliente,
                        codigoDestinatario,
                        numeroNota,
                        dataInicial,
                        dataFinal,
                        senha,
                        numeroReferencia,
                        numeroPedido,
                        numeroCTe}, this.onEndConsultarCTeDelegate, this.onConsultarCTeCompletedDelegate, userState);
        }
    }
}