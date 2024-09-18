using GuiaPlus.Domain.Enums;

namespace GuiaPlus.Application.DTOs.Cliente
{
    public class ClienteDetailsResponse
    {
        public int Id { get; set; }
        public string CPF_CNPJ { get; set; }
        public string NomeCompleto { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public StatusCliente Status { get; set; }
        public IEnumerable<ClienteEnderecoResponse> ClienteEnderecos { get; set; }
    }
}
