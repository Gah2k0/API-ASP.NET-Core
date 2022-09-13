using Aplicacao.Dto.Cliente;
using Aplicacao.Dto.Proposta;
using Dominio.Modelos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aplicacao.Interface
{
    public interface ITreinaProposta
    {
        public Task<IEnumerable<ConsultaTreinaProposta>> ConsultarProposta(PropostaBuscada propostaBuscada, string usuario);


        public Task<TreinaProposta> CriarProposta(PropostaDto propostaDto, ClienteDto clienteDto, string usuario);
       
    }
}
