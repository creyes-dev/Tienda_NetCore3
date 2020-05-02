using System;
using System.Collections.Generic;
using System.Text;
using Tienda_NetCore3.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Tienda_NetCore3.AccesoDatos.Data.Repository
{
    public interface ICategoriaRepository : IRepository<Categoria>
    {
        IEnumerable<SelectListItem> ObtenerListadoCategoriasParaDropDown();

        void Update(Categoria categoria);
    } 
}
