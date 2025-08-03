using AutoMapper;
using MediatR;
using OP.Prueba.Application.Interfaces;

namespace OP.Prueba.Application.Features.Movimiento.Queries.GetMovimientoByIdQuery
{
    public class GetMovimientoByIdQuery : IRequest<Domain.Entities.Movimiento>
    {
        public int MovimientoId { get; set; }
    }

    public class GetMovimientoByIdQueryHandler : IRequestHandler<GetMovimientoByIdQuery, Domain.Entities.Movimiento>
    {
        private readonly IRepositoryAsync<Domain.Entities.Movimiento> _repository;
        private readonly IMapper _mapper;

        public GetMovimientoByIdQueryHandler(IRepositoryAsync<Domain.Entities.Movimiento> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Domain.Entities.Movimiento> Handle(GetMovimientoByIdQuery request, CancellationToken cancellationToken)
        {
            var movimiento = await _repository.GetByIdAsync(request.MovimientoId);
            if (movimiento == null)
                throw new KeyNotFoundException($"Movimiento con ID {request.MovimientoId} no encontrado.");

            return movimiento;
        }
    }
}