namespace Op.Prueba.Application.DTOs
{
    public class EstadoCuentaResponse
    {
        public string Cliente { get; set; }
        public List<CuentaEstadoDto> Cuentas { get; set; }
        public string? PdfBase64 { get; set; } // Si ExportarPdf = true
    }
}
