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

namespace GuiaPlus.Application.Services
{
    public class GuiaService : IGuiaService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public GuiaService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GuiaResponse> CreateGuiaAsync(GuiaCreateRequest guiaCreateRequest)
        {
            var guia = _mapper.Map<Guia>(guiaCreateRequest);

            guia.DataHoraRegistro = DateTime.Now;
            guia.NumeroGuia = RandomGenerator.GenerateGuiaNumber();

            guia.Status = StatusGuia.NOVO;

            if (!await _context.Clientes.AnyAsync(c => c.Id == guiaCreateRequest.ClienteId))
            {
                throw new Exception("Cliente dessa guia não encontrado.");
            }

            if (!await _context.ClienteEnderecos.AnyAsync(ce => ce.Id == guiaCreateRequest.ClienteEnderecoId))
            {
                throw new Exception("Endereço dessa guia não encontrado.");
            }

            if (!await _context.Servicos.AnyAsync(s => s.Id == guiaCreateRequest.ServicoId))
            {
                throw new Exception("Serviço dessa guia não encontrado.");
            }

            try
            {
                var createdGuia = _context.Guias.Add(guia);

                await _context.SaveChangesAsync();
                return _mapper.Map<GuiaResponse>(createdGuia.Entity);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Um erro ocorreu ao tentar criar a guia.", ex);
            }
            
        }

        public async Task<IEnumerable<GuiaResponse>> GetAllGuiasAsync()
        {
            try
            {
                return await _context.Guias
                .ProjectTo<GuiaResponse>(_mapper.ConfigurationProvider)
                .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Um erro ocorreu ao resgatar as guias.", ex);
            }
        }

        public async Task DeleteGuiaAsync(int id)
        {
            var guia = await _context.Guias.FirstOrDefaultAsync(g => g.Id == id);

            if (guia is null)
                throw new Exception("Guia não encontrada.");

            if (guia.Status != StatusGuia.NOVO)
                throw new InvalidOperationException("Somente guias com status 'NOVO' podem ser excluídas.");

            try
            {
                _context.Guias.Remove(guia);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex) 
            { 
                throw new ApplicationException("Um erro ocorreu ao tentar excluir essa guia.", ex);
            }
            
        }

        public async Task<GuiaResponse> UpdateGuiaStatusAsync(GuiaUpdateStatusRequest guiaUpadateRequest)
        {
            try
            {
                var guia = await _context.Guias.FirstOrDefaultAsync(g => g.Id == guiaUpadateRequest.Id);

                if (guia is null)
                    throw new Exception("Guia não encontrada.");

                guia.Status = guiaUpadateRequest.Status;

                if (guiaUpadateRequest.Status == StatusGuia.INICIOU_COLETA)
                    guia.DataHoraIniciouColeta = DateTime.Now;
                else if (guiaUpadateRequest.Status == StatusGuia.CONFIRMOU_RETIRADA)
                    guia.DataHoraConfirmouRetirada = DateTime.Now;

                await _context.SaveChangesAsync();

                return _mapper.Map<GuiaResponse>(guia);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Um erro ocorreu ao tentar atualizar essa guia.", ex);
            }
        }

        public async Task<GuiaResponse> UpdateGuiaEnderecoAsync(GuiaUpdateEnderecoRequest request)
        {
            try
            {
                var guia = await _context.Guias.FindAsync(request.Id);
                if (guia is null)
                    throw new Exception("Guia não encontrada.");

                if (guia.Status != StatusGuia.INICIOU_COLETA)
                    throw new InvalidOperationException("Somente guias com status 'INICIOU_COLETA' podem ter o endereço corrigido.");

                guia.ClienteEnderecoId = request.ClienteEnderecoId;
                await _context.SaveChangesAsync();

                return _mapper.Map<GuiaResponse>(guia);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Um erro ocorreu ao tentar atualizar o endereço dessa guia.", ex);
            }
            
        }
    }
}
