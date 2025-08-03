using OP.Prueba.Domain.Common;

namespace OP.Prueba.Domain.Entities
{
    public class TipoGeneral 
    {
        public int TipoGeneralId { get; set; }
        public int TipoCategoriaId { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }

        public TipoCategoria TipoCategoria { get; set; }
    }
}