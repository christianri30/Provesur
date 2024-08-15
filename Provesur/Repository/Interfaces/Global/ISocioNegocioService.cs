using Provesur.Models.Global;
using Provesur.Models.Response;

namespace Provesur.Repository.Interfaces.Global
{
    public interface ISocioNegocioService
    {
        Task<Respuesta> List(char t);
        Task<Respuesta> ListID(string id);
        Task<Respuesta> Merge(SocioNegocio obj);
        Task<Respuesta> Delete(string id);
    }
}
