﻿using System;
using ApiRestCompra.Models;

namespace ApiRestCompra.Repositories.IRepository
{
    public interface ICompraRespository : IRepository<Compra>
    {
        void Actualizar(int id, Compra compra);
    }
}
