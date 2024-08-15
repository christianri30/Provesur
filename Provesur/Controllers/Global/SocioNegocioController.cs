using Microsoft.AspNetCore.Mvc;
using Provesur.Models.Global;
using Provesur.Models.Response;
using Provesur.Repository.Interfaces.Global;

namespace Provesur.Controllers.Global
{
    public class SocioNegocioController : Controller
    {
        public readonly ISocioNegocioService _socioNegocioService;
        public SocioNegocioController(ISocioNegocioService socioNegocioService)
        {
            _socioNegocioService = socioNegocioService;
        }
        public async Task<IActionResult> GetAll([FromQuery] char t)
        {
            Respuesta _lista = await _socioNegocioService.List(t);
            return StatusCode(StatusCodes.Status200OK, _lista);
        }

        [HttpGet]
        public async Task<IActionResult> GetId(string id)
        {
            Respuesta _obj = await _socioNegocioService.ListID(id);
            return StatusCode(StatusCodes.Status200OK, _obj);
        }
        [HttpPost]
        public async Task<Respuesta> Post([FromBody] SocioNegocio obj)
        {
            //Merge aqui se valida
            if (obj.origenPost == "create")
            {
                Respuesta r = await _socioNegocioService.ListID(obj.Tipo + obj.NumeroDocumento);
                if (((SocioNegocio)r.Objeto).RazonSocial != null)
                {
                    Respuesta rError = new Respuesta();
                    rError.Resultado = false;
                    string tipo = obj.Tipo == "C" ? "Cliente" : "Proveedor";

                    rError.Data = "El " + tipo + " ya se encuentra registrado";
                    return rError;
                }
                else
                {
                    Respuesta rpta = await _socioNegocioService.Merge(obj);
                    return rpta;
                }
            }
            else
            {
                Respuesta rpta = await _socioNegocioService.Merge(obj);
                return rpta;
            }


        }
        [HttpDelete("{id}")]
        public async Task<Respuesta> Delete(string id)
        {
            Respuesta rpta = await _socioNegocioService.Delete(id);
            return rpta;
        }
    }
}
