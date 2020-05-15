using System;
using System.Collections.Generic;
using System.Text;

namespace Tienda_NetCore3.AccesoDatos.Data.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoriaRepository categoria { get; }
        IFrecuenciaRepository frecuencia { get; }
        IServicioRepository servicio { get; }
        IEncabezadoCompraRepository encabezadoCompra { get; }
        IDetalleCompraRepository detalleCompra { get; }

        IUsuarioRepository usuario { get; }

        ISP_Call SP_Call { get;  }

        void AplicarCambios();
    }
}
