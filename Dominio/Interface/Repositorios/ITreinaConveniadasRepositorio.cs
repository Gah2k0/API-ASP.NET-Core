using Dominio.Modelos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dominio.Interface.Repositorios
{
    public interface ITreinaConveniadasRepositorio
    {
        public Task<IEnumerable<TreinaConveniada>> BuscarConveniada();

    }
}
