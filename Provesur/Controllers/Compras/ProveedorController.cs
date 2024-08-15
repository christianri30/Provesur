using Microsoft.AspNetCore.Mvc;

namespace Provesur.Controllers.Compras
{
    public class ProveedorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Crear()
        {
            return View();
        }
        public IActionResult Detalle(int id)
        {
            return View();
        }
        public IActionResult Editar(int id)
        {
            return View();
        }
    }
}
