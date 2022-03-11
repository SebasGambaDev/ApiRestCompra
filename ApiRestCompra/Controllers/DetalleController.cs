using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiRestCompra.Models;
using ApiRestCompra.Repositories.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiRestCompra.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DetalleController : ControllerBase
    {
        private readonly IUnidadTrabajo _unidadTrabajo;

        public DetalleController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }

        // GET: api/Detalle
        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                var compras = _unidadTrabajo.Detalle.ObtenerTodos();
                if (compras == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(compras);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Detalle/5
        [HttpGet("{id}", Name = "GetDetalle")]
        public ActionResult Get(int id)
        {
            try
            {
                var detalle = _unidadTrabajo.Detalle.Obtener(id);
                if (detalle == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(detalle);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Detalle
        [HttpPost]
        public ActionResult Post([FromBody] Detalle detalle)
        {
            try
            {
                _unidadTrabajo.Detalle.Agregar(detalle);
                _unidadTrabajo.Guardar();

                return CreatedAtRoute("GetDetalle", new { id = detalle.Id }, detalle);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // PUT: api/Compra/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Detalle detalle)
        {
            try
            {
                var obj = _unidadTrabajo.Detalle.Obtener(id);
                _unidadTrabajo.Detalle.Actualizar(detalle);
                _unidadTrabajo.Guardar();
                
                return CreatedAtRoute("GetDetalle", new { id = obj.Id }, obj);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Compra/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                _unidadTrabajo.Detalle.Remover(id);
                _unidadTrabajo.Guardar();
                return Ok(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
