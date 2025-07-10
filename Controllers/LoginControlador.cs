using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microservicio.Login.Api.Aplicacion;

namespace Microservicio.Login.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginControlador : ControllerBase
    {
        private readonly IMediator _mediator;

        public LoginControlador(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        public async Task<ActionResult<List<LoginDto>>> GetLogins()
        {
            return await _mediator.Send(new ConsultaLogin.ListaLogin());
        }

        // Endpoint para buscar por usuario
        [HttpGet("usuario/{usuario}")]
        public async Task<ActionResult<LoginDto>> GetPorUsuario(string usuario)
        {
            try
            {
                var result = await _mediator.Send(new ConsultaLoginPorUsuario.Ejecuta
                {
                    Usuario = usuario // Búsqueda exacta (case-sensitive)
                });
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message); // 404 si no existe
            }
        }

        [HttpPost]
        public async Task<IActionResult> CrearLogin([FromBody] NuevoLogin.Ejecuta data)
        {
            await _mediator.Send(data);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> ActualizarLogin([FromBody] ActualizarLogin.EjecutaActualizar data)
        {
            try
            {
                await _mediator.Send(data);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
