using Dominio.Mensagem;
using Dominio.Modelos;
using Dominio.Validacoes;
using Dominio.Validacoes.Interface;
using Xunit;

namespace BemPreparadosTestes.Testes
{
    public class ValidacoesTreinaPropostaTeste
    {
        private readonly IMensagem _mensagem;
        private readonly IValidacaoProposta _validacaoProposta;

        public ValidacoesTreinaPropostaTeste()
        {
            _mensagem = new Mensagem();
            _validacaoProposta = new ValidacaoProposta(_mensagem);
        }

        [Fact]
        public void TesteValidarProposta()
        {
            var proposta = new TreinaProposta()
            {
                Proposta = 1,
                Vlr_Financiado = 100,
                Vlr_Solicitado = 100
            };

            _validacaoProposta.ValidarProposta(proposta);

            Assert.True(!_mensagem.PossuiErros);
        }

        [Fact]
        public void TesteValidarProposta_ComNumeroNegativo_AdicionaErro()
        {
            var proposta = new TreinaProposta()
            {
                Proposta = -1,
                Vlr_Financiado = 100,
                Vlr_Solicitado = 100
            };

            _validacaoProposta.ValidarProposta(proposta);

            Assert.True(_mensagem.PossuiErros);
        }

        [Theory]
        [InlineData(100, false)]
        [InlineData(1, false)]
        [InlineData(-1, true)]
        public void TesteValidarNumeroProposta(decimal proposta, bool possuiErro)
        {
            _validacaoProposta.ValidarNumeroProposta(proposta);
            Assert.Equal(possuiErro, _mensagem.PossuiErros);

        }



        [Theory]
        [InlineData(100, 100, false)]
        [InlineData(1, 1, false)]
        [InlineData(-1, -1, true)]
        [InlineData(-1, 1, true)]
        [InlineData(1, -1, true)]
        public void TesteValidarValorProposta(decimal valorSolicitado, decimal valorFinanciado, bool possuiErro)
        {
            _validacaoProposta.ValidarValor(valorSolicitado, valorFinanciado);
            Assert.Equal(possuiErro, _mensagem.PossuiErros);

        }

    }
}
