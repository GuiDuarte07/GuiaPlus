using GuiaPlus.Application.DTOs.Cliente;

namespace GuiaPlus.Domain.Interfaces.Services
{
    public interface IClienteService
    {
        Task<ClienteDetailsResponse> CreateClienteAsync(ClienteCreateRequest clienteCreateRequest);
        Task<ClienteEnderecoResponse> CreateEnderecoAsync(ClienteEnderecoCreateRequest clienteEnderecoCreateRequest);
        Task<ClienteDetailsResponse?> GetClienteByIdAsync(int id);
        Task<IEnumerable<ClienteSummaryResponse>> GetAllClientesAsync();
    }
}
