namespace GuiaPlus.Domain.Entities;

public class ClienteEndereco
{
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public string CEP { get; set; }
    public string Logradouro { get; set; }
    public string Bairro { get; set; }
    public string Cidade { get; set; }
    public string Complemento { get; set; }
    public string Numero { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    // Navegação para Cliente
    public Cliente Cliente { get; set; }
}

