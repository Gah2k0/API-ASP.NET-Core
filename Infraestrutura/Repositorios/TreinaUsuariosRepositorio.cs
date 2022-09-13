using System.Data;
using Dapper;
using Infraestrutura.Conexao;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Dominio.Interface.Repositorios;

namespace Infraestrutura.Repositorios
{
    public class TreinaUsuariosRepositorio : ITreinaUsuariosRepositorio
    {
        private readonly IConfiguration _configuration;
        private readonly ConexaoDb _conexaoDb;

        public TreinaUsuariosRepositorio(IConfiguration configuration, ConexaoDb conexaoDb)
        {
            _configuration = configuration;
            _conexaoDb = conexaoDb;
        }

        public async Task<int> VerificarLogin(string usuario, string senha)
        {
            using (IDbConnection con = _conexaoDb.ConexaoDapper)
            {
                con.Open();
                var parametros = new DynamicParameters();
                parametros.Add("@Usuario", usuario, DbType.String, ParameterDirection.Input);
                parametros.Add("@Senha", senha, DbType.String, ParameterDirection.Input);
                string login = "SELECT [dbo].[F_VALIDACAO_LOGIN](@Usuario, @Senha)";
                return await con.QueryFirstAsync<int>(login, parametros);
            }
        }

    }
}
