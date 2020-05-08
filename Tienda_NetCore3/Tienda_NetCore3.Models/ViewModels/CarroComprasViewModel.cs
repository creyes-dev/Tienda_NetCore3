using System;
using System.Collections.Generic;
using System.Text;

namespace Tienda_NetCore3.Models.ViewModels
{
    public class CarroComprasViewModel
    {
        public IList<Servicio> ListadoServicios { get; set; }
        public EncabezadoCompra EncabezadoCompra { get; set; }

    }
}
