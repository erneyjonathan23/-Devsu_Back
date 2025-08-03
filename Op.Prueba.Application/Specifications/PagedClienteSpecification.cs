using Ardalis.Specification;
using OP.Prueba.Domain.Entities;

namespace OP.Prueba.Application.Specifications
{
    public class PagedClienteSpecification : Specification<Cliente>
    {
        public PagedClienteSpecification(int? pageSize, int? pageNumber, string? nombres, string? identificacion, int? estadoId)
        {
            Query
                .Include(m => m.Genero)
                .Include(m => m.Estado);

            if (!string.IsNullOrWhiteSpace(nombres))
                Query.Where(c => c.Nombres.Contains(nombres));

            if (!string.IsNullOrWhiteSpace(identificacion))
                Query.Where(c => c.Identificacion.Contains(identificacion));

            if (estadoId.HasValue)
                Query.Where(c => c.EstadoId == estadoId.Value);

            if (pageSize != null && pageNumber != null)
                Query.Skip(((int)pageNumber - 1) * (int)pageSize)
                    .Take((int)pageSize);
        }
    }
}