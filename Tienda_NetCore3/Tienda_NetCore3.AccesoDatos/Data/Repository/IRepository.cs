using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Tienda_NetCore3.AccesoDatos.Data.Repository
{
    public interface IRepository<T> where T : class
    {
        T Get(int id);

        IEnumerable<T> GetAll(
            Expression<Func<T,bool>> filtro = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> ordenarPor = null,
            string incluirPropiedades = null
            );

        T GetFirstOrDefault(
            Expression<Func<T, bool>> filtro = null,
            string incluirPropiedades = null
            );

        void Add(T entidad);

        void Remove(int id);

        void Remove(T entidad);
    }
}
