using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Tienda_NetCore3.Models.ViewModels
{
    public class ServicioViewModel
    {
        public Servicio Servicio { get; set; }
        public IEnumerable<SelectListItem> ListadoCategorias { get; set; }
        public IEnumerable<SelectListItem> ListadoFrecuencias { get; set; }


    }
}
