using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using Tienda_NetCore3.Models;
using System.Linq;

namespace Tienda_NetCore3.AccesoDatos.Data.Repository
{
    public class DetalleCompraRepository : Repository<DetalleCompra>, IDetalleCompraRepository
    {
        private readonly ApplicationDbContext _contexto;

        public DetalleCompraRepository(ApplicationDbContext contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public IEnumerable<SelectListItem> ObtenerListadoCategoriasParaDropDown()
        {
            return _contexto.Categoria.Select(i => new SelectListItem()
            {
                Text = i.Nombre,
                Value = i.Id.ToString()
            });
        }

        public void Update(Categoria categoria)
        {
            var objeto = _contexto.Categoria.FirstOrDefault(s => s.Id == categoria.Id);
            objeto.Nombre = categoria.Nombre;
            objeto.Orden = categoria.Orden;

            _contexto.SaveChanges();
        }
    }
}
