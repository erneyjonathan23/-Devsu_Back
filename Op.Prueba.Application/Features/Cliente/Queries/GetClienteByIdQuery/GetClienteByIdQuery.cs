using MediatR;
using OP.Prueba.Application.Exceptions;
using OP.Prueba.Application.Interfaces;

namespace OP.Prueba.Application.Features.Cliente.Queries.GetClienteByIdQuery
{
    public class GetClienteByIdQuery : IRequest<Domain.Entities.Cliente>
    {
        public int Id { get; set; }
    }

    public class GetClienteByIdQueryHandler : IRequestHandler<GetClienteByIdQuery, Domain.Entities.Cliente>
    {
        private readonly IRepositoryAsync<Domain.Entities.Cliente> _clienteRepository;

        public GetClienteByIdQueryHandler(IRepositoryAsync<Domain.Entities.Cliente> clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<Domain.Entities.Cliente> Handle(GetClienteByIdQuery request, CancellationToken cancellationToken)
        {
            Domain.Entities.Cliente? cliente = await _clienteRepository.GetByIdAsync(request.Id);
            if (cliente == null)
                throw new ApiException($"Cliente no encontrado con el id {request.Id}");

            return cliente;
        }
    }
}