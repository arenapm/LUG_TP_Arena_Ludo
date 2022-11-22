using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    internal class Acceso
    {
        private SqlConnection cn;

        public void Abrir()
        {
            cn = new SqlConnection("Integrated Security=SSPI;Initial Catalog=LUDO;Data Source=.");
            cn.Open();
        }

        public void Cerrar()
        {
            cn.Close();
            cn = null;
            GC.Collect();
        }

        private SqlCommand CrearComando(string sql)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cn;
            return cmd;
        }

        public int LeerEscalar(string sql)
        {
            int res;
            SqlCommand cmd = CrearComando(sql);
            try
            {
                res = int.Parse(cmd.ExecuteScalar().ToString());
            }
            catch
            {
                res = -1;
            }
            return res;
        }

        public int Escribir(string sql)
        {
            int res;
            SqlCommand cmd = CrearComando(sql);
            try
            {
                res = cmd.ExecuteNonQuery();
            }
            catch
            {
                res = -1;
            }
            return res;
        }

        public DataTable Leer(string sql)
        {
            DataTable tabla = new DataTable();
            using (SqlDataAdapter da = new SqlDataAdapter())
            {
                da.SelectCommand = CrearComando(sql);
                da.Fill(tabla);
                da.Dispose();
            }
            return tabla;
        }
    }
}
