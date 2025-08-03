using AutoMapper;
using MediatR;
using OP.Prueba.Application.Interfaces;

namespace OP.Prueba.Application.Features.Cuenta.Commands.UpdateCuentaCommand
{
    public class UpdateCuentaCommand : IRequest<int>
    {
        public int CuentaId { get; set; }
        public string NumeroCuenta { get; set; }
        public int TipoCuentaId { get; set; }
        public decimal SaldoInicial { get; set; }
        public int EstadoId { get; set; }
    }

    public class UpdateCuentaCommandHandler : IRequestHandler<UpdateCuentaCommand, int>
    {
        private readonly IRepositoryAsync<Domain.Entities.Cuenta> _repository;
        private readonly IMapper _mapper;

        public UpdateCuentaCommandHandler(IRepositoryAsync<Domain.Entities.Cuenta> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> Handle(UpdateCuentaCommand request, CancellationToken cancellationToken)
        {
            var cuenta = await _repository.GetByIdAsync(request.CuentaId);
            if (cuenta == null)
                throw new KeyNotFoundException($"Cuenta con ID {request.CuentaId} no encontrada.");

            _mapper.Map(request, cuenta);
            await _repository.UpdateAsync(cuenta);
            return cuenta.CuentaId;
        }
    }
}