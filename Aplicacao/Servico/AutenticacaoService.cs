using Aplicacao.Dto.Usuario;
using Aplicacao.Interface;
using Dominio.Mensagem;
using Dominio.Interface.Repositorios;
using System.Threading.Tasks;

namespace Aplicacao.Servico
{
    public class AutenticacaoService : IAutenticacao
    {
        private readonly ITokenService _tokenService;
        private readonly IMensagem _mensagem;
        private readonly ITreinaUsuariosRepositorio _repositorio;
        public AutenticacaoService(ITokenService tokenService, IMensagem mensagem, ITreinaUsuariosRepositorio repositorio)
        {
            _tokenService = tokenService;
            _mensagem = mensagem;
            _repositorio = repositorio;
        }


        public async Task<Autenticado> Autenticar(LoginRequest loginRequest)
        {
            switch( await _repositorio.VerificarLogin(loginRequest.Usuario, loginRequest.Senha))
            {
                    case 404:
                {
                    _mensagem.AdicionarErro("Usuário ou senha inválidos");
                    return null;
                }
                    case 403:
                {
                    _mensagem.AdicionarErro("Senha expirada");
                    return null;
                }
            }
            var token = _tokenService.GerarToken(loginRequest);
            Autenticado usuarioComToken = new Autenticado { Nome = loginRequest.Usuario, Token = token };
            return usuarioComToken;
        }

    }

}
