using Aplicacao;
using Aplicacao.Interface;
using Aplicacao.Servico;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Xunit;

namespace BemPreparadosTestes.Testes
{
    public class TokenServiceTeste
    {
        private readonly ITokenService _tokenService;

        public TokenServiceTeste()
        {
            var token = new Dictionary<string, string>
            {
                {"Secret", "aaaaaaa!!!sadqwd" }
            };
            var expiracao = new Dictionary<string, string>
            {
                {"TokenHorasExpiracao", "8" }
            };
            var _configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(token)
                .AddInMemoryCollection(expiracao)
                .Build();


            _tokenService = new TokenService(_configuration);
        }


        [Fact]
        public void GerarToken()
        {
            var login = new LoginRequest()
            {
                Usuario = "Gabrielf",
                Senha = "12345678"
            };

            var resultado = _tokenService.GerarToken(login);

            var token = new JwtSecurityTokenHandler().ReadJwtToken(resultado);
            var usuario = token.Claims.FirstOrDefault(claim => claim.Type == "unique_name")?.Value;

            Assert.Equal(login.Usuario, usuario);
        }
    }
}
