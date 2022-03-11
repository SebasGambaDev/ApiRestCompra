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
    public class CompraController : ControllerBase
    {
        private readonly IUnidadTrabajo _unidadTrabajo;

        public CompraController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }

        // GET: api/Compra
        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                var compras = _unidadTrabajo.Compra.ObtenerTodos();
                if (compras == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(compras);
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Compra/5
        [HttpGet("{id}", Name = "GetCompra")]
        public ActionResult Get(int id)
        {
            try
            {
                var compra = _unidadTrabajo.Compra.Obtener(id);
                if(compra == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(compra);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Compra
        [HttpPost]
        public ActionResult Post([FromBody] Compra compra)
        {
            try
            {
                _unidadTrabajo.Compra.Agregar(compra);
                _unidadTrabajo.Guardar();
                
                return CreatedAtRoute("GetCompra", new { id = compra.Id}, compra);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // PUT: api/Compra/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Compra compra)
        {
            try
            {
                _unidadTrabajo.Compra.Actualizar(id, compra);
                _unidadTrabajo.Guardar();
                var obj = _unidadTrabajo.Compra.Obtener(id);
                return CreatedAtRoute("GetCompra", new { id = obj.Id }, obj);
                
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
                _unidadTrabajo.Compra.Remover(id);
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
