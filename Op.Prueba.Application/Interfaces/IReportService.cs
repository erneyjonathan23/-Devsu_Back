using Op.Prueba.Application.DTOs;

namespace Op.Prueba.Application.Interfaces
{
    public interface IReportService
    {
        byte[] GenerarPdfEstadoCuenta(EstadoCuentaResponse data);
    }
}
