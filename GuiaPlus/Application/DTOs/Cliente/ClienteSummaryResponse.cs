using GuiaPlus.Domain.Enums;

namespace GuiaPlus.Application.DTOs.Cliente
{
    public class ClienteSummaryResponse
    {
        public int Id { get; set; }
        public string NomeCompleto { get; set; }
        public string CPF_CNPJ { get; set; }
        public StatusCliente Status { get; set; }
    }
}
