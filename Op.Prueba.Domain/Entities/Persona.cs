namespace OP.Prueba.Domain.Entities
{
    public class Persona 
    {
        public int PersonaId { get; set; }
        public string Nombres { get; set; }
        public int GeneroId { get; set; }
        public TipoGeneral Genero { get; set; }
        public int Edad { get; set; }
        public string Identificacion { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
    }
}