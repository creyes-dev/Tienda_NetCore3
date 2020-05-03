using System;
using System.Collections.Generic;
using System.Text;

namespace Tienda_NetCore3.AccesoDatos.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _contexto;

        public UnitOfWork(ApplicationDbContext contexto)
        {
            _contexto = contexto;
            categoria = new CategoriaRepository(contexto);
            frecuencia = new FrecuenciaRepository(contexto);
            servicio = new ServicioRepository(contexto);
        }

        public ICategoriaRepository categoria { get; private set; }

        public IFrecuenciaRepository frecuencia { get; private set; }

        public IServicioRepository servicio { get; private set; }

        public void AplicarCambios()
        {
            _contexto.SaveChanges();
        }

        public void Dispose()
        {
            _contexto.Dispose();
        }
    }
}
