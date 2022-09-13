using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Dominio.Mensagem;
using Dominio.Modelos;
using Dominio.RetornoApi;
using Microsoft.AspNetCore.Authorization;
using Aplicacao.Interface;

namespace Projeto_BemPreparados.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : BaseController
    {
        private readonly ICliente _clienteService;

        public ClienteController(IMensagem mensagem, ICliente clienteService) 
            : base(mensagem)
        {
            _clienteService = clienteService;
        }

        [HttpGet] 
        [Authorize]
        public async Task<IActionResult> BuscarCliente(string cpf)
        {
            var usuario = await _clienteService.BuscarCliente(cpf);
            return GerarRetorno(new RetornoApi<TreinaCliente>(usuario, _mensagem));
        }


    }
}
