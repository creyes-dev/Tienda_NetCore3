using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using Tienda_NetCore3.Models;
using System.Linq;

namespace Tienda_NetCore3.AccesoDatos.Data.Repository
{
    public class ServicioRepository : Repository<Servicio>, IServicioRepository
    {
        private readonly ApplicationDbContext _contexto;

        public ServicioRepository(ApplicationDbContext contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public void Update(Servicio servicio)
        {
            var objeto = _contexto.Servicio.FirstOrDefault(s => s.Id == servicio.Id);
            objeto.Nombre = servicio.Nombre;
            objeto.Descripcion = servicio.Descripcion;
            objeto.Precio = servicio.Precio;
            objeto.UrlImagen = servicio.UrlImagen;
            objeto.IdCategoria = servicio.IdCategoria;
            objeto.IdFrecuencia = servicio.IdFrecuencia;

            _contexto.SaveChanges();
        }
    }
}
