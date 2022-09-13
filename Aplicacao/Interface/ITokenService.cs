
namespace Aplicacao.Interface
{
    public interface ITokenService 
    {
        public string GerarToken(LoginRequest login);
    }
}
