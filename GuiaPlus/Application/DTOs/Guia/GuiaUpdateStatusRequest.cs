using GuiaPlus.Domain.Enums;

namespace GuiaPlus.Application.DTOs.Guia
{
    public class GuiaUpdateStatusRequest
    {
        public int Id { get; set; }
        public StatusGuia Status { get; set; }
    }
}
