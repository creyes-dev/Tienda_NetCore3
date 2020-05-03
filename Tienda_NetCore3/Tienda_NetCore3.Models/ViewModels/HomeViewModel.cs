using System;
using System.Collections.Generic;
using System.Text;

namespace Tienda_NetCore3.Models.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Categoria> ListadoCategorias { get; set; }
        public IEnumerable<Servicio> ListadoServicios { get; set; }
    }
}
