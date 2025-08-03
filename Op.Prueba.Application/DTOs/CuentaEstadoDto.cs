namespace Op.Prueba.Application.DTOs
{
    public class CuentaEstadoDto
    {
        public string NumeroCuenta { get; set; }
        public decimal SaldoInicial { get; set; }
        public decimal SaldoFinal { get; set; }
        public List<MovimientoDto> Movimientos { get; set; }
    }
}
