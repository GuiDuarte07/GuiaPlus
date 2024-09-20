using GuiaPlus.Application.DTOs.Cliente;

namespace GuiaPlus.Domain.Interfaces.Services
{
    public interface IClienteService
    {
        public Task<ClienteDetailsResponse> CreateClienteAsync(ClienteCreateRequest clienteCreateRequest);
        public Task<ClienteEnderecoResponse> CreateEnderecoAsync(ClienteEnderecoCreateRequest clienteEnderecoCreateRequest);
        public Task<ClienteDetailsResponse?> GetClienteByCpfCnpjAsync(string cpfCnpj);
        public Task<IEnumerable<ClienteSummaryResponse>> GetAllClientesAsync();
        public Task<ClienteEnderecoResponse> UpdateEnderecoPosition(EnderecoUpdatePositionRequest enderecoUpdatePositionRequest);
    }
}
