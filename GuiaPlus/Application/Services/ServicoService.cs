using AutoMapper;
using AutoMapper.QueryableExtensions;
using GuiaPlus.Application.DTOs.Servico;
using GuiaPlus.Domain.Entities;
using GuiaPlus.Domain.Interfaces.Services;
using GuiaPlus.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

namespace GuiaPlus.Application.Services
{
    public class ServicoService : IServicoService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<ServicoService> _logger;

        public ServicoService(AppDbContext context, IMapper mapper, ILogger<ServicoService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ServicoResponse> CreateServicoAsync(ServicoCreateRequest request)
        {
            try
            {
                var servico = _mapper.Map<Servico>(request);
                var createdServico = _context.Servicos.Add(servico);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Serviço criado com sucesso. ID: {servicoId}", createdServico.Entity.Id);

                return _mapper.Map<ServicoResponse>(createdServico.Entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Um erro ocorreu ao tentar criar esse serviço.");
                throw new ApplicationException("Um erro ocorreu ao tentar criar esse serviço.", ex);
            }
        }

        public async Task<IEnumerable<ServicoResponse>> GetAllServicosAsync()
        {
            try
            {
                _logger.LogInformation("Iniciando consulta para obter todos os serviços.");

                var servicos = await _context.Servicos
                    .ProjectTo<ServicoResponse>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                _logger.LogInformation("{Count} Serviços encontrados", servicos.Count);

                return servicos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Um erro ocorreu ao resgatar os serviços.");
                throw new ApplicationException("Um erro ocorreu ao resgatar os serviços.", ex);
            }
        }
    }
}
