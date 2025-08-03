using Ardalis.Specification;
using OP.Prueba.Domain.Entities;

namespace OP.Prueba.Application.Specifications
{
    public class PagedCuentaSpecification : Specification<Cuenta>
    {
        public PagedCuentaSpecification(int? pageSize, int? pageNumber, string? numeroCuenta, int? tipoCuentaId, int? estadoId)
        {
            Query
                .Include(m => m.Cliente)
                .Include(m => m.TipoCuenta)
                .Include(m => m.Estado);

            if (!string.IsNullOrWhiteSpace(numeroCuenta))
                Query.Where(c => c.NumeroCuenta.Contains(numeroCuenta));

            if (tipoCuentaId.HasValue)
                Query.Where(c => c.TipoCuentaId == tipoCuentaId.Value);

            if (estadoId.HasValue)
                Query.Where(c => c.EstadoId == estadoId.Value);

            if (pageSize != null && pageNumber != null)
                Query.Skip(((int)pageNumber - 1) * (int)pageSize)
                     .Take((int)pageSize);
        }
    }
}