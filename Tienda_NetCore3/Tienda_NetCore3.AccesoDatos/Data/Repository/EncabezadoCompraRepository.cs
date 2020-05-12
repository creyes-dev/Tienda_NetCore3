using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using Tienda_NetCore3.Models;
using System.Linq;

namespace Tienda_NetCore3.AccesoDatos.Data.Repository
{
    public class EncabezadoCompraRepository : Repository<EncabezadoCompra>, IEncabezadoCompraRepository
    {
        private readonly ApplicationDbContext _contexto;

        public EncabezadoCompraRepository(ApplicationDbContext contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public void CambiarEstadoCompra(int idEncabezadoCompra, string estado)
        {
            var encabezadoCompra = _contexto.EncabezadoCompra.FirstOrDefault(o => o.Id == idEncabezadoCompra);
            encabezadoCompra.Estado = estado;
            _contexto.SaveChanges();
        }

    }
}
