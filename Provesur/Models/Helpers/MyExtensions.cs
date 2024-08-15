using System.Data;

namespace Provesur.Models.Helpers
{
    public static class MyExtensions
    {
        public static DataTable ToDataTable<T>(this List<T> list)
        {
            DataTable dataTable = new DataTable();

            List<System.Reflection.PropertyInfo> properties = typeof(T).GetProperties()
                .Where(x => x.CustomAttributes.Any(y => y.AttributeType.Name == "DescriptionAttribute"))
                .ToList();

            DataColumn[] columns = properties.Select(x => new DataColumn(x.Name)).ToArray();
            dataTable.Columns.AddRange(columns);

            if (list.Any())
            {
                for (int i = 0; i < list.Count; i++)
                {
                    object[] values = properties.Select(x => x.GetValue(list[i])).ToArray();
                    dataTable.Rows.Add(values);
                }
            }
            return dataTable;
        }
        
        public static int ToInt(this object obj)
        {
            int entero = 0;
            int.TryParse(obj.ToString(), out entero);
            return entero;
        }
       
        public static decimal ToDecimal(this object obj)
        {
            decimal numero = 0;
            decimal.TryParse(obj.ToString(), out numero);
            return numero;
        }
        
        public static DateTime ToDateTime(this object obj)
        {
            DateTime fecha = new DateTime();
            DateTime.TryParse(obj.ToString(), out fecha);
            if (fecha == DateTime.MinValue) return Convert.ToDateTime("01-01-2000");
            else return fecha;
        }

        public static object ToDateTimeSQL(object objDate)
        {
            DateTime date = new DateTime();
            DateTime.TryParse(objDate?.ToString(), out date);
            if (date == DateTime.MinValue)
                return DBNull.Value;
            return date;
        }
    }
}
