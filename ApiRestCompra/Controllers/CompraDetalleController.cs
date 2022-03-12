using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ApiRestCompra.Models;
using ApiRestCompra.Repositories.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ApiRestCompra.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CompraDetalleController : ControllerBase
    {
        private readonly IUnidadTrabajo _unidadTrabajo;

        private readonly ILogger<CompraDetalleController> _logger;

        public CompraDetalleController(IUnidadTrabajo unidadTrabajo, ILogger<CompraDetalleController> logger)
        {
            _unidadTrabajo = unidadTrabajo;
            _logger = logger;
        }


        private UserModel GetCurrentUser()
        {
            _logger.LogWarning("Llamar usuario autenticado o autorizado");

            var identidad = HttpContext.User.Identity as ClaimsIdentity;
            if(identidad != null)
            {
                var userClaims = identidad.Claims;

                return new UserModel
                {
                    Username = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value
                };
            }
            return null;
        }


        // GET: api/CompraDetalle
        [HttpGet]
        public ActionResult Get()
        {
            var currentUser = GetCurrentUser();
            try
            {
                var compras = _unidadTrabajo.Compra.ObtenerTodos();
                foreach (var obj in compras)
                {
                    var id = obj.Id;
                    var detalles = _unidadTrabajo.Detalle.ObtenerRelacionMuchos(id);
                    obj.Detalles = detalles;
                }
                _logger.LogWarning("Se obtienen todos los registros de la tabla Compras con sus registros relacionados de la tabla Detalles");
                return Ok(compras);
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // GET: api/CompraDetalle/5
        [HttpGet("{id}", Name = "GetCompraDetalle")]
        public ActionResult Get(int id)
        {
            try
            {
                var compra = _unidadTrabajo.Compra.Obtener(id);
                if (compra == null)
                {
                    return NotFound();
                }
                else
                {
                    var detalle = _unidadTrabajo.Detalle.ObtenerRelacionMuchos(id);
                    foreach(var item in detalle)
                    {
                        compra.Detalles.Add(item);
                    }
                    _logger.LogWarning("Se obtiene el registro Compra con id " + id + "y los registros Detalles relacionados a este.");
                    return Ok(compra);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // POST: api/CompraDetalle
        [HttpPost]
        public ActionResult Post([FromBody] Compra compra)
        {
            
            try
            {
                var Compras = _unidadTrabajo.Compra.ObtenerTodos();
                var nroCompras = 1;
                foreach(var c in Compras)
                {
                    nroCompras = (int)(c.NumeroFactura + 1);
                }
                

                var detalles = compra.Detalles;

                List<decimal?> valores = new List<decimal?>();
                
                foreach (var item in detalles)
                {
                    item.ValorTotal = _unidadTrabajo.CalcularPrecioDetalle(item.Cantidad, item.ValorUnitario);
                    var precio = item.ValorTotal;
                    valores.Add(precio);
                    item.CompraId = compra.Id;
                    _unidadTrabajo.Detalle.Agregar(item);
                }

                decimal? subtotal = valores.Sum();

                compra.NumeroFactura = nroCompras;
                compra.TotalArticulos = subtotal;
                compra.TotalImpuestosVenta = _unidadTrabajo.CalcularIva(subtotal);
                compra.TotalImpuestosFlete = _unidadTrabajo.CalcularIva(compra.ValorFlete);
                compra.TotalImpuestosNetos = compra.TotalImpuestosFlete + compra.TotalImpuestosVenta;
                compra.ValorTotalFactura = compra.TotalArticulos + compra.TotalImpuestosNetos + compra.ValorFlete;
                _unidadTrabajo.Compra.Agregar(compra);
                _unidadTrabajo.Guardar();

                _logger.LogWarning("Se crea un nuevo registro en la tabla Compra con registros relacionados en la tabla detalles");
                return CreatedAtRoute("GetCompraDetalle", new { id = compra.Id }, compra);

            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }

        }

        // PUT: api/CompraDetalle/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Compra compra)
        {
            try
            {
                var detalles = compra.Detalles;

                List<decimal?> valores = new List<decimal?>();

                foreach (var item in detalles)
                {
                    item.ValorTotal = _unidadTrabajo.CalcularPrecioDetalle(item.Cantidad, item.ValorUnitario);
                    var precio = item.ValorTotal;
                    valores.Add(precio);
                    item.CompraId = compra.Id;
                    _unidadTrabajo.Detalle.Actualizar(item);
                }

                decimal? subtotal = valores.Sum();

                compra.TotalArticulos = subtotal;
                compra.TotalImpuestosVenta = _unidadTrabajo.CalcularIva(subtotal);
                compra.TotalImpuestosFlete = _unidadTrabajo.CalcularIva(compra.ValorFlete);
                compra.TotalImpuestosNetos = compra.TotalImpuestosFlete + compra.TotalImpuestosVenta;
                compra.ValorTotalFactura = compra.TotalArticulos + compra.TotalImpuestosNetos + compra.ValorFlete;

                _unidadTrabajo.Compra.Actualizar(compra);
                _unidadTrabajo.Guardar();

                _logger.LogWarning("Se actualizan el registro Compra con id "+ id + "y los registros de la tabla detalle relacionados a este");
                return CreatedAtRoute("GetCompraDetalle", new { id = compra.Id }, compra);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/CompraDetalle/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                _unidadTrabajo.Detalle.RemoverRelacionados(id);
                _unidadTrabajo.Compra.Remover(id);
                _unidadTrabajo.Guardar();
                _logger.LogInformation("Se elimina el registro Compra con ID("+ id +") con los registros Detalles relacionados a este");
                return Ok(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
