using LeitorDeNotas.ClearArch.Application.Interfaces;
using LeitorDeNotas.ClearArch.Domain.Interfaces;
using LeitorDeNotas.ClearArch.IoC;
using Microsoft.AspNetCore.Mvc;



namespace LeitorDeNotas.ClearArch.NotasApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotaFiscalController : BaseController
    {
        private readonly INotaFiscalXmlParser _xmlParser;
        private readonly INotaFiscalRepository _notaFiscalRepository;
        public NotaFiscalController(INotaFiscalXmlParser xmlParser, INotaFiscalRepository notaFiscalRepository)
        {
            _xmlParser = xmlParser;
            _notaFiscalRepository = notaFiscalRepository;
        }

        [HttpPost]
        public IActionResult Index([FromBody] string xml)
        {
            return Created();
        }
    }
}
