namespace Op.Prueba.Application.DTOs
{
    public class MovimientoDto
    {
        public DateTime Fecha { get; set; }
        public string TipoMovimiento { get; set; }
        public decimal Valor { get; set; }
        public decimal Saldo { get; set; }
    }
}
