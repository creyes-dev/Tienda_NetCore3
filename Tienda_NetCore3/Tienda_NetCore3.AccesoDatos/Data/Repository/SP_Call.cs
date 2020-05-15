using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Tienda_NetCore3.AccesoDatos.Data.Repository
{
    public class SP_Call : ISP_Call
    {
        private readonly ApplicationDbContext _db;
        private static string ConnectionString = "";

        public SP_Call(ApplicationDbContext db)
        {
            _db = db;
            ConnectionString = db.Database.GetDbConnection().ConnectionString;
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public IEnumerable<T> DevolverListado<T>(string nombreSP, DynamicParameters param = null)
        {
            using (SqlConnection sqlCon = new SqlConnection(ConnectionString))
            {
                sqlCon.Open();
                return sqlCon.Query<T>(nombreSP, param, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public void Ejecutar(string nombreSP, DynamicParameters param = null)
        {
            using (SqlConnection sqlCon = new SqlConnection(ConnectionString))
            {
                sqlCon.Open();
                sqlCon.Execute(nombreSP, param, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public T EjecutarEscalar<T>(string nombreSP, DynamicParameters param = null)
        {
            using (SqlConnection sqlCon = new SqlConnection(ConnectionString))
            {
                sqlCon.Open();
                return (T) Convert.ChangeType(sqlCon.ExecuteScalar<T>(nombreSP, param, commandType: System.Data.CommandType.StoredProcedure), typeof(T));
            }
        }
    }
}
