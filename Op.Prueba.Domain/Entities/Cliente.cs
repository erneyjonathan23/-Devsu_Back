namespace OP.Prueba.Domain.Entities
{
    public class Cliente : Persona
    {
        public string Contrasena { get; set; }

        public int EstadoId { get; set; } = 3;
        public TipoGeneral Estado { get; set; }
    }
}