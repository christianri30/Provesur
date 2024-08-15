using Provesur.Models.Helpers;
using System.ComponentModel;

namespace Provesur.Models.Global
{
    public class SocioNegocio : Base
    {
        public string SocioNegocioId { get; set; } = string.Empty;
        public int EmpresaId { get; set; }
        public int LocalId { get; set; }
        public int TipoDocumentoId { get; set; }
        public string NumeroDocumento { get; set; }
        public string RazonSocial { get; set; } 
        public string NombreComercial { get; set; } = string.Empty;
        public string TipoSocioNegocio { get; set; }
        public string Pais { get; set; } = string.Empty;
        public string Ciudad { get; set; } = string.Empty;
        public int PaisId { get; set; }
        public string CodDepartamento { get; set; } = string.Empty;
        public string CodProvincia { get; set; } = string.Empty;
        public string CodDistrito { get; set; } = string.Empty;
        public string Web { get; set; } = string.Empty;
        public string Mail { get; set; } = string.Empty;
        public int EstadoId { get; set; }
        public List<SocioNegocioContacto> contactos { get; set; }
        public List<SocioNegocioLocal> locales { get; set; }
        public List<SocioNegocioCuentaBancaria> cuentasBancarias { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public string origenPost { get; set; }
    }
    public class SocioNegocioContacto : Base
    {
        [Description]
        public int ContactoId { get; set; } = 0;
        [Description]
        public string SocioNegocioId { get; set; } = string.Empty;
        [Description]
        public string Contacto { get; set; }
        [Description]
        public string Cargo { get; set; }
        [Description]
        public string Telefono { get; set; }
        [Description]
        public string Mail { get; set; } = string.Empty;
    }
    public class SocioNegocioLocal : Base
    {
        [Description]
        public int LocalId { get; set; } = 0;
        [Description]
        public string SocioNegocioId { get; set; } = string.Empty;
        [Description]
        public string Direccion { get; set; }
        [Description]
        public bool Principal { get; set; }
        [Description]
        public string Telefono { get; set; } = string.Empty;
    }
    public class SocioNegocioCuentaBancaria : Base
    {
        [Description]
        public int CuentaBancariaId { get; set; } = 0;
        [Description]
        public string SocioNegocioId { get; set; } = string.Empty;
        [Description]
        public string Descripcion { get; set; }
        [Description]
        public int BancoId { get; set; }
        [Description]
        public int MonedaId { get; set; }
        [Description]
        public string NumeroCuenta { get; set; } = string.Empty;
        [Description]
        public string NumeroCuentaInterbancaria { get; set; } = string.Empty;
        [Description]
        public string NumeroCuentaDetraccion { get; set; } = string.Empty;
    }
}
