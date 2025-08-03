using MediatR;
using OP.Prueba.Application.Interfaces;
using OP.Prueba.Application.Specifications;
using OP.Prueba.Application.Wrappers;

namespace OP.Prueba.Application.Features.Cliente.Queries.GetAllClientesQuery
{
    public class GetAllClientesQuery : IRequest<PagedResponse<List<Domain.Entities.Cliente>>>
    {
        public int? PageNumber { get; set; } = null;
        public int? PageSize { get; set; } = null;
        public string? Nombres { get; set; }
        public string? Identificacion { get; set; }
        public int? EstadoId { get; set; }
    }

    public class GetAllClientesQueryHandler : IRequestHandler<GetAllClientesQuery, PagedResponse<List<Domain.Entities.Cliente>>>
    {
        private readonly IRepositoryAsync<Domain.Entities.Cliente> _clienteRepository;

        public GetAllClientesQueryHandler(IRepositoryAsync<Domain.Entities.Cliente> clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<PagedResponse<List<Domain.Entities.Cliente>>> Handle(GetAllClientesQuery request, CancellationToken cancellationToken)
        {
            var spec = new PagedClienteSpecification(request.PageSize, request.PageNumber, request.Nombres, request.Identificacion, request.EstadoId);

            var clientes = await _clienteRepository.ListAsync(spec);
            int totalCount = await _clienteRepository.CountAsync(spec);

            return new PagedResponse<List<Domain.Entities.Cliente>>(clientes, request.PageNumber ?? 1, request.PageSize ?? 10, totalCount);
        }
    }
}
