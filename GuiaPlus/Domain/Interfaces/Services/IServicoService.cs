using GuiaPlus.Application.DTOs.Servico;

namespace GuiaPlus.Domain.Interfaces.Services
{
    public interface IServicoService
    {
        public Task<ServicoResponse> CreateServicoAsync(ServicoCreateRequest request);
        public Task<IEnumerable<ServicoResponse>> GetAllServicosAsync();
    }
}
