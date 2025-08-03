using Microsoft.AspNetCore.Mvc;
using OP.Prueba.Application.Features.Cuenta.Commands.CreateCuentaCommand;
using OP.Prueba.Application.Features.Cuenta.Commands.DeleteCuentaCommand;
using OP.Prueba.Application.Features.Cuenta.Commands.UpdateCuentaCommand;
using OP.Prueba.Application.Features.Cuenta.Queries.GetAllCuentasQuery;
using OP.Prueba.Application.Features.Cuenta.Queries.GetCuentaByIdQuery;
using OP.Prueba.Application.Wrappers;
using OP.Prueba.WebAPI.Controllers;

namespace OP.Prueba.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CuentaController : BaseApiController
    {
        /// <summary>
        /// Obtiene una lista de cuentas.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<Response<List<Domain.Entities.Cuenta>>>> GetAllCuentas([FromQuery] GetAllCuentasQuery query)
        {
            Response<IEnumerable<Domain.Entities.Cuenta>> result = await Mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Obtiene una cuenta por su Id.
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Response<Domain.Entities.Cuenta>>> GetCuentaById(int id)
        {
            var query = new GetCuentaByIdQuery { CuentaId = id };
            var result = await Mediator.Send(query);
            return Ok(new Response<Domain.Entities.Cuenta>(result));
        }

        /// <summary>
        /// Crea una nueva cuenta.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Response<int>>> CreateCuenta([FromBody] CreateCuentaCommand command)
        {
            var result = await Mediator.Send(command);
            return CreatedAtAction(nameof(GetCuentaById), new { id = result }, new Response<int>(result));
        }

        /// <summary>
        /// Actualiza una cuenta existente.
        /// </summary>
        [HttpPut("{id:int}")]
        public async Task<ActionResult<Response<int>>> UpdateCuenta(int id, [FromBody] UpdateCuentaCommand command)
        {
            if (id != command.CuentaId)
                return BadRequest("El Id de la cuenta en la URL no coincide con el del cuerpo de la solicitud.");

            var result = await Mediator.Send(command);
            return Ok(new Response<int>(result));
        }

        /// <summary>
        /// Elimina una cuenta por su Id.
        /// </summary>
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Response<int>>> DeleteCuenta(int id)
        {
            var command = new DeleteCuentaCommand { CuentaId = id };
            var result = await Mediator.Send(command);
            return Ok(new Response<int>(result));
        }
    }
}