using Dominio.Mensagem;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dominio.Modelos;
using Dominio.Interface.Repositorios;
using Aplicacao.Interface;

namespace Aplicacao.Servico
{
    public class ConveniadaService : IConveniada
    {
        private readonly IMensagem _mensagem;
        private readonly ITreinaConveniadasRepositorio _repositorio;
        public ConveniadaService(IMensagem mensagem, ITreinaConveniadasRepositorio repositorio)
        {
            _mensagem = mensagem;
            _repositorio = repositorio;
        }

        public async Task<IEnumerable<TreinaConveniada>> BuscarConveniada()
        {
            var conveniada = await _repositorio.BuscarConveniada();
            if (conveniada == null)
            {
                _mensagem.AdicionarErro("Conveniada não encontrada");
            }
            return conveniada;
        }
    }
}
