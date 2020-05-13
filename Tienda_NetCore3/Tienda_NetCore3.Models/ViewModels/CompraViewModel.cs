using System;
using System.Collections.Generic;
using System.Text;

namespace Tienda_NetCore3.Models.ViewModels
{
    public class CompraViewModel
    {
        public EncabezadoCompra EncabezadoCompra { get; set; }
        public IEnumerable<DetalleCompra> DetallesCompra { get; set; }
    }
}
