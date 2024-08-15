using Provesur.Models.Global;
using Provesur.Models.Response;

namespace Provesur.Repository.Interfaces.Global
{
    public interface IGlobalMaestroService
    {
        Task<Respuesta> List(Maestro obj);
    }
}
