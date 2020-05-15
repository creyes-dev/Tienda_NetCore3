using System;
using System.Collections.Generic;
using System.Text;
using Dapper;

namespace Tienda_NetCore3.AccesoDatos.Data.Repository
{
    public interface ISP_Call : IDisposable
    {
        IEnumerable<T> DevolverListado<T>(string nombreSP, DynamicParameters param = null);
        void Ejecutar(string nombreSP, DynamicParameters param = null);
        T EjecutarEscalar<T>(string nombreSP, DynamicParameters param = null);
    }
}
