using Dominio.Mensagem;
using Dominio.Validacoes;
using Dominio.Validacoes.Interface;
using Xunit;

namespace BemPreparadosTestes.Testes
{
    public class ValidacoesClienteTeste
    {
        private readonly IMensagem _mensagem;
        private readonly IValidacaoCliente _validacaoCliente;
        

        public ValidacoesClienteTeste()
        {
            _mensagem = new Mensagem();
            _validacaoCliente = new ValidacaoCliente(_mensagem);
        }


        [Theory]
        [InlineData("Gabriel Francisco", false)]
        [InlineData("", true)]
        [InlineData("G4br13l", true)]
        [InlineData("123456789", true)]
        [InlineData("Gabr!el", true)]
        public void ValidaNome_RecebeStringNome(string nome, bool possuiErro)
        {
            _validacaoCliente.ValidarNome(nome);
            Assert.Equal(possuiErro, _mensagem.PossuiErros);
        }



        [Theory]
        [InlineData("12345678912", false)]
        [InlineData("", true)]
        [InlineData("1234567891", true)]
        [InlineData("123456789as", true)]
        [InlineData("123456789!", true)]
        public void ValidaCpf_RecebeStringCpf(string cpf, bool possuiErro)
        {
            // Arrange

            // Act
            _validacaoCliente.ValidarCpf(cpf);

            // Assert
            Assert.Equal(possuiErro, _mensagem.PossuiErros);
        }



        [Theory]
        [InlineData("10000200", false)]
        [InlineData("", true)]
        [InlineData("1000020", true)]
        [InlineData("1000020a", true)]
        [InlineData("1000020!", true)]
        public void ValidarCep_RecebeStringCep(string cep, bool possuiErro)
        {
            _validacaoCliente.ValidarCep(cep);
            Assert.Equal(possuiErro, _mensagem.PossuiErros);

        }



        [Theory]
        [InlineData("M", false)]
        [InlineData("F", false)]
        [InlineData("1", true)]
        [InlineData("a", true)]
        [InlineData("!", true)]
        [InlineData("", true)]
        public void ValidarGenero_RecebeStringGenero(string genero, bool possuiErro)
        {
            _validacaoCliente.ValidarGenero(genero);
            Assert.Equal(possuiErro, _mensagem.PossuiErros);

        }



        [Theory]
        [InlineData(100, false)]
        [InlineData(0, false)]
        [InlineData(-1, true)]
        public void ValidarSalario_RecebeDecimalSalario(decimal salario, bool possuiErro)
        {
            _validacaoCliente.ValidarSalario(salario);
            Assert.Equal(possuiErro, _mensagem.PossuiErros);

        }
    }

}
