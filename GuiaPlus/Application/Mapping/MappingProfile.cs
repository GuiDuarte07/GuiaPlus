using AutoMapper;
using GuiaPlus.Application.DTOs.Cliente;
using GuiaPlus.Domain.Entities;

namespace GuiaPlus.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Maps da entidade Cliente e ClienteEndereco
            CreateMap<ClienteCreateRequest, Cliente>();
            CreateMap<ClienteEnderecoCreateRequest, ClienteEndereco>();
            CreateMap<Cliente, ClienteSummaryResponse>();
            CreateMap<Cliente, ClienteDetailsResponse>()
                .ForMember(dest => dest.ClienteEnderecos, opt => opt.MapFrom(src => src.ClienteEnderecos));
            CreateMap<ClienteEndereco, ClienteEnderecoResponse>();


            // Maps da entidade Servico

            // Maps da entidade Guia
        }
    }
}
