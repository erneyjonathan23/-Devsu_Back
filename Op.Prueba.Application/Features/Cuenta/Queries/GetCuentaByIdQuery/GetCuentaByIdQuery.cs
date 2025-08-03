using AutoMapper;
using MediatR;
using OP.Prueba.Application.Interfaces;

namespace OP.Prueba.Application.Features.Cuenta.Queries.GetCuentaByIdQuery
{
    public class GetCuentaByIdQuery : IRequest<Domain.Entities.Cuenta>
    {
        public int CuentaId { get; set; }
    }

    public class GetCuentaByIdQueryHandler : IRequestHandler<GetCuentaByIdQuery, Domain.Entities.Cuenta>
    {
        private readonly IRepositoryAsync<Domain.Entities.Cuenta> _repository;
        private readonly IMapper _mapper;

        public GetCuentaByIdQueryHandler(IRepositoryAsync<Domain.Entities.Cuenta> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Domain.Entities.Cuenta> Handle(GetCuentaByIdQuery request, CancellationToken cancellationToken)
        {
            var cuenta = await _repository.GetByIdAsync(request.CuentaId);
            if (cuenta == null)
                throw new KeyNotFoundException($"Cuenta con ID {request.CuentaId} no encontrada.");

            return cuenta;
        }
    }
}