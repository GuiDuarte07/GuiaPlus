using GuiaPlus.Application.DTOs.Cliente;
using GuiaPlus.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace GuiaPlus.Api.Controllers
{
    [ApiController]
    [Route("api/cliente")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateCliente([FromBody] ClienteCreateRequest clienteDto)
        {
            var cliente = await _clienteService.CreateClienteAsync(clienteDto);
            return CreatedAtAction(nameof(GetClienteById), new { id = cliente.Id }, cliente);
        }

        [HttpPost]
        [Route("create-address")]
        public async Task<IActionResult> CreateEndereco([FromBody] ClienteEnderecoCreateRequest enderecoDto)
        {
            var endereco = await _clienteService.CreateEnderecoAsync(enderecoDto);
            return Created("", endereco);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClienteById(int id)
        {
            var cliente = await _clienteService.GetClienteByIdAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return Ok(cliente);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllClientes()
        {
            var clientes = await _clienteService.GetAllClientesAsync();
            return Ok(clientes);
        }
    }

}
