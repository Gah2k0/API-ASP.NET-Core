using Dominio.Modelos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aplicacao.Interface
{
    public interface IConveniada
    {
        public Task<IEnumerable<TreinaConveniada>> BuscarConveniada();

    }
}
