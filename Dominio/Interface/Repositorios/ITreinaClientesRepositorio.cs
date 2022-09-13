using Dominio.Modelos;
using System.Threading.Tasks;

namespace Dominio.Interface.Repositorios
{
    public interface ITreinaClientesRepositorio
    {
        public Task<TreinaCliente> BuscarCliente(string cpf);
        public Task<TreinaCliente> CadastrarCliente(TreinaCliente treinaCliente, string usuario);
        public Task<TreinaCliente> AtualizarCliente(TreinaCliente treinaCliente, string usuario);


    }
}
