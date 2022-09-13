using Aplicacao.Dto.Cliente;
using Aplicacao.Interface;
using Aplicacao.Profiles;
using Aplicacao.Servico;
using AutoMapper;
using Dominio.Mensagem;
using Dominio.Modelos;
using Dominio.Validacoes;
using Dominio.Validacoes.Interface;
using Dominio.Interface.Repositorios;
using Moq;
using Xunit;

namespace BemPreparadosTestes.Testes
{
    public class ClienteServiceTeste
    {
        private readonly IMapper _mapper;
        private readonly IMensagem _mensagem;
        private readonly Mock<ITreinaClientesRepositorio> _repositorio;
        private readonly IValidacaoCliente _validacaoCliente;
        private readonly ICliente _clienteService;

        public ClienteServiceTeste()
        {
            _mapper = new Mapper(
                new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new ClienteProfile());
                }));

            _mensagem = new Mensagem();

            _repositorio = new();

            _validacaoCliente = new ValidacaoCliente(_mensagem);

            _clienteService = new ClienteService(_mapper, _mensagem, _repositorio.Object, _validacaoCliente);
        }


        [Fact]
        public async void BuscaCliente_RecebeStringCpf_RetornaTreinaCliente()
        {

            var retornoCliente = new TreinaCliente()
            {
                Cpf = "12345678912"
            };

            var cpfBuscado = "12345678912";

            _repositorio.Setup(m => m.BuscarCliente("12345678912")
                ).ReturnsAsync(retornoCliente);

            var resultado = await _clienteService.BuscarCliente(cpfBuscado);
            Assert.True(retornoCliente.Cpf == resultado.Cpf);
        }


        [Fact]
        public async void BuscaCliente_RecebeStringCpf_RetornaNull()
        {

            TreinaCliente retornoClienteNull = null;

            var cpfBuscado = "12345678912";

            _repositorio.Setup(m => m.BuscarCliente(It.IsAny<string>()))
                .ReturnsAsync(retornoClienteNull);

            var resultado = await _clienteService.BuscarCliente(cpfBuscado);
            Assert.True(resultado is null);
        }


        [Fact]
        public async void CadastraEAtualizaCliente_RecebeClienteDtoEUsuario_RetornaTreinaClienteCriado()
        {
            TreinaCliente retornoClienteNull = null;

            var clienteEntrada = new ClienteDto()
            {
                Cpf = "12345678912",
                Nome = "Gabriel Francisco",
                Genero = "M",
                Cep = "10000100",
                VlrSalario = 1000
            };

            var clienteSaida = _mapper.Map<TreinaCliente>(clienteEntrada);

            _repositorio.Setup(m => m.BuscarCliente("12345678912")
                ).ReturnsAsync(retornoClienteNull);

            _repositorio.Setup(m => m.CadastrarCliente(It.IsAny<TreinaCliente>(), It.IsAny<string>())
                ).ReturnsAsync(clienteSaida);

            var resultado = await _clienteService.CadastrarEAtualizarCliente(clienteEntrada, "4001MARIA");
            Assert.Equal("12345678912", resultado.Cpf);
        }


        [Fact]
        public async void CadastraEAtualizaCliente_RecebeClienteDtoEUsuario_RetornaAtualizaClienteAtualizado()
        {

            var clienteEntrada = new ClienteDto()
            {
                Cpf = "12345678912",
                Nome = "Gabriel Francisco",
                Genero = "M",
                Cep = "10000100",
                VlrSalario = 1000
            };

            var retornoCliente = _mapper.Map<TreinaCliente>(clienteEntrada);

            var clienteAtualizado = _mapper.Map<TreinaCliente>(clienteEntrada);
            clienteAtualizado.VlrSalario = 2000;

            _repositorio.Setup(m => m.BuscarCliente("12345678912")
                ).ReturnsAsync(retornoCliente);

            _repositorio.Setup(m => m.AtualizarCliente(It.IsAny<TreinaCliente>(), It.IsAny<string>())
                ).ReturnsAsync(clienteAtualizado);

            var resultado = await _clienteService.CadastrarEAtualizarCliente(clienteEntrada, "4001MARIA");

            Assert.True(resultado.VlrSalario == 2000);
        }



        [Fact]
        public async void CadastraEAtualizaCliente_RecebeClienteDtoEUsuario_ValidacaoNaoPassa()
        {

            var clienteEntrada = new ClienteDto()
            {
                Cpf = "",
                Nome = "Gabriel Francisco",
                Genero = "M",
                Cep = "10000100",
                VlrSalario = 1000
            };

            var resultado = await _clienteService.CadastrarEAtualizarCliente(clienteEntrada, "4001MARIA");
            Assert.True(resultado is null && _mensagem.PossuiErros);
        }

    }
}
