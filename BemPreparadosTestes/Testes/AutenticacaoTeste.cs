using Aplicacao;
using Aplicacao.Dto.Usuario;
using Aplicacao.Interface;
using Aplicacao.Servico;
using Dominio.Mensagem;
using Dominio.Interface.Repositorios;
using Moq;
using Xunit;

namespace BemPreparadosTestes.Testes
{
    public class AutenticacaoTeste 
    {
        private readonly IMensagem _mensagem;
        private readonly Mock<ITokenService> _tokenService;
        private readonly Mock<ITreinaUsuariosRepositorio> _treinaUsuariosRepositorioMock;
        private readonly IAutenticacao _autenticacaoService;

        public AutenticacaoTeste()
        {
            _tokenService = new();
            
            _mensagem = new Mensagem();

            _treinaUsuariosRepositorioMock = new Mock<ITreinaUsuariosRepositorio>(); // autenticacao repositorio

            _autenticacaoService = new AutenticacaoService(_tokenService.Object, _mensagem, 
                _treinaUsuariosRepositorioMock.Object); // autenticacao service

        }


        [Fact]
        public async void TestaAutentica_RecebeStringNomeESenhaErrados_AdicionaErro()
        {

            _treinaUsuariosRepositorioMock.Setup(m => m.VerificarLogin(It.IsAny<string>(), It.IsAny<string>())
               ).ReturnsAsync(404);

            var loginRequestErrada = new LoginRequest();
            loginRequestErrada.Usuario = "Paulet";
            loginRequestErrada.Senha = "12345678!";

            var resultado = await _autenticacaoService.Autenticar(loginRequestErrada);

            Assert.True(_mensagem.PossuiErros);
        }


        [Fact]
        public async void TestaAutentica_RecebeStringNomeESenhaExpirada_AdicionaErro()
        {

            _treinaUsuariosRepositorioMock.Setup(m => m.VerificarLogin(It.IsAny<string>(), It.IsAny<string>())
               ).ReturnsAsync(403);

            var loginRequestExpirado = new LoginRequest();
            loginRequestExpirado.Usuario = "Gabrielf";
            loginRequestExpirado.Senha = "12345678";

            var resultado = await _autenticacaoService.Autenticar(loginRequestExpirado);

            Assert.True(_mensagem.PossuiErros);
        }


        [Fact]
        public async void TestaAutentica_RecebeStringNomeESenhaCertos_RetornaAutenticado()
        {

            _treinaUsuariosRepositorioMock.Setup(m => m.VerificarLogin("Paulete", "12345678")
                ).ReturnsAsync(200);

            var loginRequest = new LoginRequest();
            loginRequest.Usuario = "Paulete";
            loginRequest.Senha = "12345678";
            var autenticado = new Autenticado();
            autenticado.Nome = "Paulete";

            var resultado = await _autenticacaoService.Autenticar(loginRequest);

            Assert.Equal(loginRequest.Usuario, resultado.Nome);
        }



    }
}
