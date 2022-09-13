using Dapper;
using Dominio.Modelos;
using Infraestrutura.Conexao;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dominio.Interface.Repositorios;

namespace Infraestrutura.Repositorios
{
    public class TreinaConveniadasRepositorio : ITreinaConveniadasRepositorio
    {
        private readonly IConfiguration _configuration;
        private readonly ConexaoDb _conexaoDb;

        public TreinaConveniadasRepositorio(IConfiguration configuration, ConexaoDb conexaoDb)
        {
            _configuration = configuration;
            _conexaoDb = conexaoDb;
        }

        public async Task<IEnumerable<TreinaConveniada>> BuscarConveniada()
        {
            using (IDbConnection con = _conexaoDb.ConexaoDapper)
            {
                con.Open();
                string busca = @"SELECT [ID_TREINA_CONVENIADA] AS IdTreinaConveniada
                                       ,[CONVENIADA]
                                       ,[DESCRICAO]
                                       ,[USUARIO_ATUALIZACAO] AS UsuarioAtualizacao
                                       ,[DATA_ATUALIZACAO] AS DataAtualizacao
                                      FROM [dbo].[TREINA_CONVENIADAS]";
                return await con.QueryAsync<TreinaConveniada>(busca);
            }
        }

    }
}
