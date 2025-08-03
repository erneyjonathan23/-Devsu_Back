using OP.Prueba.Domain.Common;

namespace OP.Prueba.Domain.Entities
{
    public class Movimiento
    {
        public int MovimientoId { get; set; }
        public DateTime Fecha { get; set; }

        public int CuentaId { get; set; }
        public Cuenta? Cuenta { get; set; } = null;

        public int TipoMovimientoId { get; set; }
        public TipoGeneral? TipoMovimiento { get; set; } = null;

        public decimal Valor { get; set; }
        public decimal Saldo { get; set; }
    }
}