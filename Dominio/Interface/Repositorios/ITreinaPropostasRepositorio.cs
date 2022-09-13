using Dominio.Modelos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dominio.Interface.Repositorios
{
    public interface ITreinaPropostasRepositorio
    {
        public Task<IEnumerable<ConsultaTreinaProposta>> ConsultarProposta(PropostaBuscada propostaBuscada, string usuario);
        public Task<TreinaProposta> CriarProposta(TreinaProposta treinaProposta, TreinaCliente cliente, string usuario);


    }
}
