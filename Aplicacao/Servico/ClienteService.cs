using Aplicacao.Dto.Cliente;
using Aplicacao.Interface;
using AutoMapper;
using Dominio.Mensagem;
using Dominio.Modelos;
using Dominio.Validacoes.Interface;
using Dominio.Interface.Repositorios;
using System.Threading.Tasks;

namespace Aplicacao.Servico
{
    public class ClienteService : ICliente
    {
        private readonly IMapper _mapper;
        private readonly IMensagem _mensagem;
        private readonly ITreinaClientesRepositorio _repositorio;
        private readonly IValidacaoCliente _validacaoCliente;
        public ClienteService(IMapper mapper, IMensagem mensagem, ITreinaClientesRepositorio repositorio, IValidacaoCliente validacaoCliente)
        {
            _mapper = mapper;
            _mensagem = mensagem;
            _repositorio = repositorio;
            _validacaoCliente = validacaoCliente;
        }

        public async Task<TreinaCliente> BuscarCliente(string cpf)
        {
            var cliente = await _repositorio.BuscarCliente(cpf);
            return cliente;
        }

        public async Task<TreinaCliente> CadastrarEAtualizarCliente(ClienteDto clienteDto, string usuario)
        {
            var treinaCliente = _mapper.Map<TreinaCliente>(clienteDto);
            _validacaoCliente.ValidarCliente(treinaCliente);
            if (_mensagem.PossuiErros)
            {
                return null;
            }

            var cliente = await _repositorio.BuscarCliente(treinaCliente.Cpf);

            if(cliente == null)
            {
                return await _repositorio.CadastrarCliente(treinaCliente, usuario);
            } 

            return await _repositorio.AtualizarCliente(treinaCliente, usuario);
        }

    }
}
