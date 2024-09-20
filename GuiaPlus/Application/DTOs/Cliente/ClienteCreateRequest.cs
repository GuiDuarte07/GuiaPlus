using GuiaPlus.Domain.Enums;

namespace GuiaPlus.Application.DTOs.Cliente
{
    public class ClienteCreateRequest
    {
        public string CPF_CNPJ { get; set; }
        public string NomeCompleto { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
    }
}
