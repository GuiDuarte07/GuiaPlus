using AutoMapper;
using AutoMapper.QueryableExtensions;
using GuiaPlus.Application.DTOs.Guia;
using GuiaPlus.Application.DTOs.Servico;
using GuiaPlus.Domain.Entities;
using GuiaPlus.Domain.Enums;
using GuiaPlus.Domain.Interfaces.Services;
using GuiaPlus.Infrastructure.Data.Context;
using GuiaPlus.Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging; // Importação para o Logger

namespace GuiaPlus.Application.Services
{
    public class GuiaService : IGuiaService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<GuiaService> _logger; // Logger

        public GuiaService(AppDbContext context, IMapper mapper, ILogger<GuiaService> logger) // Injeção do Logger
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GuiaResponse> CreateGuiaAsync(GuiaCreateRequest guiaCreateRequest)
        {
            _logger.LogInformation("Iniciando a criação de uma nova guia.");

            var guia = _mapper.Map<Guia>(guiaCreateRequest);
            guia.DataHoraRegistro = DateTime.Now;
            guia.NumeroGuia = RandomGenerator.GenerateGuiaNumber();
            guia.Status = StatusGuia.NOVO;

            _logger.LogInformation("Validando cliente, endereço e serviço para a nova guia.");
            if (!await _context.Clientes.AnyAsync(c => c.Id == guiaCreateRequest.ClienteId))
            {
                _logger.LogError("Cliente não encontrado. ID: {ClienteId}", guiaCreateRequest.ClienteId);
                throw new Exception("Cliente dessa guia não encontrado.");
            }

            if (!await _context.ClienteEnderecos.AnyAsync(ce => ce.Id == guiaCreateRequest.ClienteEnderecoId))
            {
                _logger.LogError("Endereço não encontrado. ID: {EnderecoId}", guiaCreateRequest.ClienteEnderecoId);
                throw new Exception("Endereço dessa guia não encontrado.");
            }

            if (!await _context.Servicos.AnyAsync(s => s.Id == guiaCreateRequest.ServicoId))
            {
                _logger.LogError("Serviço não encontrado. ID: {ServicoId}", guiaCreateRequest.ServicoId);
                throw new Exception("Serviço dessa guia não encontrado.");
            }

            try
            {
                _logger.LogInformation("Criando a guia.");
                var createdGuia = _context.Guias.Add(guia);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Guia criada com sucesso. ID: {GuiaId}", createdGuia.Entity.Id);

                return _mapper.Map<GuiaResponse>(createdGuia.Entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar a guia.");
                throw new ApplicationException("Um erro ocorreu ao tentar criar a guia.", ex);
            }
        }

        public async Task<IEnumerable<GuiaResponse>> GetAllGuiasAsync(bool filterFinished)
        {
            _logger.LogInformation("Resgatando todas as guias. Filtrar concluídas: {FilterFinished}", filterFinished);
            try
            {
                var query = _context.Guias.AsQueryable();
                if (filterFinished)
                {
                    query = query.Where(g => g.Status != StatusGuia.CONFIRMOU_RETIRADA);
                }

                var guias = await query.ProjectTo<GuiaResponse>(_mapper.ConfigurationProvider).ToListAsync();
                _logger.LogInformation("{Count} Guias encontrados", guias.Count);

                return guias;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao resgatar as guias.");
                throw new ApplicationException("Um erro ocorreu ao resgatar as guias.", ex);
            }
        }

        public async Task DeleteGuiaAsync(int id)
        {
            _logger.LogInformation("Tentando excluir a guia. ID: {GuiaId}", id);
            var guia = await _context.Guias.FirstOrDefaultAsync(g => g.Id == id);

            if (guia is null)
            {
                _logger.LogError("Guia não encontrada. ID: {GuiaId}", id);
                throw new Exception("Guia não encontrada.");
            }

            if (guia.Status != StatusGuia.NOVO)
            {
                _logger.LogWarning("Tentativa de excluir uma guia que não está com status NOVO. Status atual: {Status}", guia.Status);
                throw new InvalidOperationException("Somente guias com status 'NOVO' podem ser excluídas.");
            }

            try
            {
                _logger.LogInformation("Excluindo a guia. ID: {GuiaId}", id);
                _context.Guias.Remove(guia);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Guia excluída com sucesso. ID: {GuiaId}", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao excluir a guia. ID: {GuiaId}", id);
                throw new ApplicationException("Um erro ocorreu ao tentar excluir essa guia.", ex);
            }
        }

        public async Task<GuiaResponse> UpdateGuiaStatusAsync(GuiaUpdateStatusRequest guiaUpdateRequest)
        {
            _logger.LogInformation("Atualizando o status da guia. ID: {GuiaId}, Novo Status: {Status}", guiaUpdateRequest.Id, guiaUpdateRequest.Status);
            try
            {
                var guia = await _context.Guias.FirstOrDefaultAsync(g => g.Id == guiaUpdateRequest.Id);

                if (guia is null)
                {
                    _logger.LogError("Guia não encontrada. ID: {GuiaId}", guiaUpdateRequest.Id);
                    throw new Exception("Guia não encontrada.");
                }

                guia.Status = guiaUpdateRequest.Status;

                if (guiaUpdateRequest.Status == StatusGuia.INICIOU_COLETA)
                {
                    guia.DataHoraIniciouColeta = DateTime.Now;
                    _logger.LogInformation("Data de inicio de coleta da guia atualizado para {dataHoraIniciouColeta}", guia.DataHoraIniciouColeta);
                }
                else if (guiaUpdateRequest.Status == StatusGuia.CONFIRMOU_RETIRADA)
                {
                    guia.DataHoraConfirmouRetirada = DateTime.Now;
                    _logger.LogInformation("Data de retirada da guia atualizado para {dataHoraConfirmouRetirada}", guia.DataHoraConfirmouRetirada);
                }

                await _context.SaveChangesAsync();
                _logger.LogInformation("Status da guia atualizado com sucesso. ID: {GuiaId}, Novo Status: {Status}", guia.Id, guia.Status);

                return _mapper.Map<GuiaResponse>(guia);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar o status da guia. ID: {GuiaId}", guiaUpdateRequest.Id);
                throw new ApplicationException("Um erro ocorreu ao tentar atualizar essa guia.", ex);
            }
        }

        public async Task<GuiaResponse> UpdateGuiaEnderecoAsync(GuiaUpdateEnderecoRequest request)
        {
            _logger.LogInformation("Atualizando o endereço da guia. ID: {GuiaId}, Novo Endereço ID: {EnderecoId}", request.Id, request.ClienteEnderecoId);
            try
            {
                var guia = await _context.Guias.FindAsync(request.Id);

                if (guia is null)
                {
                    _logger.LogError("Guia não encontrada. ID: {GuiaId}", request.Id);
                    throw new Exception("Guia não encontrada.");
                }

                if (guia.Status != StatusGuia.INICIOU_COLETA)
                {
                    _logger.LogWarning("Tentativa de alterar o endereço de uma guia que não está com status INICIOU_COLETA. Status atual: {Status}", guia.Status);
                    throw new InvalidOperationException("Somente guias com status 'INICIOU_COLETA' podem ter o endereço corrigido.");
                }

                guia.ClienteEnderecoId = request.ClienteEnderecoId;
                await _context.SaveChangesAsync();
                _logger.LogInformation("Endereço da guia atualizado com sucesso. ID: {GuiaId}, Novo Endereço ID: {EnderecoId}", guia.Id, guia.ClienteEnderecoId);

                return _mapper.Map<GuiaResponse>(guia);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar o endereço da guia. ID: {GuiaId}", request.Id);
                throw new ApplicationException("Um erro ocorreu ao tentar atualizar o endereço dessa guia.", ex);
            }
        }
    }
}
