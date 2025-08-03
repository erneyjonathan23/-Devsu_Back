using AutoMapper;
using MediatR;
using OP.Prueba.Application.Interfaces;

namespace OP.Prueba.Application.Features.Cuenta.Commands.CreateCuentaCommand
{
    public class CreateCuentaCommand : IRequest<int>
    {
        public string NumeroCuenta { get; set; }
        public int TipoCuentaId { get; set; }
        public decimal SaldoInicial { get; set; }
        public int ClienteId { get; set; }
    }

    public class CreateCuentaCommandHandler : IRequestHandler<CreateCuentaCommand, int>
    {
        private readonly IRepositoryAsync<Domain.Entities.Cuenta> _repository;
        private readonly IMapper _mapper;

        public CreateCuentaCommandHandler(IRepositoryAsync<Domain.Entities.Cuenta> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateCuentaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var cuenta = _mapper.Map<Domain.Entities.Cuenta>(request);
                await _repository.AddAsync(cuenta);
                return cuenta.CuentaId;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al guardar cambios: {ex.InnerException?.Message ?? ex.Message}", ex);
            }
        }
    }

}