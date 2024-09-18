namespace GuiaPlus.Domain.Entities;
using GuiaPlus.Domain.Enums;

public class Cliente
{
    public int Id { get; set; }
    public string CPF_CNPJ { get; set; }
    public string NomeCompleto { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public StatusCliente Status { get; set; }
}
