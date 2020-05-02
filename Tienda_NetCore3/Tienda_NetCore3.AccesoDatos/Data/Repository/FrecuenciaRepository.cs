using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using Tienda_NetCore3.Models;
using System.Linq;

namespace Tienda_NetCore3.AccesoDatos.Data.Repository
{
    public class FrecuenciaRepository : Repository<Frecuencia>, IFrecuenciaRepository
    {
        private readonly ApplicationDbContext _contexto;

        public FrecuenciaRepository(ApplicationDbContext contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public IEnumerable<SelectListItem> ObtenerListadoFrecuenciasParaDropDown()
        {
            return _contexto.Frecuencia.Select(i => new SelectListItem()
            {
                Text = i.Nombre,
                Value = i.Id.ToString()
            });            
        }

        public void Update(Frecuencia frecuencia)
        {
            var objeto = _contexto.Frecuencia.FirstOrDefault(s => s.Id == frecuencia.Id);
            objeto.Nombre = frecuencia.Nombre;
            objeto.Conteo = frecuencia.Conteo;

            _contexto.SaveChanges();            
        }
    }
}
