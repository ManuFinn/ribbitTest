using Examen.Interfaces;
using Examen.Models;
using Examen.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Examen.Controllers
{

    [Route("api/[controller]")]

    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly IProductoService _productoService;

        public ProductosController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        // GET: api/Productos
        /// <summary>
        /// Obtener todos los Productos
        /// </summary>
        /// <remarks>
        /// Peticion GET para obtener todos los Productos en la Base de Datos.
        /// </remarks>
        /// <response code="200">OK. Devuelve la lista de Productos.</response>        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var pro = await _productoService.GetAllAsync();
            return Ok(pro);
        }

        // GET: api/Productos/id
        /// <summary>
        /// Obtener un Producto por Id
        /// </summary>
        /// <remarks>
        /// Petición GET para obtener un Producto especifico por su Id en la Base de Datos.
        /// </remarks>
        /// <param name="id">Id del Producto.</param>
        /// <response code="200">OK. Devuelve el Producto buscado.</response>
        /// <response code="404">NotFound. No se ha encontrado el Producto solicitado.</response>
        [HttpGet("/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var pro = await _productoService.GetProductoByIdAsync(id);
            if (pro != null)
            {
                return Ok(pro);
            }
            else
            {
                return NotFound();
            }
        }


        // POST: api/Productos
        /// <summary>
        /// Crear un nuevo Producto
        /// </summary>
        /// <remarks>
        /// Petición POST para crear un nuevo registro Producto en la Base de Datos. 
        /// 
        /// Nota: El Id y Fecha no son necesarios para la petición.
        /// 
        /// Ejemplo de petición:
        /// 
        ///      POST api/Productos
        ///     {
        ///        "nombre": "Producto Generico 1",
        ///        "descripcion": "Descripción del producto generico 1",
        ///        "precio": 100,
        ///        "stock": 20
        ///     }
        /// 
        /// </remarks>
        /// <response code="201">Created. Producto correctamente creado en la BD.</response>        
        /// <response code="400">BadRequest. No se ha creado el Producto en la BD. Formato del objeto incorrecto, verificar mensajes de errores.</response>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Producto pro)
        {
            try
            {
                var (isValid, errors) = await _productoService.IsValidAsync(pro);

                if (isValid)
                {
                    await _productoService.CreateProductoAsync(pro);
                    return Ok();
                }
                else
                {
                    return BadRequest(errors);
                }
            }
            catch (Exception ex)
            {
                var errorMessage = ex.InnerException?.Message ?? ex.Message;
                return StatusCode(500, errorMessage);
            }
        }


        // PUT: api/Productos/id
        /// <summary>
        /// Editar un Producto ya existente
        /// </summary>
        /// <remarks>
        /// Petición PUT para editar un registro Producto ya existente en la Base de Datos. 
        /// 
        /// Nota: El Id y Fecha no son necesarios para la petición.
        /// 
        /// Ejemplo de petición:
        /// 
        ///      PUT api/Productos/id
        ///     {
        ///        "nombre": "Producto Generico 2",
        ///        "descripcion": "Descripción del producto generico 2",
        ///        "precio": 32,
        ///        "stock": 64
        ///     }
        /// 
        /// </remarks>
        /// <response code="201">Created. Producto correctamente editado en la BD.</response>        
        /// <response code="400">BadRequest. No se ha editado el Producto en la BD. Formato del objeto incorrecto, verificar mensajes de errores.</response>
        [HttpPut("/{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Producto pro)
        {
            try
            {
                var productOriginal = await _productoService.GetProductoByIdAsync(id);
                if (productOriginal != null)
                {
                    var (isValid, errors) = await _productoService.IsValidAsync(pro);

                    if (isValid)
                    {
                        productOriginal.Nombre = pro.Nombre;
                        productOriginal.Descripcion = pro.Descripcion;
                        productOriginal.Precio = pro.Precio;
                        productOriginal.Stock = pro.Stock;

                        await _productoService.UpdateProductoAsync(productOriginal);
                        return Ok();
                    }
                    else
                    {
                        return BadRequest(errors);
                    }
                }
                else
                {
                    return NotFound("Producto no encontrado.");
                }
            }
            catch (Exception ex)
            {
                var errorMessage = ex.InnerException?.Message ?? ex.Message;
                return StatusCode(500, errorMessage);
            }
        }

        // DELETE: api/Productos/id
        /// <summary>
        /// Borrar un registro Producto por Id
        /// </summary>
        /// <remarks>
        /// Petición DELETE para eliminar un registro Producto especifico por su Id en la Base de Datos.
        /// </remarks>
        /// <param name="id">Id del Producto.</param>
        /// <response code="200">OK. El registro del Producto se ha eliminado correctamente.</response>
        /// <response code="404">NotFound. No se ha eliminado el Producto solicitado.</response>
        [HttpDelete("/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var pro = await _productoService.GetProductoByIdAsync(id);
                if (pro != null)
                {
                    await _productoService.DeleteProductoAsync(id);
                    return Ok();
                }
                else
                {
                    return NotFound("Producto no encontrado.");
                }
            }
            catch (Exception ex)
            {
                var errorMessage = ex.InnerException?.Message ?? ex.Message;
                return StatusCode(500, errorMessage);
            }
        }

    }
}
