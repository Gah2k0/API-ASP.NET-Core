using Aplicacao;
using Aplicacao.Dto.Usuario;
using Aplicacao.Interface;
using Dominio.Mensagem;
using Dominio.RetornoApi;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Projeto_BemPreparados.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacaoController : BaseController
    {
        private readonly IAutenticacao _autenticacaoService;

        public AutenticacaoController(IMensagem mensagem, IAutenticacao autenticacaoservice) 
            : base(mensagem)
        {
            _autenticacaoService = autenticacaoservice;
        }

        [HttpPost]
        public async Task<IActionResult> Autenticar([FromBody] LoginRequest login) 
        {
            var usuario = await _autenticacaoService.Autenticar(login);
            return GerarRetorno(new RetornoApi<Autenticado>(usuario, _mensagem));
        }


    }
}
