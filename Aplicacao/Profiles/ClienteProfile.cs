using Aplicacao.Dto;
using Aplicacao.Dto.Cliente;
using AutoMapper;
using Dominio.Modelos;


namespace Aplicacao.Profiles
{
    public class ClienteProfile : Profile
    {
        public ClienteProfile()
        {
            CreateMap<ClienteDto, TreinaCliente>();
        }
    }
}
