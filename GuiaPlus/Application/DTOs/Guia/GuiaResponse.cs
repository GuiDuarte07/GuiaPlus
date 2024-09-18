using GuiaPlus.Application.DTOs.Cliente;
using GuiaPlus.Application.DTOs.Servico;
using GuiaPlus.Domain.Enums;

namespace GuiaPlus.Application.DTOs.Guia
{
    public class GuiaResponse
    {
        public int Id { get; set; }
        public ClienteSummaryResponse Cliente { get; set; }
        public ServicoResponse Servico { get; set; }
        public ClienteEnderecoResponse ClienteEndereco { get; set; }
        public string NumeroGuia { get; set; }
        public StatusGuia Status { get; set; }
        public DateTime DataHoraRegistro { get; set; }
        public DateTime? DataHoraIniciouColeta { get; set; }
        public DateTime? DataHoraConfirmouRetirada { get; set; }
    }
}
