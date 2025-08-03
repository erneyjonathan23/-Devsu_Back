using Microsoft.AspNetCore.Mvc;
using OP.Prueba.Application.Features.Movimiento.Commands.CreateMovimientoCommand;
using OP.Prueba.Application.Features.Movimiento.Commands.DeleteMovimientoCommand;
using OP.Prueba.Application.Features.Movimiento.Commands.UpdateMovimientoCommand;
using OP.Prueba.Application.Features.Movimiento.Queries.GetAllMovimientosQuery;
using OP.Prueba.Application.Features.Movimiento.Queries.GetMovimientoByIdQuery;
using OP.Prueba.Application.Wrappers;
using OP.Prueba.WebAPI.Controllers;

namespace OP.Prueba.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovimientoController : BaseApiController
    {
        /// <summary>
        /// Obtiene una lista paginada de movimientos.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<Response<IEnumerable<Domain.Entities.Movimiento>>>> GetAllMovimientos([FromQuery] GetAllMovimientosQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Obtiene un movimiento por su Id.
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Response<Domain.Entities.Movimiento>>> GetMovimientoById(int id)
        {
            var query = new GetMovimientoByIdQuery { MovimientoId = id };
            var result = await Mediator.Send(query);
            return Ok(new Response<Domain.Entities.Movimiento>(result));
        }

        /// <summary>
        /// Crea un nuevo movimiento.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Response<int>>> CreateMovimiento([FromBody] CreateMovimientoCommand command)
        {
            var result = await Mediator.Send(command);
            return CreatedAtAction(nameof(GetMovimientoById), new { id = result }, new Response<int>(result));
        }

        /// <summary>
        /// Actualiza un movimiento existente.
        /// </summary>
        [HttpPut("{id:int}")]
        public async Task<ActionResult<Response<int>>> UpdateMovimiento(int id, [FromBody] UpdateMovimientoCommand command)
        {
            if (id != command.MovimientoId)
                return BadRequest("El Id del movimiento en la URL no coincide con el del cuerpo de la solicitud.");

            var result = await Mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Elimina un movimiento por su Id.
        /// </summary>
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Response<int>>> DeleteMovimiento(int id)
        {
            var command = new DeleteMovimientoCommand { MovimientoId = id };
            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}