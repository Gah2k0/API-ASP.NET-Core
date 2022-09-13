using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;


namespace Infraestrutura.Conexao
{
    public class ConexaoDb 
    {
        private readonly IConfiguration _configuracao;
        private readonly string _stringConexao;
        
        public ConexaoDb(IConfiguration configuration)
        {
            _configuracao = configuration;
            _stringConexao = _configuracao.GetConnectionString("UsuarioConnection");
        }

        public IDbConnection ConexaoDapper => new SqlConnection(_stringConexao);

    }
}
