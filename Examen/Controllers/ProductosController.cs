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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var pro = await _productoService.GetAllAsync();
            return Ok(pro);
        }

        [HttpGet("/{id}")]
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
