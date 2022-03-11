using System;
using System.Collections.Generic;
using ApiRestCompra.Repositories.IRepository;

namespace ApiRestCompra.Repositories.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public void Agregar(T entidad)
        {
            throw new NotImplementedException();
        }

        public T Obtener(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> ObtenerTodos()
        {
            throw new NotImplementedException();
        }

        public void Remover(int id)
        {
            throw new NotImplementedException();
        }
    }
}
