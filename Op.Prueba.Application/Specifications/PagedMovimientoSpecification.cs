using Ardalis.Specification;
using OP.Prueba.Domain.Entities;

namespace OP.Prueba.Application.Specifications
{
    public class PagedMovimientoSpecification : Specification<Movimiento>
    {
        public PagedMovimientoSpecification(int? pageSize, int? pageNumber, DateTime? fechaDesde, DateTime? fechaHasta, int? tipoMovimientoId)
        {
            Query
                .Include(m => m.Cuenta)
                .Include(m => m.TipoMovimiento);

            if (fechaDesde.HasValue)
                Query.Where(m => m.Fecha >= fechaDesde.Value);

            if (fechaHasta.HasValue)
                Query.Where(m => m.Fecha <= fechaHasta.Value);

            if (tipoMovimientoId.HasValue)
                Query.Where(m => m.TipoMovimientoId == tipoMovimientoId.Value);

            if (pageSize != null && pageNumber != null)
                Query.Skip(((int)pageNumber - 1) * (int)pageSize)
                    .Take((int)pageSize);
        }
    }
}
