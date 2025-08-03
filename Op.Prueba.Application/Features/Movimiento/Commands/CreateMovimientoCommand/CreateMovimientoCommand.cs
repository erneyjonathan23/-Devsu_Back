using MediatR;
using Op.Prueba.Application.Enums;
using OP.Prueba.Application.Interfaces;

namespace OP.Prueba.Application.Features.Movimiento.Commands.CreateMovimientoCommand
{
    public class CreateMovimientoCommand : IRequest<int>
    {
        public int TipoMovimientoId { get; set; }
        public int CuentaId { get; set; }
        public decimal Valor { get; set; }
    }

    public class CreateMovimientoCommandHandler : IRequestHandler<CreateMovimientoCommand, int>
    {
        private readonly IRepositoryAsync<Domain.Entities.Movimiento> _movimientoRepo;
        private readonly IRepositoryAsync<Domain.Entities.Cuenta> _cuentaRepo;
        private readonly IDateTimeService _dateTimeService;

        public CreateMovimientoCommandHandler(IRepositoryAsync<Domain.Entities.Movimiento> movimientoRepo, IRepositoryAsync<Domain.Entities.Cuenta> cuentaRepo, IDateTimeService dateTimeService)
        {
            _movimientoRepo = movimientoRepo;
            _cuentaRepo = cuentaRepo;
            _dateTimeService = dateTimeService;
        }

        public async Task<int> Handle(CreateMovimientoCommand request, CancellationToken cancellationToken)
        {
            var cuenta = await _cuentaRepo.GetByIdAsync(request.CuentaId);
            if (cuenta == null)
                throw new KeyNotFoundException("Cuenta no encontrada");

            var saldoDisponible = cuenta.SaldoInicial;

            // Validación de saldo disponible
            if (request.TipoMovimientoId == (int)TipoMovimientoEnum.Retiro && saldoDisponible < request.Valor)
                throw new Exception("Saldo no disponible");

            // Validación de cupo diario
            var retirosHoy = await _movimientoRepo.ListAsync(new MovimientosDelDiaSpecification(request.CuentaId, _dateTimeService.NowUtc));
            var totalRetiros = retirosHoy
                .Where(m => m.TipoMovimientoId == (int)TipoMovimientoEnum.Retiro)
                .Sum(m => Math.Abs(m.Valor));


            const decimal LIMITE_DIARIO = 1000;
            if (request.TipoMovimientoId == (int)TipoMovimientoEnum.Retiro && (totalRetiros + request.Valor) > LIMITE_DIARIO)
                throw new Exception("Cupo diario excedido");

            // Actualización de saldo
            var nuevoSaldo = request.TipoMovimientoId == (int)TipoMovimientoEnum.Retiro ? saldoDisponible - request.Valor : saldoDisponible + request.Valor;
            cuenta.SaldoInicial = nuevoSaldo;

            await _cuentaRepo.UpdateAsync(cuenta);

            var movimiento = new Domain.Entities.Movimiento
            {
                CuentaId = request.CuentaId,
                Fecha = _dateTimeService.NowUtc,
                TipoMovimientoId = request.TipoMovimientoId,
                Valor = request.TipoMovimientoId == (int)TipoMovimientoEnum.Retiro ? -request.Valor : request.Valor,
                Saldo = nuevoSaldo
            };

            await _movimientoRepo.AddAsync(movimiento);
            return movimiento.MovimientoId;
        }
    }

}
