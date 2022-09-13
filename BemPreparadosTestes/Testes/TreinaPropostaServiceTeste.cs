using Aplicacao.Dto.Cliente;
using Aplicacao.Dto.Proposta;
using Aplicacao.Interface;
using Aplicacao.Profiles;
using Aplicacao.Servico;
using AutoMapper;
using Dominio.Interface.Provedor;
using Dominio.Mensagem;
using Dominio.Modelos;
using Dominio.Validacoes;
using Dominio.Validacoes.Interface;
using Dominio.Interface.Repositorios;
using Moq;
using System.Collections.Generic;
using Xunit;
using Microsoft.Extensions.Configuration;

namespace BemPreparadosTestes.Testes
{
    public class TreinaPropostaServiceTeste
    {
        private readonly IMapper _mapper;
        private readonly IMensagem _mensagem;
        private readonly IValidacaoProposta _validacaoProposta;
        private readonly ITreinaProposta _treinaPropostaService;
        private readonly Mock<IFilaProvedor> _provedorMock;
        private readonly Mock<ICliente> _clienteServiceMock;
        private readonly Mock<ITreinaPropostasRepositorio> _treinaPropostasRepositorioMock;
        private readonly IConfiguration _configuration;

        public TreinaPropostaServiceTeste()
        {
            var taxa = new Dictionary<string, string>
            {
                {"TaxaJuros", "0.011" }
            };
            var _configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(taxa)
                .Build();


            _mapper = new Mapper(
                new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new PropostaProfile());
                }));

            _mensagem = new Mensagem();

            _validacaoProposta = new ValidacaoProposta(_mensagem);

            _provedorMock = new();

            _clienteServiceMock = new();

            _treinaPropostasRepositorioMock = new();

            _treinaPropostaService = new TreinaPropostaService(_mapper, _mensagem,
                _treinaPropostasRepositorioMock.Object,
                _clienteServiceMock.Object,
                _validacaoProposta,
                _provedorMock.Object,
                _configuration);
            
        }



        [Fact]
        public async void ConsultaProposta_RecebePropopostaEUsuario_RetornaProposta()
        {
            var retorno = new List<ConsultaTreinaProposta>();
            var consultaProposta = new ConsultaTreinaProposta()
            {
                Cpf = "123"
            };
            retorno.Add(consultaProposta); // lista populada para ser retornada

            _treinaPropostasRepositorioMock.Setup(m => m.ConsultarProposta(It.IsAny<PropostaBuscada>(), It.IsAny<string>())
               ).ReturnsAsync(retorno);


            var prop = new PropostaBuscada();

            var resultado = await _treinaPropostaService.ConsultarProposta(prop, "4001MARIA");

            Assert.NotEmpty(resultado);
        }



        [Fact]
        public async void ConsultaProposta_RecebePropopostaEUsuario_RetornaNull()
        {
            var retorno = new List<ConsultaTreinaProposta>(); // lista vazia

            _treinaPropostasRepositorioMock.Setup(m => m.ConsultarProposta(It.IsAny<PropostaBuscada>(), It.IsAny<string>())
               ).ReturnsAsync(retorno);


            var prop = new PropostaBuscada();

            var resultado = await _treinaPropostaService.ConsultarProposta(prop, "4001MARIA");

            Assert.Null(resultado);
        }


        [Fact]
        public async void CriaProposta_RecebePropostaDtoClienteDtoUsuario_RetornaProposta()
        {
            var propDto = new PropostaDto()
            {
                Prazo = 10,
                Vlr_Financiado = 100,
                Vlr_Solicitado = 100,
                Conveniada = "000020"
            };

            var clienteDto = new ClienteDto()
            {
                Nome = "Frodo Bolseiro"
            };

            var usuario = "4001Maria";

            var retornoCliente = new TreinaCliente()
            {
                Cpf = "12345678912"
            };

            var resultadoEsperado = _mapper.Map<TreinaProposta>(propDto);

            _clienteServiceMock.Setup(m => m.CadastrarEAtualizarCliente(It.IsAny<ClienteDto>()
                , It.IsAny<string>())
                ).ReturnsAsync(retornoCliente);

            _treinaPropostasRepositorioMock.Setup(m => m.CriarProposta(It.IsAny<TreinaProposta>()
                , It.IsAny<TreinaCliente>()
                , It.IsAny<string>())
                ).ReturnsAsync(_mapper.Map<TreinaProposta>(propDto));

            

            var resultado = await _treinaPropostaService.CriarProposta(propDto, clienteDto, usuario);

            Assert.True(resultadoEsperado.Equals(resultado));
        }


        [Fact]
        public async void CriaProposta_RecebePropostaDtoClienteDtoUsuario_RetornaClienteNull()
        {
            var propDto = new PropostaDto()
            {
                Prazo = 10,
                Vlr_Financiado = 100,
                Vlr_Solicitado = 100,
                Conveniada = "000020"
            };

            var clienteDto = new ClienteDto()
            {
                Nome = "Frodo Bolseiro"
            };

            var usuario = "4001Maria";

            TreinaCliente retornoClienteNull = null;

            var resultadoEsperado = _mapper.Map<TreinaProposta>(propDto);

            _clienteServiceMock.Setup(m => m.CadastrarEAtualizarCliente(It.IsAny<ClienteDto>()
                , It.IsAny<string>())
                ).ReturnsAsync(retornoClienteNull);

            _treinaPropostasRepositorioMock.Setup(m => m.CriarProposta(It.IsAny<TreinaProposta>()
                , It.IsAny<TreinaCliente>()
                , It.IsAny<string>())
                ).ReturnsAsync(_mapper.Map<TreinaProposta>(propDto));



            var resultado = await _treinaPropostaService.CriarProposta(propDto, clienteDto, usuario);

            Assert.True(resultado is null);
        }


        [Fact]
        public async void CriaProposta_RecebePropostaDtoInvalida_AdicionaErroNaMensagem()
        {
            var propDto = new PropostaDto()
            {
                Prazo = 10,
                Vlr_Financiado = -100,
                Vlr_Solicitado = 100,
                Conveniada = "000020"
            };

            var clienteDto = new ClienteDto()
            {
                Nome = "Frodo Bolseiro"
            };

            var usuario = "4001Maria";

            var retornoCliente = new TreinaCliente()
            {
                Cpf = "12345678912"
            };

            var resultadoEsperado = _mapper.Map<TreinaProposta>(propDto);

            _clienteServiceMock.Setup(m => m.CadastrarEAtualizarCliente(It.IsAny<ClienteDto>()
                , It.IsAny<string>())
                ).ReturnsAsync(retornoCliente);

            _treinaPropostasRepositorioMock.Setup(m => m.CriarProposta(It.IsAny<TreinaProposta>()
                , It.IsAny<TreinaCliente>()
                , It.IsAny<string>())
                ).ReturnsAsync(_mapper.Map<TreinaProposta>(propDto));



            var resultado = await _treinaPropostaService.CriarProposta(propDto, clienteDto, usuario);

            Assert.True(_mensagem.PossuiErros);
        }

    }
}
