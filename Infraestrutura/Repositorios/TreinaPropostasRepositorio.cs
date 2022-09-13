using Dapper;
using Dominio.Modelos;
using Infraestrutura.Conexao;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dominio.Interface.Repositorios;

namespace Infraestrutura.Repositorios
{
    public class TreinaPropostasRepositorio : ITreinaPropostasRepositorio
    {
        private readonly IConfiguration _configuration;
        private readonly ConexaoDb _conexaoDb;

        public TreinaPropostasRepositorio(IConfiguration configuration, ConexaoDb conexaoDb)
        {
            _configuration = configuration;
            _conexaoDb = conexaoDb;
        }

        public async Task<IEnumerable<ConsultaTreinaProposta>> ConsultarProposta(PropostaBuscada propostaBuscada, string usuario)
        {
            using (IDbConnection con = _conexaoDb.ConexaoDapper)
            {
                con.Open();
                DynamicParameters parametros = new DynamicParameters();
                parametros.Add("@Proposta", propostaBuscada.NumeroProposta, DbType.Decimal);
                parametros.Add("@Cpf", propostaBuscada.Cpf, DbType.StringFixedLength, size: 11);
                parametros.Add("@Nome", propostaBuscada.Nome, DbType.String);
                parametros.Add("@Usuario", usuario, DbType.String, size: 10);
                string Selectproposta = @"SELECT 
                                PROPOSTA 
                                ,TC.CPF
                                ,TC.NOME
                                ,VLR_FINANCIADO AS VlrFinanciado
                                ,PRAZO
                                ,SITUACAO
                                FROM [dbo].[TREINA_PROPOSTAS] TP 
                                INNER JOIN TREINA_CLIENTES TC ON
                                TP.CPF = TC.CPF
                                WHERE (PROPOSTA = @Proposta OR @Proposta IS NULL OR @Proposta = 0) AND
                                        (TC.CPF = @Cpf OR @Cpf IS NULL) AND
                                        (TC.NOME LIKE @Nome + '%' OR @Nome IS NULL) AND
                                        (@Usuario = TP.USUARIO)";
                var proposta = await con.QueryAsync<ConsultaTreinaProposta>(Selectproposta, parametros);
                return proposta;
            }
        }

        public async Task<TreinaProposta> CriarProposta(TreinaProposta treinaProposta, TreinaCliente cliente, string usuario)
        {
            using (IDbConnection con = _conexaoDb.ConexaoDapper)
            {
                con.Open();
                DynamicParameters parametros = new DynamicParameters();
                var numeroProposta = await con.QuerySingleAsync<decimal>(
                                        "SELECT MAX(proposta)+1 FROM TREINA_PROPOSTAS");
                parametros.Add("@Proposta", numeroProposta, DbType.Decimal);
                parametros.Add("@Cpf", cliente.Cpf, DbType.StringFixedLength, size: 11);
                parametros.Add("@Conveniada", treinaProposta.Conveniada, DbType.String);
                parametros.Add("@VlrSolicitado", treinaProposta.Vlr_Solicitado, DbType.Decimal);
                parametros.Add("@Prazo", treinaProposta.Prazo, DbType.Int16);
                parametros.Add("@VlrFinanciado", treinaProposta.Vlr_Financiado, DbType.Decimal);
                parametros.Add("@Situacao", "AG", DbType.String);
                parametros.Add("@DtSituacao", DateTime.Now, DbType.Date);
                parametros.Add("@Usuario", usuario, DbType.String, size: 10);
                parametros.Add("@UsuarioAtualizacao", usuario, DbType.String, size: 10);
                parametros.Add("@DataAtualizacao", DateTime.Now, DbType.Date);
                var propostaInserida = new TreinaProposta()
                {
                    Proposta = numeroProposta,
                    Cpf = cliente.Cpf,
                    Conveniada = treinaProposta.Conveniada,
                    Vlr_Solicitado = treinaProposta.Vlr_Solicitado,
                    Vlr_Financiado = treinaProposta.Vlr_Financiado,
                    Situacao = "AG",
                    Dt_Situacao = DateTime.Now,
                    Usuario = usuario,
                    Usuario_Atualizacao = usuario,
                    Data_Atualizacao = DateTime.Now
                };
                string insercao = @"INSERT INTO [dbo].[TREINA_PROPOSTAS]
                                       ([PROPOSTA]
                                       ,[CPF]
                                       ,[CONVENIADA]
                                       ,[VLR_SOLICITADO] 
                                       ,[PRAZO]
                                       ,[VLR_FINANCIADO]
                                       ,[SITUACAO]
                                       ,[DT_SITUACAO]
                                       ,[USUARIO]
                                       ,[USUARIO_ATUALIZACAO]
                                       ,[DATA_ATUALIZACAO])
                                 VALUES
                                       (@Proposta
                                       ,@Cpf
                                       ,@Conveniada
                                       ,@VlrSolicitado
                                       ,@Prazo
                                       ,@VlrFinanciado
                                       ,@Situacao
                                       ,@DtSituacao
                                       ,@Usuario
                                       ,@UsuarioAtualizacao
                                       ,@DataAtualizacao)";
                await con.ExecuteAsync(insercao, parametros);
                return propostaInserida;
            }
        }
    }
}
