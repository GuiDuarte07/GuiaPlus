using GuiaPlus.Application.DTOs.Servico;

namespace GuiaPlus.Domain.Interfaces.Services
{
    public interface IServicoService
    {
        Task<ServicoResponse> CreateServicoAsync(ServicoCreateRequest request);
        Task<IEnumerable<ServicoResponse>> GetAllServicosAsync();
    }
}
