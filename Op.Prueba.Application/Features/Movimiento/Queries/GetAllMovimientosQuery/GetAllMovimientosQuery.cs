using AutoMapper;
using MediatR;
using OP.Prueba.Application.Interfaces;
using OP.Prueba.Application.Specifications;
using OP.Prueba.Application.Wrappers;

namespace OP.Prueba.Application.Features.Movimiento.Queries.GetAllMovimientosQuery
{
    public class GetAllMovimientosQuery : IRequest<PagedResponse<IEnumerable<Domain.Entities.Movimiento>>>
    {
        public int? PageSize { get; set; }
        public int? PageNumber { get; set; }
        public DateTime? FechaDesde { get; set; }
        public DateTime? FechaHasta { get; set; }
        public int? TipoMovimientoId { get; set; }
    }

    public class GetAllMovimientosQueryHandler : IRequestHandler<GetAllMovimientosQuery, PagedResponse<IEnumerable<Domain.Entities.Movimiento>>>
    {
        private readonly IRepositoryAsync<Domain.Entities.Movimiento> _repository;
        private readonly IMapper _mapper;

        public GetAllMovimientosQueryHandler(IRepositoryAsync<Domain.Entities.Movimiento> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<Domain.Entities.Movimiento>>> Handle(GetAllMovimientosQuery request, CancellationToken cancellationToken)
        {
            var specification = new PagedMovimientoSpecification(
                request.PageSize,
                request.PageNumber,
                request.FechaDesde,
                request.FechaHasta,
                request.TipoMovimientoId
            );

            var movimientos = await _repository.ListAsync(specification, cancellationToken);
            var totalCount = await _repository.CountAsync(specification, cancellationToken);

            var movimientosDto = _mapper.Map<IEnumerable<Domain.Entities.Movimiento>>(movimientos);

            return new PagedResponse<IEnumerable<Domain.Entities.Movimiento>>(movimientosDto, request.PageNumber ?? 1, request.PageSize ?? totalCount, totalCount);
        }
    }
}
