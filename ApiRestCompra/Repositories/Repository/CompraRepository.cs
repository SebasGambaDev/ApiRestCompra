using System;
using System.Linq;
using ApiRestCompra.Context;
using ApiRestCompra.Models;
using ApiRestCompra.Repositories.IRepository;

namespace ApiRestCompra.Repositories.Repository
{
    public class CompraRepository : Repository<Compra>, ICompraRespository
    {
        private readonly AppDbContext _db;

        public CompraRepository(AppDbContext db ): base(db)
        {
            _db = db;
        }

        public void Actualizar(Compra compra)
        {
            var CompraDb = _db.Compras.FirstOrDefault(x => x.Id == compra.Id);
            if(CompraDb != null)
            {
                CompraDb.ClienteApellido1 = compra.ClienteApellido1;
                CompraDb.ClienteApellido2 = compra.ClienteApellido2;
                CompraDb.ClienteNombre1 = compra.ClienteNombre1;
                CompraDb.ClienteNombre2 = compra.ClienteNombre2;
                CompraDb.ClienteEmail = compra.ClienteEmail;
                CompraDb.ClienteDireccionDespacho = compra.ClienteDireccionDespacho;
                CompraDb.CiudadDespacho = compra.CiudadDespacho;
                CompraDb.ClienteDireccionFacturacion = compra.ClienteDireccionFacturacion;
                CompraDb.CiudadFacturacion = compra.CiudadFacturacion;
                CompraDb.ValorFlete = compra.ValorFlete;
                CompraDb.NumeroFactura = compra.NumeroFactura;
                CompraDb.TotalArticulos = compra.TotalArticulos;
                CompraDb.TotalImpuestosFlete = compra.TotalImpuestosFlete;
                CompraDb.TotalImpuestosNetos = compra.TotalImpuestosNetos;
                CompraDb.TotalImpuestosVenta = compra.TotalImpuestosVenta;
                CompraDb.ValorTotalFactura = compra.ValorTotalFactura;

                
                
            }
            
        }
    }
}
