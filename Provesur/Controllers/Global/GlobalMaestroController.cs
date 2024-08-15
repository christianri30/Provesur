using Microsoft.AspNetCore.Mvc;
using Provesur.Models.Global;
using Provesur.Models.Response;
using Provesur.Repository.Interfaces.Global;

namespace Provesur.Controllers.Global
{
    public class GlobalMaestroController : Controller
    {
        private readonly IGlobalMaestroService _globalMaestroRepository;

        public GlobalMaestroController(IGlobalMaestroService globalMaestroRepository)
        {
            _globalMaestroRepository = globalMaestroRepository;
        }
        [HttpPost]
        public async Task<IActionResult> Get([FromBody] Maestro obj)
        {
            Respuesta _lista = await _globalMaestroRepository.List(obj);
            return StatusCode(StatusCodes.Status200OK, _lista);
        }
    }
}
