using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiRestCompra.Models;
using ApiRestCompra.Repositories.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiRestCompra.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompraDetalleController : ControllerBase
    {
        private readonly IUnidadTrabajo _unidadTrabajo;

        public CompraDetalleController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }

        // GET: api/CompraDetalle
        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                var compras = _unidadTrabajo.Compra.ObtenerTodos();
                foreach (var obj in compras)
                {
                    var id = obj.Id;
                    var detalles = _unidadTrabajo.Detalle.ObtenerRelacionMuchos(id);
                    obj.Detalles = detalles;
                }
                
                return Ok(compras);
                
            }
            catch (Exception ex)
            {
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
                    return Ok(compra);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/CompraDetalle
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/CompraDetalle/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/CompraDetalle/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
