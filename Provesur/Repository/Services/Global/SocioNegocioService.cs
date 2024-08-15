using Provesur.Models.Global;
using Provesur.Models.Helpers;
using Provesur.Models.Response;
using Provesur.Repository.Interfaces.Global;
using System.Data;
using System.Data.SqlClient;

namespace Provesur.Repository.Services.Global
{
    public class SocioNegocioService : ISocioNegocioService
    {
        private readonly string _conn = "";
        public SocioNegocioService(IConfiguration configuration) {
            _conn = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<Respuesta> List(char t)
        {
            Respuesta response = new Respuesta();
            List<SocioNegocio> Lista = new List<SocioNegocio>();
            try
            {
                using (SqlConnection cn = new SqlConnection(_conn))
                {
                    cn.Open();
                    SqlCommand cm = new SqlCommand("Sp_Ad_SocioNegocio_Get", cn);
                    cm.Parameters.AddWithValue("@TipoSocio", t);
                    cm.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = await cm.ExecuteReaderAsync();
                    while (await dr.ReadAsync())
                    {
                        SocioNegocio objModel = new SocioNegocio();
                        objModel.SocioNegocioId = dr["SocioNegocioId"].ToString();
                        objModel.NumeroDocumento = dr["NumeroDocumento"].ToString();
                        objModel.RazonSocial = dr["RazonSocial"].ToString();
                        objModel.NombreComercial = dr["NombreComercial"].ToString();
                        objModel.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString() == "" ? "01-01-2000" : dr["FechaModificacion"].ToString()).ToString("dd/MM/yyyy");
                        objModel.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"]);
                    } 
                }
            }
            catch (Exception ex)
            {
                response.Resultado = false;
                response.Data = ex.Message;
            }
            return response;
        }
        public async Task<Respuesta> ListID(string id)
        {
            Respuesta response = new Respuesta();
            SocioNegocio objModel = new SocioNegocio();
            List<SocioNegocioContacto> listaContacto = new List<SocioNegocioContacto>();
            List<SocioNegocioLocal> listaLocal = new List<SocioNegocioLocal>();
            List<SocioNegocioCuentaBancaria> listaCuenta = new List<SocioNegocioCuentaBancaria>();
            try
            {
                using(SqlConnection cn = new SqlConnection(_conn))
                {
                    cn.Open();
                    SqlCommand cm = new SqlCommand("Sp_Ad_SocioNegocio_GetRegister", cn);
                    cm.Parameters.AddWithValue("@SocioNegocioId", id);
                    cm.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = await cm.ExecuteReaderAsync();
                    while (await dr.ReadAsync())
                    {
                        objModel.SocioNegocioId = dr["SocioNegocioId"].ToString();
                        objModel.EmpresaId = Convert.ToInt32(dr["EmpresaId"]);
                        objModel.LocalId = Convert.ToInt32(dr["LocalId"]);
                        objModel.TipoDocumentoId = Convert.ToInt32(dr["TipoDocumentoId"]);
                        objModel.NumeroDocumento = dr["NumeroDocumento"].ToString();
                        objModel.RazonSocial = dr["RazonSocial"].ToString();
                        objModel.NombreComercial = dr["NombreComercial"].ToString();
                        objModel.Tipo = dr["Tipo"].ToString();
                        //objModel.Pais = dr["Pais"].ToString();
                        //objModel.Ciudad = dr["Ciudad"].ToString();
                        objModel.PaisId = Convert.ToInt32(dr["PaisId"]);
                        objModel.CodDepartamento = dr["CodDepartamento"].ToString();
                        objModel.CodProvincia = dr["CodProvincia"].ToString();
                        objModel.CodDistrito = dr["CodDistrito"].ToString();
                        objModel.Web = dr["Web"].ToString();
                        objModel.Mail = dr["Mail"].ToString();
                        objModel.EstadoId = Convert.ToInt32(dr["EstadoId"]);
                    }
                    if (dr.NextResult())
                    {
                        while (await dr.ReadAsync())
                        {
                            SocioNegocioContacto objContacto = new SocioNegocioContacto();
                            objContacto.ContactoId = Convert.ToInt32(dr["ContactoId"]);
                            objContacto.Contacto = dr["Contacto"].ToString();
                            objContacto.Cargo = dr["Cargo"].ToString();
                            objContacto.Telefono = dr["Telefono"].ToString();
                            objContacto.Mail = dr["Mail"].ToString();
                            listaContacto.Add(objContacto);
                        }
                        objModel.contactos = listaContacto;
                    }
                    if (dr.NextResult())
                    {
                        while (await dr.ReadAsync())
                        {
                            SocioNegocioLocal objLocal = new SocioNegocioLocal();
                            objLocal.LocalId = Convert.ToInt32(dr["LocalId"]);
                            objLocal.Direccion = dr["Direccion"].ToString();
                            objLocal.Principal = Convert.ToBoolean(dr["Principal"]);
                            objLocal.Telefono = dr["Telefono"].ToString();
                            listaLocal.Add(objLocal);
                        }
                        objModel.locales = listaLocal;
                    }
                    if (dr.NextResult())
                    {
                        while (await dr.ReadAsync())
                        {
                            SocioNegocioCuentaBancaria objCuenta = new SocioNegocioCuentaBancaria();
                            objCuenta.CuentaBancariaId = Convert.ToInt32(dr["CuentaBancariaId"]);
                            objCuenta.Descripcion = dr["Descripcion"].ToString();
                            objCuenta.BancoId = Convert.ToInt32(dr["BancoId"]);
                            objCuenta.MonedaId = Convert.ToInt32(dr["MonedaId"]);
                            objCuenta.NumeroCuenta = dr["NumeroCuenta"].ToString();
                            objCuenta.NumeroCuentaInterbancaria = dr["NumeroCuentaInterbancaria"].ToString();
                            objCuenta.NumeroCuentaDetraccion = dr["NumeroCuentaDetraccion"].ToString();
                            listaCuenta.Add(objCuenta);
                        }
                        objModel.cuentasBancarias = listaCuenta;
                    }
                }
                response.Resultado = true;
                response.Objeto = objModel;
            }
            catch (Exception ex)
            {
                response.Resultado = false;
                response.Data = ex.Message;
            }
            return response;
        }
        public async Task<Respuesta> Merge(SocioNegocio obj)
        {
            Respuesta response = new Respuesta();
            try
            {
                using (SqlConnection cn = new SqlConnection(_conn))
                {
                    cn.Open();
                    SqlTransaction tr = cn.BeginTransaction(IsolationLevel.Serializable);
                    SqlCommand cm = new SqlCommand("Sp_SocioNegocio_InsertUpdate", cn, tr);
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.AddWithValue("@SocioNegocioId", obj.SocioNegocioId);
                    cm.Parameters.AddWithValue("@EmpresaId", obj.EmpresaId);
                    cm.Parameters.AddWithValue("@LocalId", obj.LocalId);
                    cm.Parameters.AddWithValue("@TipoDocumentoId", obj.TipoDocumentoId);
                    cm.Parameters.AddWithValue("@NumeroDocumento", obj.NumeroDocumento);
                    cm.Parameters.AddWithValue("@RazonSocial", obj.RazonSocial);
                    cm.Parameters.AddWithValue("@NombreComercial", obj.NombreComercial);
                    cm.Parameters.AddWithValue("@Tipo", obj.Tipo);
                    cm.Parameters.AddWithValue("@PaisId", obj.PaisId);
                    cm.Parameters.AddWithValue("@CodDepartamento", obj.CodDepartamento);
                    cm.Parameters.AddWithValue("@CodProvincia", obj.CodProvincia);
                    cm.Parameters.AddWithValue("@CodDistrito", obj.CodDistrito);
                    cm.Parameters.AddWithValue("@Web", obj.Web);
                    cm.Parameters.AddWithValue("@Mail", obj.Mail);
                    cm.Parameters.AddWithValue("@UsuarioCreacion", obj.UsuarioCreacion);
                    cm.Parameters.AddWithValue("@EstadoId", obj.EstadoId);

                    DataTable dtContacto = obj.contactos.ToDataTable();
                    SqlParameter tbParamContacto = cm.Parameters.AddWithValue("@TB_DetalleContacto", dtContacto);
                    tbParamContacto.SqlDbType = SqlDbType.Structured;

                    DataTable dtLocal = obj.locales.ToDataTable();
                    SqlParameter tbParamLocal = cm.Parameters.AddWithValue("@TB_DetalleLocal", dtLocal);
                    tbParamLocal.SqlDbType = SqlDbType.Structured;

                    DataTable dtCuentas = obj.cuentasBancarias.ToDataTable();
                    SqlParameter tbParamCuentas = cm.Parameters.AddWithValue("@TB_DetalleCuentasBancarias", dtCuentas);
                    tbParamCuentas.SqlDbType = SqlDbType.Structured;

                    await cm.ExecuteNonQueryAsync();

                    tr.Commit();
                    response.Resultado = true;
                    response.Data = "OK";
                }
            }
            catch (Exception ex)
            {
                response.Resultado = false;
                response.Data = ex.Message;
            }
            return response;
        }
        public async Task<Respuesta> Delete(string id)
        {
            Respuesta response = new Respuesta();
            try
            {
                using (SqlConnection cn = new SqlConnection(_conn))
                {
                    cn.Open();
                    SqlTransaction tr = cn.BeginTransaction(IsolationLevel.Serializable);
                    SqlCommand cm = new SqlCommand("Sp_SocioNegocio_Delete", cn, tr);
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.AddWithValue("@SocioNegocioId", id);
                    await cm.ExecuteNonQueryAsync();

                    tr.Commit();
                    response.Resultado = true;
                    response.Data = "OK";
                }
            }
            catch (Exception ex)
            {
                response.Resultado = false;
                response.Data = ex.Message;
            }
            return response;
        }
    }
}
