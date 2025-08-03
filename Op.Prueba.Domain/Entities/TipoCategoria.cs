using OP.Prueba.Domain.Common;

namespace OP.Prueba.Domain.Entities
{
    public class TipoCategoria
    {
        public int TipoCategoriaId { get; set; }  
        public string Nombre { get; set; }

        public ICollection<TipoGeneral> TiposGenerales { get; set; } = new List<TipoGeneral>();
    }
}