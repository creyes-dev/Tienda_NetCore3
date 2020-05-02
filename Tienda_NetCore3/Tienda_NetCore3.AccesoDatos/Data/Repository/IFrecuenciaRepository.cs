using System;
using System.Collections.Generic;
using System.Text;
using Tienda_NetCore3.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Tienda_NetCore3.AccesoDatos.Data.Repository
{
    public interface IFrecuenciaRepository : IRepository<Frecuencia>
    {
        IEnumerable<SelectListItem> ObtenerListadoFrecuenciasParaDropDown();

        void Update(Frecuencia frecuencia);
    }
}
