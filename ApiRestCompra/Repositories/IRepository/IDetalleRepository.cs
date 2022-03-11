using System;
using System.Collections.Generic;
using ApiRestCompra.Models;

namespace ApiRestCompra.Repositories.IRepository
{
    public interface IDetalleRespository : IRepository<Detalle>
    {
        void Actualizar(int id, Detalle detalle);

        ICollection<Detalle> ObtenerRelacionMuchos(int id);
    }
}