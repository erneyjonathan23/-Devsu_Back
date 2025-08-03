using AutoMapper;
using OP.Prueba.Application.Exceptions;
using OP.Prueba.Application.Interfaces;
using OP.Prueba.Application.Specifications;
using OP.Prueba.Application.Wrappers;
using OP.Prueba.Domain.Entities;
using OP.Prueba.Application.Features.Cliente.Commands.CreateClienteCommand;
using OP.Prueba.Application.Features.Cliente.Commands.DeleteClienteCommand;
using OP.Prueba.Application.Features.Cliente.Commands.UpdateClienteCommand;
using OP.Prueba.Application.Features.Cliente.Queries.GetClienteByIdQuery;
using OP.Prueba.Application.Features.Cliente.Queries.GetAllClientesQuery;

namespace OP.Prueba.Application.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IRepositoryAsync<Cliente> _clienteRepository;
        private readonly IMapper _mapper;

        public ClienteService(IRepositoryAsync<Cliente> clienteRepository, IMapper mapper)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
        }

        public async Task<Response<int>> CreateCliente(CreateClienteCommand request, CancellationToken cancellationToken)
        {
            Cliente newCliente = _mapper.Map<Cliente>(request);

            Cliente? createdCliente = await _clienteRepository.AddAsync(newCliente);

            return new Response<int>(createdCliente.PersonaId, "¡Cliente creado exitosamente!");
        }

        public async Task<Response<int>> DeleteCliente(DeleteClienteCommand request, CancellationToken cancellationToken)
        {
            Cliente? cliente = await _clienteRepository.GetByIdAsync(request.Id);
            if (cliente == null)
                throw new ApiException($"Cliente no encontrado con el id {request.Id}");

            await _clienteRepository.DeleteAsync(cliente);

            return new Response<int>(cliente.PersonaId, "¡Cliente eliminado exitosamente!");
        }

        public async Task<PagedResponse<List<Cliente>>> GetAllClientes(GetAllClientesQuery request, CancellationToken cancellationToken)
        {
            PagedClienteSpecification spec = new PagedClienteSpecification(request.PageSize, request.PageNumber, request.Nombres, request.Identificacion, request.EstadoId);

            List<Cliente> clientes = await _clienteRepository.ListAsync(spec);
            int totalCount = await _clienteRepository.CountAsync(spec);

            return new PagedResponse<List<Cliente>>(clientes, request.PageNumber ?? 1, request.PageSize ?? 10, totalCount);
        }

        public async Task<Response<Cliente>> GetClienteById(GetClienteByIdQuery request, CancellationToken cancellationToken)
        {
            Cliente? cliente = await _clienteRepository.GetByIdAsync(request.Id);
            if (cliente == null)
                throw new ApiException($"Cliente no encontrado con el id {request.Id}");

            return new Response<Cliente>(cliente, "¡Cliente encontrado exitosamente!");
        }

        public async Task<Response<int>> UpdateCliente(UpdateClienteCommand request, CancellationToken cancellationToken)
        {
            Cliente? cliente = await _clienteRepository.GetByIdAsync(request.Id);
            if (cliente == null)
                throw new ApiException($"Cliente no encontrado con el id {request.Id}");

            _mapper.Map(request, cliente);

            await _clienteRepository.UpdateAsync(cliente);

            return new Response<int>(cliente.PersonaId, "¡Cliente actualizado exitosamente!");
        }
    }
}
