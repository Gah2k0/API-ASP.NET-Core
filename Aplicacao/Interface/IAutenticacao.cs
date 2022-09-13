using Aplicacao.Dto.Usuario;
using System.Threading.Tasks;

namespace Aplicacao.Interface
{
    public interface IAutenticacao
    {
        public Task<Autenticado> Autenticar(LoginRequest loginRequest);

    }
}
