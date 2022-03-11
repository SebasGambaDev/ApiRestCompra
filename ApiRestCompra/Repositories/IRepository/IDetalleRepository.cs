using System;
using System.Collections.Generic;
using ApiRestCompra.Models;

namespace ApiRestCompra.Repositories.IRepository
{
    public interface IDetalleRespository : IRepository<Detalle>
    {
        public void Actualizar(Detalle detalle);

        ICollection<Detalle> ObtenerRelacionMuchos(int id);

        public void RemoverRelacionados(int id);
    }
}