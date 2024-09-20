using GuiaPlus.Application.DTOs.Guia;
using GuiaPlus.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace GuiaPlus.Api.Controllers
{
    [ApiController]
    [Route("api/guia")]
    public class GuiaController : ControllerBase
    {
        private readonly IGuiaService _guiaService;

        public GuiaController(IGuiaService guiaService)
        {
            _guiaService = guiaService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateGuia([FromBody] GuiaCreateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var guia = await _guiaService.CreateGuiaAsync(request);
                return Ok(guia);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGuias([FromQuery] bool filterFinished = true)
        {
            try
            {
                var guias = await _guiaService.GetAllGuiasAsync(filterFinished);
                return Ok(guias);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGuia(int id)
        {
            try
            {
                await _guiaService.DeleteGuiaAsync(id);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("status")]
        public async Task<IActionResult> UpdateGuiaStatus([FromBody] GuiaUpdateStatusRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _guiaService.UpdateGuiaStatusAsync(request);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("endereco")]
        public async Task<IActionResult> UpdateGuiaEndereco([FromBody] GuiaUpdateEnderecoRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _guiaService.UpdateGuiaEnderecoAsync(request);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
