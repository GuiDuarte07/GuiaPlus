using AutoMapper;
using AutoMapper.QueryableExtensions;
using GuiaPlus.Application.DTOs.Cliente;
using GuiaPlus.Domain.Entities;
using GuiaPlus.Domain.Enums;
using GuiaPlus.Domain.Interfaces.Services;
using GuiaPlus.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace GuiaPlus.Application.Services
{
    public class ClienteService : IClienteService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<ClienteService> _logger;

        public ClienteService(AppDbContext context, IMapper mapper, ILogger<ClienteService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ClienteDetailsResponse> CreateClienteAsync(ClienteCreateRequest clienteCreateRequest)
        {
            _logger.LogInformation("Iniciando criação do cliente com CPF/CNPJ: {CpfCnpj}", clienteCreateRequest.CPF_CNPJ);

            var cliente = _mapper.Map<Cliente>(clienteCreateRequest);
            cliente.Status = StatusCliente.ATIVO;

            try
            {
                if (await _context.Clientes.AnyAsync(c => c.CPF_CNPJ == cliente.CPF_CNPJ)) {
                    _logger.LogWarning("Tentativa de criação de cliente com CPF/CNPJ já existente: {CpfCnpj}", cliente.CPF_CNPJ);
                    throw new ArgumentException("CPF/CNPJ já existe");
                }
                var createdUser = _context.Clientes.Add(cliente);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Cliente criado com sucesso: {ClienteId}", createdUser.Entity.Id);

                return _mapper.Map<ClienteDetailsResponse>(createdUser.Entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar o cliente com CPF/CNPJ: {CpfCnpj}", cliente.CPF_CNPJ);
                throw new InvalidOperationException("Ocorreu um erro ao criar o endereço.", ex);
            }

           
        }

        public async Task<ClienteEnderecoResponse> CreateEnderecoAsync(ClienteEnderecoCreateRequest clienteEnderecoCreateRequest)
        {
            _logger.LogInformation("Iniciando criação de endereço para o cliente id {ClienteId}", clienteEnderecoCreateRequest.ClienteId);

            var clienteExists = await _context.Clientes.AnyAsync(c => c.Id == clienteEnderecoCreateRequest.ClienteId);
            if (!clienteExists)
            {
                _logger.LogWarning("Tentativa de criação de endereço para cliente de id inexistente: {ClienteId}", clienteEnderecoCreateRequest.ClienteId);
                throw new ArgumentException("O cliente especificado não existe.");
            }

            var endereco = _mapper.Map<ClienteEndereco>(clienteEnderecoCreateRequest);

            try
            {
                var createdEndereco = _context.ClienteEnderecos.Add(endereco);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Endereço criado com sucesso para o cliente id {ClienteId}", clienteEnderecoCreateRequest.ClienteId);

                return _mapper.Map<ClienteEnderecoResponse>(createdEndereco.Entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar o endereço para o cliente id {ClienteId}", clienteEnderecoCreateRequest.ClienteId);
                throw new InvalidOperationException("Ocorreu um erro ao criar o endereço.", ex);
            }
        }

        public async Task<IEnumerable<ClienteSummaryResponse>> GetAllClientesAsync()
        {
            _logger.LogInformation("Buscando todos os clientes");

            var clientes = await _context.Clientes
                .ProjectTo<ClienteSummaryResponse>(_mapper.ConfigurationProvider)
                .ToListAsync();
            //ProjectTo é um metodo extendido pelo autoMapper que permite uma conversão da Entidade original para um DTO
            //Diretamente na consulta SQL, melhorando a consulta e a perfomace da aplicação.

            _logger.LogInformation("{Count} clientes encontrados", clientes.Count);

            return clientes;
        }

        public async Task<ClienteDetailsResponse?> GetClienteByCpfCnpjAsync(string cpfCnpj)
        {
            _logger.LogInformation("Buscando cliente com CPF/CNPJ: {CpfCnpj}", cpfCnpj);

            var cliente = await _context.Clientes
                .Include(c => c.ClienteEnderecos)
                .FirstOrDefaultAsync(c => c.CPF_CNPJ == cpfCnpj);

            if (cliente is null)
            {
                _logger.LogWarning("Cliente com CPF/CNPJ: {CpfCnpj} não encontrado", cpfCnpj);
                return null;
            }

            _logger.LogInformation("Cliente com CPF/CNPJ {CpfCnpj} encontrado: {ClienteId}", cpfCnpj, cliente.Id);

            return _mapper.Map<ClienteDetailsResponse>(cliente);
        }

        public async Task<ClienteEnderecoResponse> UpdateEnderecoPosition(EnderecoUpdatePositionRequest enderecoUpdatePositionRequest) 
        {
            _logger.LogInformation("Atualizando posição do endereço {EnderecoId}, lat {lat}, lon {lon}", enderecoUpdatePositionRequest.Id, enderecoUpdatePositionRequest.Latitude, enderecoUpdatePositionRequest.Longitude);

            var clienteEndereco = await _context.ClienteEnderecos.FindAsync(enderecoUpdatePositionRequest.Id);

            if (clienteEndereco is null)
            {
                _logger.LogWarning("Endereço não encontrado: {EnderecoId}", enderecoUpdatePositionRequest.Id);
                throw new ArgumentException("Endereço não encontrado.");
            }

            clienteEndereco.Latitude = enderecoUpdatePositionRequest.Latitude;
            clienteEndereco.Longitude = enderecoUpdatePositionRequest.Longitude;

            _context.Update(clienteEndereco);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Endereço {EnderecoId} atualizado com sucesso", enderecoUpdatePositionRequest.Id);

            return _mapper.Map<ClienteEnderecoResponse>(clienteEndereco);
        }
    }
}
