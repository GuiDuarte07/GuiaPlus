using AutoMapper;
using AutoMapper.QueryableExtensions;
using GuiaPlus.Application.DTOs.Servico;
using GuiaPlus.Domain.Entities;
using GuiaPlus.Domain.Interfaces.Services;
using GuiaPlus.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace GuiaPlus.Application.Services
{
    public class ServicoService : IServicoService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ServicoService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServicoResponse> CreateServicoAsync(ServicoCreateRequest request)
        {
            try
            {
                var servico = _mapper.Map<Servico>(request);
                var createdServico = _context.Servicos.Add(servico);
                await _context.SaveChangesAsync();

                return _mapper.Map<ServicoResponse>(createdServico.Entity);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Um erro ocorreu ao tentar criar esse serviço.", ex);
            }
        }

        public async Task<IEnumerable<ServicoResponse>> GetAllServicosAsync()
        {
            try
            {
                return await _context.Servicos
                    .ProjectTo<ServicoResponse>(_mapper.ConfigurationProvider)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Um erro ocorreu ao resgatar os serviços.", ex);
            }
        }
    }
}
