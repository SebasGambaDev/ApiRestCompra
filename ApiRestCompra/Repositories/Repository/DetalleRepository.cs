using System;
using System.Collections.Generic;
using System.Linq;
using ApiRestCompra.Context;
using ApiRestCompra.Models;
using ApiRestCompra.Repositories.IRepository;

namespace ApiRestCompra.Repositories.Repository
{
    public class DetalleRepository : Repository<Detalle>, IDetalleRespository
    {
        private readonly AppDbContext _db;

        public DetalleRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public void Actualizar(int id, Detalle detalle)
        {
            var DetalleDb = _db.Detalles.FirstOrDefault(x => x.Id == id);
            if (DetalleDb != null)
            {
                DetalleDb.CodigoReferencia = detalle.CodigoReferencia;
                DetalleDb.Cantidad = detalle.Cantidad;
                DetalleDb.ValorUnitario = detalle.ValorUnitario;
                DetalleDb.Descripcion = detalle.Descripcion;
                DetalleDb.referencia = detalle.referencia;
                DetalleDb.ValorTotal = detalle.ValorTotal;
            }
        }

        public ICollection<Detalle> ObtenerRelacionMuchos(int id)
        {
            return _db.Detalles.Where(x => x.CompraId == id).ToList();
        }
    }
}
