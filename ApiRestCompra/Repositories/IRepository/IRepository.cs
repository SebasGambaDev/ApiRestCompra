using System;
using System.Collections.Generic;

namespace ApiRestCompra.Repositories.IRepository
{
    public interface IRepository<T> where T: class
    {
        T Obtener(int id);

        IEnumerable<T> ObtenerTodos();

        void Agregar(T entidad);

        void Remover(int id);

        
 
    }
}
