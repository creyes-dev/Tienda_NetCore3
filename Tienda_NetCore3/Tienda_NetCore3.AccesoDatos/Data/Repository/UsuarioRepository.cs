using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Tienda_NetCore3.Models;

namespace Tienda_NetCore3.AccesoDatos.Data.Repository
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        private readonly ApplicationDbContext _contexto;

        public UsuarioRepository(ApplicationDbContext contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public void BloquearUsuario(string idUsuario)
        {
            var usuario = _contexto.Usuario.FirstOrDefault(u => u.Id == idUsuario);
            usuario.LockoutEnd = DateTime.Now.AddYears(1000);
            _contexto.SaveChanges();
        }
        public void DesbloquearUsuario(string idUsuario)
        {
            var usuario = _contexto.Usuario.FirstOrDefault(u => u.Id == idUsuario);
            usuario.LockoutEnd = DateTime.Now;
            _contexto.SaveChanges();
        }

    }
}

