using Dominio.Mensagem;
using Xunit;

namespace BemPreparadosTestes.Testes
{
    public class MensagemTeste
    {
        
        private readonly IMensagem _mensagem;

        public MensagemTeste()
        {
            _mensagem = new Mensagem();
        }

        [Fact]
        public void AdicionaMensagem_NaoRecebeParam_AdicionaErroComSucesso()
        {
            _mensagem.AdicionarErro("erro de teste");
            Assert.Contains("erro de teste", _mensagem.RetornaErros());
        }

    }
}
