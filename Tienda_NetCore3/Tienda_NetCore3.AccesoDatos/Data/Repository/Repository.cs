using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Tienda_NetCore3.AccesoDatos.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext Contexto;
        internal DbSet<T> dbSet;

        public Repository(DbContext contexto)
        {
            Contexto = contexto;
            dbSet = contexto.Set<T>();
        }

        public void Add(T entidad)
        {
            dbSet.Add(entidad);
        }

        public T Get(int id)
        {
            return dbSet.Find(id);
        }
        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filtro = null, Func<IQueryable<T>, IOrderedQueryable<T>> ordenarPor = null, string incluirPropiedades = null)
        {
            IQueryable<T> consulta = dbSet;

            if(filtro != null)
            {
                consulta = consulta.Where(filtro);
            } 

            // Las propiedades a incluir estarán separadas por comas
            if(incluirPropiedades != null)
            {
                foreach (var incluirPropiedad in incluirPropiedades.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)) {
                    consulta = consulta.Include(incluirPropiedad);
                } 
            }

            if(ordenarPor != null)
            {
                return ordenarPor(consulta).ToList();
            }
            return consulta.ToList(); 
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filtro = null, string incluirPropiedades = null)
        {
            IQueryable<T> consulta = dbSet;

            if (filtro != null)
            {
                consulta = consulta.Where(filtro);
            }

            // Las propiedades a incluir estarán separadas por comas
            if (incluirPropiedades != null)
            {
                foreach (var incluirPropiedad in incluirPropiedades.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    consulta.Include(incluirPropiedad);
                }
            }

            return consulta.FirstOrDefault();
        }

        public void Remove(int id)
        {
            T entidad = dbSet.Find(id);
            Remove(entidad);
        }

        public void Remove(T entidad)
        {
            dbSet.Remove(entidad);
        }
    }
}
