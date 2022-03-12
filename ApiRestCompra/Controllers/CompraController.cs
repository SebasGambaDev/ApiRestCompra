using System;
using System.Collections.Generic;
using System.Linq;
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
    public class CompraController : ControllerBase
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        private readonly ILogger<CompraDetalleController> _logger;


        public CompraController(IUnidadTrabajo unidadTrabajo, ILogger<CompraDetalleController> logger)
        {
            _unidadTrabajo = unidadTrabajo;
            _logger = logger;
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
                    _logger.LogInformation("Se obtienen todos los registros de la tabla Compras");
                    return Ok(compras);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
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
                    _logger.LogInformation("Se obtiene el registro de la tabla Compras con id " + id);
                    return Ok(compra);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
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

                _logger.LogInformation("Se crea un nuevo registro en la tabla Compras con id" + compra.Id);
                return CreatedAtRoute("GetCompra", new { id = compra.Id}, compra);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }

        }

        // PUT: api/Compra/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Compra compra)
        {
            try
            {
                _unidadTrabajo.Compra.Actualizar(compra);
                _unidadTrabajo.Guardar();

                _logger.LogInformation("Se actualizan los datos de un registro en la tabla Compras con id" + compra.Id);
                return CreatedAtRoute("GetCompra", new { id = compra.Id }, compra);
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
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

                _logger.LogInformation("Se elimina registro de la tabla Compras con id " + id);
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
