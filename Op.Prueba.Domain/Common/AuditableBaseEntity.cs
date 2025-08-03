namespace OP.Prueba.Domain.Common
{
    public abstract class AuditableBaseEntity
    {
        public DateTime? FechaRegistro { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }
}
