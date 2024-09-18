using AutoMapper;
using AutoMapper.QueryableExtensions;
using GuiaPlus.Application.DTOs.Cliente;
using GuiaPlus.Domain.Entities;
using GuiaPlus.Domain.Interfaces.Services;
using GuiaPlus.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace GuiaPlus.Application.Services
{
    public class ClienteService : IClienteService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ClienteService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ClienteDetailsResponse> CreateClienteAsync(ClienteCreateRequest clienteCreateRequest)
        {
            var cliente = _mapper.Map<Cliente>(clienteCreateRequest);

            try
            {
                var createdUser = _context.Clientes.Add(cliente);
                await _context.SaveChangesAsync();

                return _mapper.Map<ClienteDetailsResponse>(createdUser.Entity);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Ocorreu um erro ao criar o endereço.", ex);
            }

           
        }

        public async Task<ClienteEnderecoResponse> CreateEnderecoAsync(ClienteEnderecoCreateRequest clienteEnderecoCreateRequest)
        {
            var clienteExists = await _context.Clientes.AnyAsync(c => c.Id == clienteEnderecoCreateRequest.ClienteId);
            if (!clienteExists)
            {
                throw new ArgumentException("O cliente especificado não existe.");
            }

            var endereco = _mapper.Map<ClienteEndereco>(clienteEnderecoCreateRequest);

            try
            {
                var createdEndereco = _context.ClienteEnderecos.Add(endereco);
                await _context.SaveChangesAsync();

                return _mapper.Map<ClienteEnderecoResponse>(createdEndereco.Entity);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Ocorreu um erro ao criar o endereço.", ex);
            }
        }

        public async Task<IEnumerable<ClienteSummaryResponse>> GetAllClientesAsync()
        {
            return await _context.Clientes
                .ProjectTo<ClienteSummaryResponse>(_mapper.ConfigurationProvider)
                .ToListAsync();
            //ProjectTo é um metodo extendido pelo autoMapper que permite uma conversão da Entidade original para um DTO
            //Diretamente na consulta SQL, melhorando a consulta e a perfomace da aplicação.
        }

        public async Task<ClienteDetailsResponse?> GetClienteByIdAsync(int id)
        {
            var cliente = await _context.Clientes
                .Include(c => c.ClienteEnderecos)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cliente is null) return null;

            return _mapper.Map<ClienteDetailsResponse>(cliente);
        }
    }
}
