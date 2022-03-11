using System;
namespace ApiRestCompra.Repositories.IRepository
{
    public interface IUnidadTrabajo : IDisposable
    {
        ICompraRespository Compra { get; }
        IDetalleRespository Detalle { get; }

        void Guardar();
    }
}
