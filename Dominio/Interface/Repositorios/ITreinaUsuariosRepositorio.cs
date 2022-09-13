using System.Threading.Tasks;

namespace Dominio.Interface.Repositorios
{
    public interface ITreinaUsuariosRepositorio
    {
        public Task<int> VerificarLogin(string usuario, string senha);

    }
}
