namespace OP.Prueba.Domain.Entities
{
    public class Cuenta
    {
        public int CuentaId { get; set; }
        public string NumeroCuenta { get; set; }

        public decimal SaldoInicial { get; set; }

        public int ClienteId { get; set; }
        public Cliente? Cliente { get; set; } = null;

        public int TipoCuentaId { get; set; }
        public TipoGeneral? TipoCuenta { get; set; } = null;

        public int EstadoId { get; set; } = 3;
        public TipoGeneral? Estado { get; set; } = null;
    }
}