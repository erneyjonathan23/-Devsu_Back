using OP.Prueba.Application.Wrappers;
using OP.Prueba.Application.Features.Cliente.Commands.CreateClienteCommand;
using OP.Prueba.Application.Features.Cliente.Commands.DeleteClienteCommand;
using OP.Prueba.Application.Features.Cliente.Commands.UpdateClienteCommand;
using OP.Prueba.Application.Features.Cliente.Queries.GetClienteByIdQuery;
using OP.Prueba.Application.Features.Cliente.Queries.GetAllClientesQuery;
using OP.Prueba.Domain.Entities;

namespace OP.Prueba.Application.Interfaces
{
    /// <summary>
    /// Servicio para gestionar operaciones CRUD sobre la entidad Cliente.
    /// </summary>
    public interface IClienteService
    {
        /// <summary>
        /// Crea un nuevo cliente en el sistema.
        /// </summary>
        /// <param name="request">Datos del cliente a crear.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        /// <returns>Identificador único (int) del cliente creado.</returns>
        Task<Response<int>> CreateCliente(CreateClienteCommand request, CancellationToken cancellationToken);

        /// <summary>
        /// Elimina un cliente existente por su identificador.
        /// </summary>
        /// <param name="request">Comando con el Id del cliente a eliminar.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        /// <returns>Identificador (int) del cliente eliminado.</returns>
        Task<Response<int>> DeleteCliente(DeleteClienteCommand request, CancellationToken cancellationToken);

        /// <summary>
        /// Obtiene una lista paginada de clientes filtrada por parámetros.
        /// </summary>
        /// <param name="request">Parámetros de búsqueda y paginación.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        /// <returns>Lista paginada de clientes.</returns>
        Task<PagedResponse<List<Cliente>>> GetAllClientes(GetAllClientesQuery request, CancellationToken cancellationToken);

        /// <summary>
        /// Obtiene la información de un cliente por su identificador.
        /// </summary>
        /// <param name="request">Query con el Id del cliente a consultar.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        /// <returns>Entidad Cliente encontrada.</returns>
        Task<Response<Cliente>> GetClienteById(GetClienteByIdQuery request, CancellationToken cancellationToken);

        /// <summary>
        /// Actualiza la información de un cliente existente.
        /// </summary>
        /// <param name="request">Datos actualizados del cliente.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        /// <returns>Identificador (int) del cliente actualizado.</returns>
        Task<Response<int>> UpdateCliente(UpdateClienteCommand request, CancellationToken cancellationToken);
    }
}
