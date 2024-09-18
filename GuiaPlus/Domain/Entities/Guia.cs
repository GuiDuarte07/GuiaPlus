namespace GuiaPlus.Domain.Entities;
using GuiaPlus.Domain.Enums;

public class Guia
{
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public int ServicoId { get; set; }
    public int ClienteEnderecoId { get; set; }
    public string NumeroGuia { get; set; }
    public StatusGuia Status { get; set; }
    public DateTime DataHoraRegistro { get; set; }
    public DateTime? DataHoraIniciouColeta { get; set; }
    public DateTime? DataHoraConfirmouRetirada { get; set; }

    public Cliente Cliente { get; set; }
    public Servico Servico { get; set; }
    public ClienteEndereco ClienteEndereco { get; set; }
}


