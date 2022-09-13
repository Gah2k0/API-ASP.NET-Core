using Aplicacao.Dto.Cliente;
using Aplicacao.Dto.Proposta;
using Aplicacao.Interface;
using AutoMapper;
using Dominio.Fila;
using Dominio.Interface.Provedor;
using Dominio.Mensagem;
using Dominio.Modelos;
using Dominio.Validacoes.Interface;
using Dominio.Interface.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Aplicacao.Servico
{
    public class TreinaPropostaService : ITreinaProposta
    {
        private readonly IMapper _mapper;
        private readonly IMensagem _mensagem;
        private readonly ITreinaPropostasRepositorio _repositorioProposta;
        private readonly IValidacaoProposta _validacaoProposta;
        private readonly ICliente _serviceClientes;
        private readonly IFilaProvedor _provedor;
        private readonly IConfiguration _configuration;
        public TreinaPropostaService(IMapper mapper, IMensagem mensagem, ITreinaPropostasRepositorio repositorio,
            ICliente serviceClientes, IValidacaoProposta validacaoProposta, IFilaProvedor provedor, IConfiguration configuration)
        {
            _mapper = mapper;
            _mensagem = mensagem;
            _repositorioProposta = repositorio;
            _serviceClientes = serviceClientes;
            _validacaoProposta = validacaoProposta;
            _provedor = provedor;
            _configuration = configuration;
        }

        public async Task<IEnumerable<ConsultaTreinaProposta>> ConsultarProposta(PropostaBuscada propostaBuscada, string usuario)
        {
            var proposta = await _repositorioProposta.ConsultarProposta(propostaBuscada, usuario);
            if (proposta.Count() == 0)
            {
                return null;
            }
            return proposta;
        }

        public async Task<TreinaProposta> CriarProposta(PropostaDto propostaDto, ClienteDto clienteDto, string usuario)
        {
            var treinaProposta = _mapper.Map<TreinaProposta>(propostaDto);
            var potenciaJuros = Math.Pow(_configuration.GetValue<double>("TaxaJuros") + 1, (double)treinaProposta.Prazo);
            treinaProposta.Vlr_Financiado = (decimal)(((double)treinaProposta.Vlr_Solicitado) * potenciaJuros);

            var cliente = await _serviceClientes.CadastrarEAtualizarCliente(clienteDto, usuario);
            if (cliente == null)
                return null;

            _validacaoProposta.ValidarProposta(treinaProposta);
            if (_mensagem.PossuiErros)
                return null;

            var proposta = await _repositorioProposta.CriarProposta(treinaProposta, cliente, usuario);

            var mensagem = new MensagemPropostaFila
            {
                NumeroProposta = proposta.Proposta
            };

            await _provedor.GravarMensagemPropostaFila(mensagem);

            return proposta;
        }

    }
}
