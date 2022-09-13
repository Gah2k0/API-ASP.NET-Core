using Aplicacao.Interface;
using Aplicacao.Servico;
using Dominio.Mensagem;
using Dominio.Modelos;
using Dominio.Interface.Repositorios;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace BemPreparadosTestes.Testes
{
    public class ConveniadaTeste
    {
        private readonly IMensagem _mensagem;
        private readonly Mock<ITreinaConveniadasRepositorio> _repositorioConveniada;
        private readonly IConveniada _conveniadaService;

        public ConveniadaTeste()
        {
            _mensagem = new Mensagem();
            _repositorioConveniada = new Mock<ITreinaConveniadasRepositorio>();
            _conveniadaService = new ConveniadaService(_mensagem, _repositorioConveniada.Object);
        }


        [Fact]
        public async void BuscaConveniada_RetornarListaDeConveniadas()
        {
            var listaConveniada = new List<TreinaConveniada>();
            var conveniada = new TreinaConveniada();
            listaConveniada.Add(conveniada);

            _repositorioConveniada.Setup(m => m.BuscarConveniada()
                ).ReturnsAsync(listaConveniada);

            var resultado = await _conveniadaService.BuscarConveniada();

            Assert.NotEmpty(resultado);
        }



        [Fact]
        public async void BuscaConveniada_RetornarNull()
        {
            var listaConveniadaNula = new List<TreinaConveniada>();

            _repositorioConveniada.Setup(m => m.BuscarConveniada()
                ).ReturnsAsync(listaConveniadaNula);

            var resultadoVazio = await _conveniadaService.BuscarConveniada();

            Assert.Empty(resultadoVazio);
        }


        [Fact]
        public async void BuscaConveniada_AdicionarErro()
        {                                 
            List<TreinaConveniada> listaConveniadaNula = null;

            _repositorioConveniada.Setup(m => m.BuscarConveniada()
                ).ReturnsAsync(listaConveniadaNula);

            var resultadoVazio = await _conveniadaService.BuscarConveniada();

            Assert.True(_mensagem.PossuiErros);
        }

    }
}
