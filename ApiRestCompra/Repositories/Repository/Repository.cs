using System;
using System.Collections.Generic;
using System.Linq;
using ApiRestCompra.Context;
using ApiRestCompra.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace ApiRestCompra.Repositories.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _db;
        internal DbSet<T> dbSet;

        public Repository(AppDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }

        public void Agregar(T entidad)
        {
            dbSet.Add(entidad);
        }

        public T Obtener(int id)
        {
            return dbSet.Find(id);
        }

        public IEnumerable<T> ObtenerTodos()
        {
            return dbSet.ToList();
        }

        public void Remover(int id)
        {
            T entidad = dbSet.Find(id);
            dbSet.Remove(entidad);
        }
    }
}
