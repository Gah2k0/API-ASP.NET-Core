using Aplicacao.Dto.Cliente;
using Dominio.Modelos;
using System.Threading.Tasks;

namespace Aplicacao.Interface
{
    public interface ICliente
    {
        public Task<TreinaCliente> BuscarCliente(string cpf);
        public Task<TreinaCliente> CadastrarEAtualizarCliente(ClienteDto clienteDto, string usuario);


    }
}
