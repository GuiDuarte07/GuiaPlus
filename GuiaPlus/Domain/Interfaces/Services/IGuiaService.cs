using GuiaPlus.Application.DTOs.Guia;

namespace GuiaPlus.Domain.Interfaces.Services
{
    public interface IGuiaService
    {
        public Task<GuiaResponse> CreateGuiaAsync(GuiaCreateRequest guiaCreateRequest);
        public Task<IEnumerable<GuiaResponse>> GetAllGuiasAsync(bool filterFinished);
        public Task DeleteGuiaAsync(int id);
        public Task<GuiaResponse> UpdateGuiaStatusAsync(GuiaUpdateStatusRequest guiaUpadateRequest);
        public Task<GuiaResponse> UpdateGuiaEnderecoAsync(GuiaUpdateEnderecoRequest request);
    }
}
