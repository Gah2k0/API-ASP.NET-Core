using Aplicacao.Dto.Proposta;
using Aplicacao.Interface;
using Dominio.Mensagem;
using Dominio.Modelos;
using Dominio.RetornoApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Projeto_BemPreparados.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TreinaPropostaController : BaseController
    {
        private readonly ITreinaProposta _treinaPropostaService;

        public TreinaPropostaController(IMensagem mensagem, ITreinaProposta treinaPropostaService)
            : base(mensagem)
        {
            _treinaPropostaService = treinaPropostaService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> BuscarProposta([FromQuery]PropostaBuscada propostaBuscada)
        {
            var proposta = await _treinaPropostaService.ConsultarProposta(propostaBuscada, UsuarioLogado?.Usuario);
            return GerarRetorno(new RetornoApi<IEnumerable<ConsultaTreinaProposta>>(proposta, _mensagem));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CadastrarProposta([FromBody]PropostaDto propostaDto)
        {
            var resultado = await _treinaPropostaService.CriarProposta(propostaDto, propostaDto.Cliente, UsuarioLogado?.Usuario);
            return GerarRetorno(new RetornoApi<TreinaProposta>(resultado, _mensagem));
        }

    }
}
