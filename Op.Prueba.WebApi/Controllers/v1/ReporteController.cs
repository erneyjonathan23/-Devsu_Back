using Microsoft.AspNetCore.Mvc;
using OP.Prueba.Application.Features.Reportes.Queries;
using OP.Prueba.WebAPI.Controllers;

namespace OP.Prueba.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReporteController : BaseApiController
    {
        [HttpGet("EstadoCuenta")]
        public async Task<IActionResult> EstadoCuenta([FromQuery] GetEstadoCuentaQuery query)
        {
            var result = await Mediator.Send(query);

            if (query.ExportarPdf && !string.IsNullOrEmpty(result.PdfBase64))
                return Ok(new { PdfBase64 = result.PdfBase64 });

            return Ok(result);
        }
    }
}