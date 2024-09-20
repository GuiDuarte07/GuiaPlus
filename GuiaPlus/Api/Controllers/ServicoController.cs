using GuiaPlus.Application.DTOs.Servico;
using GuiaPlus.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace GuiaPlus.Api.Controllers
{
    [ApiController]
    [Route("api/servico")]
    public class ServicoController : ControllerBase
    {
        private readonly IServicoService _servicoService;

        public ServicoController(IServicoService servicoService)
        {
            _servicoService = servicoService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateServico([FromBody] ServicoCreateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _servicoService.CreateServicoAsync(request);
            return Created("", response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllServicos()
        {
            var servicos = await _servicoService.GetAllServicosAsync();
            return Ok(servicos);
        }
    }
}
