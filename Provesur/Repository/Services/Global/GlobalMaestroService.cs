using Provesur.Models.Global;
using Provesur.Models.Response;
using System.Data.SqlClient;
using System.Data;
using Provesur.Repository.Interfaces.Global;

namespace Provesur.Repository.Services.Global
{
    public class GlobalMaestroService : IGlobalMaestroService
    {
        private readonly string _conn = "";
        public GlobalMaestroService(IConfiguration configuraton)
        {
            _conn = configuraton.GetConnectionString("DefaultConnection");
        }
        public async Task<Respuesta> List(Maestro obj)
        {
            Respuesta response = new Respuesta();
            List<Maestro> Lista = new List<Maestro>();
            try
            {
                using (SqlConnection cn = new SqlConnection(_conn))
                {
                    cn.Open();
                    SqlCommand cm = new SqlCommand("sp_GlobalMaestro_GetTable", cn);
                    cm.Parameters.AddWithValue("@tabla", obj.Tabla);
                    cm.Parameters.AddWithValue("@campo", obj.Campo);
                    cm.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = await cm.ExecuteReaderAsync();
                    while (await dr.ReadAsync())
                    {
                        Maestro objModel = new Maestro();
                        objModel.MaestroId = dr["MaestroId"].ToString();
                        objModel.Clave = dr["Clave"].ToString();
                        objModel.Valor = dr["Valor"].ToString();
                        objModel.Campo = dr["Campo"].ToString();
                        objModel.Tabla = dr["Tabla"].ToString();
                        objModel.Condicion = dr["Condicion"].ToString();
                        Lista.Add(objModel);
                    }
                }
                response.Resultado = true;
                response.Data = Lista;
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
