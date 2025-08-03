using AutoMapper;
using MediatR;
using OP.Prueba.Application.Interfaces;
using OP.Prueba.Application.Specifications;
using OP.Prueba.Application.Wrappers;

namespace OP.Prueba.Application.Features.Cuenta.Queries.GetAllCuentasQuery
{
    public class GetAllCuentasQuery : IRequest<Response<IEnumerable<Domain.Entities.Cuenta>>>
    {
        public int? PageSize { get; set; }
        public int? PageNumber { get; set; }
        public string? NumeroCuenta { get; set; }
        public int? TipoCuentaId { get; set; }
        public int? EstadoId { get; set; }
    }

    public class GetAllCuentasQueryHandler : IRequestHandler<GetAllCuentasQuery, Response<IEnumerable<Domain.Entities.Cuenta>>>
    {
        private readonly IRepositoryAsync<Domain.Entities.Cuenta> _repository;
        private readonly IMapper _mapper;

        public GetAllCuentasQueryHandler(IRepositoryAsync<Domain.Entities.Cuenta> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<Domain.Entities.Cuenta>>> Handle(GetAllCuentasQuery request, CancellationToken cancellationToken)
        {
            var spec = new PagedCuentaSpecification(request.PageSize, request.PageNumber, request.NumeroCuenta, request.TipoCuentaId, request.EstadoId);
            var cuentas = await _repository.ListAsync(spec);

            return new Response<IEnumerable<Domain.Entities.Cuenta>>(cuentas);
        }
    }
}