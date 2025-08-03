using Ardalis.Specification;
using AutoMapper;
using MediatR;
using Op.Prueba.Application.DTOs;
using Op.Prueba.Application.Interfaces;
using OP.Prueba.Application.Interfaces;

namespace OP.Prueba.Application.Features.Reportes.Queries
{
    public class GetEstadoCuentaQuery : IRequest<EstadoCuentaResponse>
    {
        public int ClienteId { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public bool ExportarPdf { get; set; } // Si es true, se genera PDF en base64
    }

    public class GetEstadoCuentaQueryHandler : IRequestHandler<GetEstadoCuentaQuery, EstadoCuentaResponse>
    {
        private readonly IRepositoryAsync<Domain.Entities.Cuenta> _cuentaRepo;
        private readonly IRepositoryAsync<Domain.Entities.Movimiento> _movimientoRepo;
        private readonly IRepositoryAsync<Domain.Entities.Cliente> _clienteRepo;
        private readonly IReportService _reportService; // Interfaz para PDF
        private readonly IMapper _mapper;

        public GetEstadoCuentaQueryHandler(
            IRepositoryAsync<Domain.Entities.Cuenta> cuentaRepo,
            IRepositoryAsync<Domain.Entities.Movimiento> movimientoRepo,
            IRepositoryAsync<Domain.Entities.Cliente> clienteRepo,
            IReportService reportService,
            IMapper mapper)
        {
            _cuentaRepo = cuentaRepo;
            _movimientoRepo = movimientoRepo;
            _clienteRepo = clienteRepo;
            _reportService = reportService;
            _mapper = mapper;
        }

        public async Task<EstadoCuentaResponse> Handle(GetEstadoCuentaQuery request, CancellationToken cancellationToken)
        {
            var cliente = await _clienteRepo.GetByIdAsync(request.ClienteId);
            if (cliente == null) throw new Exception("Cliente no encontrado");

            var cuentas = await _cuentaRepo.ListAsync(new CuentaByClienteSpecification(cliente.PersonaId));
            var cuentasDto = new List<CuentaEstadoDto>();

            foreach (var cuenta in cuentas)
            {
                var movimientos = await _movimientoRepo.ListAsync(new MovimientosPorCuentaYFechaSpecification(cuenta.CuentaId, request.FechaInicio, request.FechaFin));
                var movimientosDto = movimientos.Select(m => new MovimientoDto
                {
                    Fecha = m.Fecha,
                    TipoMovimiento = m.TipoMovimiento.Descripcion,
                    Valor = m.Valor,
                    Saldo = m.Saldo
                }).ToList();

                var cuentaDto = new CuentaEstadoDto
                {
                    NumeroCuenta = cuenta.NumeroCuenta,
                    SaldoInicial = movimientosDto.OrderBy(m => m.Fecha).FirstOrDefault()?.Saldo ?? cuenta.SaldoInicial,
                    SaldoFinal = movimientosDto.OrderByDescending(m => m.Fecha).FirstOrDefault()?.Saldo ?? cuenta.SaldoInicial,
                    Movimientos = movimientosDto
                };

                cuentasDto.Add(cuentaDto);
            }

            var response = new EstadoCuentaResponse
            {
                Cliente = cliente.Nombres,
                Cuentas = cuentasDto
            };

            if (request.ExportarPdf)
            {
                var pdfBytes = _reportService.GenerarPdfEstadoCuenta(response);
                response.PdfBase64 = Convert.ToBase64String(pdfBytes);
            }

            return response;
        }
    }

    public class CuentaByClienteSpecification : Specification<Domain.Entities.Cuenta>
    {
        public CuentaByClienteSpecification(int clienteId)
        {
            Query.Where(c => c.ClienteId == clienteId);
        }
    }

    public class MovimientosPorCuentaYFechaSpecification : Specification<Domain.Entities.Movimiento>
    {
        public MovimientosPorCuentaYFechaSpecification(int cuentaId, DateTime inicio, DateTime fin)
        {
            Query.Where(m => m.CuentaId == cuentaId && m.Fecha >= inicio && m.Fecha <= fin)
                .Include(m => m.TipoMovimiento);
        }
    }
}
