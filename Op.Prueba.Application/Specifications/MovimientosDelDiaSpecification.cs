using Ardalis.Specification;
using OP.Prueba.Domain.Entities;

public class MovimientosDelDiaSpecification : Specification<Movimiento>
{
    public MovimientosDelDiaSpecification(int cuentaId, DateTime fecha)
    {
        Query.Where(m => m.CuentaId == cuentaId && m.Fecha.Date == fecha.Date)
            .Include(m => m.TipoMovimiento)
            .Include(m => m.Cuenta);
    }
}