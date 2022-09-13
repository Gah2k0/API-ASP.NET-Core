using Dapper;
using Dominio.Modelos;
using Infraestrutura.Conexao;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Threading.Tasks;
using Dominio.Interface.Repositorios;

namespace Infraestrutura.Repositorios
{
    public class TreinaClientesRepositorio : ITreinaClientesRepositorio
    {
        private readonly IConfiguration _configuration;
        private readonly ConexaoDb _conexaoDb;

        public TreinaClientesRepositorio(IConfiguration configuration, ConexaoDb conexaoDb)
        {
            _configuration = configuration;
            _conexaoDb = conexaoDb;
        }

        public async Task<TreinaCliente> BuscarCliente(string cpf)
        {
            using (IDbConnection con = _conexaoDb.ConexaoDapper)
            {
                con.Open();
                var parametros = new DynamicParameters(); 
                parametros.Add("@Cpf", cpf, DbType.String);
                string selectCliente = @"SELECT 
                                  [ID_TREINA_CLIENTE] AS IdTreinaCliente
                                  ,[CPF]
                                  ,[NOME]
                                  ,[DT_NASCIMENTO] AS DtNascimento
                                  ,[GENERO]
                                  ,[VLR_SALARIO] AS VlrSalario
                                  ,[LOGRADOURO]
                                  ,[NUMERO_RESIDENCIA] AS NumeroResidencia
                                  ,[BAIRRO]
                                  ,[CIDADE]
                                  ,[CEP]
                                  ,[USUARIO_ATUALIZACAO] AS UsuarioAtualizacao
                                  ,[DATA_ATUALIZACAO] AS DataAtualizacao
                                  FROM [DB_Projeto_02].[dbo].[TREINA_CLIENTES] 
                                  WHERE CPF = @Cpf";
                var cliente = await con.QueryFirstOrDefaultAsync<TreinaCliente>(selectCliente, parametros);
                return cliente;
            }
        }

        public async Task<TreinaCliente> CadastrarCliente(TreinaCliente treinaCliente, string usuario)
        {
            using(IDbConnection con = _conexaoDb.ConexaoDapper)
            {
                con.Open();
                var parametros = new DynamicParameters();
                parametros.Add("@Cpf", treinaCliente.Cpf, DbType.StringFixedLength, size: 11);
                parametros.Add("@Nome", treinaCliente.Nome, DbType.String);
                parametros.Add("@DtNascimento", treinaCliente.DtNascimento, DbType.Date);
                parametros.Add("@Genero", treinaCliente.Genero, DbType.StringFixedLength, size: 1);
                parametros.Add("@VlrSalario", treinaCliente.VlrSalario, DbType.Decimal);
                parametros.Add("@Logradouro", treinaCliente.Logradouro, DbType.String);
                parametros.Add("@NumeroResidencia", treinaCliente.NumeroResidencia, DbType.String, size: 10);
                parametros.Add("@Bairro", treinaCliente.Bairro, DbType.String);
                parametros.Add("@Cidade", treinaCliente.Cidade, DbType.String);
                parametros.Add("@Cep", treinaCliente.Cep, DbType.StringFixedLength, size: 8);
                parametros.Add("@UsuarioAtualizacao", usuario, DbType.String, size: 10);
                parametros.Add("@DataAtualizacao", DateTime.Now, DbType.Date);
                var cadastro = @"INSERT INTO [dbo].[TREINA_CLIENTES]
                                       ([CPF]
                                       ,[NOME]
                                       ,[DT_NASCIMENTO]
                                       ,[GENERO]
                                       ,[VLR_SALARIO]
                                       ,[LOGRADOURO]
                                       ,[NUMERO_RESIDENCIA]
                                       ,[BAIRRO]
                                       ,[CIDADE]
                                       ,[CEP]
                                       ,[USUARIO_ATUALIZACAO]
                                       ,[DATA_ATUALIZACAO])
                                 VALUES
                                       (@Cpf
                                       ,@Nome
                                       ,@DtNascimento
                                       ,@Genero
                                       ,@VlrSalario
                                       ,@Logradouro
                                       ,@NumeroResidencia
                                       ,@Bairro
                                       ,@Cidade
                                       ,@Cep
                                       ,@UsuarioAtualizacao
                                       ,@DataAtualizacao)";
                await con.ExecuteAsync(cadastro, parametros);
                return treinaCliente;
            }
        }

        public async Task<TreinaCliente> AtualizarCliente(TreinaCliente treinaCliente, string usuario)
        {
            using (IDbConnection con = _conexaoDb.ConexaoDapper)
            {
                con.Open();
                var parametros = new DynamicParameters();
                parametros.Add("@Cpf", treinaCliente.Cpf);
                parametros.Add("@VlrSalario", treinaCliente.VlrSalario, DbType.Decimal);
                parametros.Add("@Logradouro", treinaCliente.Logradouro, DbType.String);
                parametros.Add("@NumeroResidencia", treinaCliente.NumeroResidencia, DbType.String, size: 10);
                parametros.Add("@Bairro", treinaCliente.Bairro, DbType.String);
                parametros.Add("@Cidade", treinaCliente.Cidade, DbType.String);
                parametros.Add("@Cep", treinaCliente.Cep, DbType.StringFixedLength, size: 8);
                parametros.Add("@UsuarioAtualizacao", usuario, DbType.String, size: 10);
                var atualizacaoCadastro = @"UPDATE [dbo].[TREINA_CLIENTES]
                                           SET 
                                              [VLR_SALARIO] = @VlrSalario
                                              ,[LOGRADOURO] = @Logradouro
                                              ,[NUMERO_RESIDENCIA] = @NumeroResidencia
                                              ,[BAIRRO] = @Bairro
                                              ,[CIDADE] = @Cidade
                                              ,[CEP] = @Cep
                                              ,[USUARIO_ATUALIZACAO] = @UsuarioAtualizacao
                                            WHERE CPF = @Cpf";
                await con.ExecuteAsync(atualizacaoCadastro, parametros);
                return treinaCliente;
            }
        }
    }
}
