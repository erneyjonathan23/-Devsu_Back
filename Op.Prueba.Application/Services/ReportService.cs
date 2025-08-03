using Op.Prueba.Application.DTOs;
using Op.Prueba.Application.Interfaces;
using System.Text;

namespace Op.Prueba.Application.Services
{
    public class ReportService : IReportService
    {
        public byte[] GenerarPdfEstadoCuenta(EstadoCuentaResponse data)
        {
            var contenido = $"Estado de cuenta para {data.Cliente} con {data.Cuentas.Count} cuentas.";
            return Encoding.UTF8.GetBytes(contenido);
        }
    }

}
