using Aplicacao.Dto;
using Dominio.Mensagem;
using Dominio.RetornoApi;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Projeto_BemPreparados.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        public IMensagem _mensagem;


        public BaseController(IMensagem mensagem)
        {
            _mensagem = mensagem;
        }

        protected IActionResult GerarRetorno<T>(T retornoApi)
        {
            var retorno = new RetornoApi<T>(retornoApi, _mensagem);
            if(retorno.TemErros)
                return BadRequest(retorno.Retorno);
            return Ok(retorno.Retorno);
        }

        private string TokenJwt
        {
            get
            {
                HttpContext.Request.Headers.TryGetValue("Authorization", out var TokenJwt);
                return TokenJwt.ToString().Replace("Bearer", "").Trim();
            }
        }

        protected UsuarioLogado UsuarioLogado
        {
            get
            {
                if (string.IsNullOrWhiteSpace(TokenJwt))
                    return null;

                var token = new JwtSecurityTokenHandler().ReadJwtToken(TokenJwt);
                var usuario = token.Claims.FirstOrDefault(claim => claim.Type == "unique_name")?.Value;

                if (usuario == null)
                    return null;

                return new UsuarioLogado()
                {
                    Usuario = usuario,
                    Token = TokenJwt
                };
            }
        }

    }
}
