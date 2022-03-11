using System;
using ApiRestCompra.Context;
using ApiRestCompra.Repositories.IRepository;

namespace ApiRestCompra.Repositories.Repository
{
    public class UnidadTrabajo : IUnidadTrabajo
    {
        private readonly AppDbContext _db;
        public ICompraRespository Compra { get; private set; }
        public IDetalleRespository Detalle { get; private set; }

        public UnidadTrabajo(AppDbContext db)
        {
            _db = db;
            Compra = new CompraRepository(_db);
            Detalle = new DetalleRepository(_db);
        }

        public void Guardar()
        {
            _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        decimal IUnidadTrabajo.CalcularIva(decimal? valor)
        {
            const int IvaPorcentaje = 19;

            var nuevoValor = (IvaPorcentaje * valor) / 100;

            var Impuesto = Math.Round((decimal)nuevoValor, 2);

            return Impuesto;
        }

        decimal IUnidadTrabajo.CalcularPrecioDetalle(int cantidad, decimal valor)
        {
            var result = cantidad * valor;
            return result;
            
        }


    }
}
