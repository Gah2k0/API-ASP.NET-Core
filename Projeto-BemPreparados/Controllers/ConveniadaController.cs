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
    public class ConveniadaController : BaseController
    {
        private readonly IConveniada _conveniadaService;

        public ConveniadaController(IMensagem mensagem, IConveniada conveniadaService)
            : base (mensagem)
        {
            _conveniadaService = conveniadaService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> BuscarConveniada()
        {
            var conveniada = await _conveniadaService.BuscarConveniada();
            return GerarRetorno(new RetornoApi<IEnumerable<TreinaConveniada>>(conveniada, _mensagem));
        }
    }
}
