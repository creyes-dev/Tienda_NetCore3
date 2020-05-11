using System;
using System.Collections.Generic;
using System.Text;
using Tienda_NetCore3.Models;

namespace Tienda_NetCore3.AccesoDatos.Data.Repository
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        void BloquearUsuario(string idUsuario);
        void DesbloquearUsuario(string idUsuario);
    }
}
