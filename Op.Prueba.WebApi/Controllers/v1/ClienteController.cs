using Microsoft.AspNetCore.Mvc;
using MediatR;
using OP.Prueba.Application.Features.Cliente.Commands.CreateClienteCommand;
using OP.Prueba.Application.Features.Cliente.Commands.DeleteClienteCommand;
using OP.Prueba.Application.Features.Cliente.Commands.UpdateClienteCommand;
using OP.Prueba.Application.Features.Cliente.Queries.GetClienteByIdQuery;
using OP.Prueba.Application.Features.Cliente.Queries.GetAllClientesQuery;
using OP.Prueba.Application.Wrappers;
using OP.Prueba.WebAPI.Controllers;

namespace OP.Prueba.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : BaseApiController
    {
        /// <summary>
        /// Obtiene una lista paginada de clientes.
        /// </summary>
        /// <param name="query">Filtros de búsqueda y parámetros de paginación.</param>
        /// <returns>Lista paginada de clientes.</returns>
        [HttpGet]
        public async Task<ActionResult<PagedResponse<List<Domain.Entities.Cliente>>>> GetAllClientes([FromQuery] GetAllClientesQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Obtiene un cliente por su Id.
        /// </summary>
        /// <param name="id">Identificador del cliente.</param>
        /// <returns>Cliente encontrado.</returns>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Domain.Entities.Cliente>> GetClienteById(int id)
        {
            var query = new GetClienteByIdQuery { Id = id };
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Crea un nuevo cliente.
        /// </summary>
        /// <param name="command">Datos del cliente a crear.</param>
        /// <returns>Id del cliente creado.</returns>
        [HttpPost]
        public async Task<ActionResult<int>> CreateCliente([FromBody] CreateClienteCommand command)
        {
            var result = await Mediator.Send(command);
            return CreatedAtAction(nameof(GetClienteById), new { id = result }, result);
        }

        /// <summary>
        /// Actualiza un cliente existente.
        /// </summary>
        /// <param name="id">Id del cliente a actualizar.</param>
        /// <param name="command">Datos a actualizar.</param>
        /// <returns>Id del cliente actualizado.</returns>
        [HttpPut("{id:int}")]
        public async Task<ActionResult<int>> UpdateCliente(int id, [FromBody] UpdateClienteCommand command)
        {
            command.Id = id;
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Elimina un cliente por su Id.
        /// </summary>
        /// <param name="id">Id del cliente a eliminar.</param>
        /// <returns>Id del cliente eliminado.</returns>
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<int>> DeleteCliente(int id)
        {
            var command = new DeleteClienteCommand { Id = id };
            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}