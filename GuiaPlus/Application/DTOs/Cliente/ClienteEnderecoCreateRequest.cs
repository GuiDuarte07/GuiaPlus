namespace GuiaPlus.Application.DTOs.Cliente
{
    public class ClienteEnderecoCreateRequest
    {
        public int ClienteId { get; set; }
        public string CEP { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Complemento { get; set; }
        public string Numero { get; set; }
    }
}
