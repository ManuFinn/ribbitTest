using Examen.Interfaces;
using Examen.Models;
using Examen.Repositories;

namespace Examen.Services
{
    public class ProductoService : IProductoService
    {
        private readonly IProductosRepository _productoRepository;

        public ProductoService(IProductosRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        async Task<IEnumerable<Producto>> IProductoService.GetAllAsync()
        {
            return await _productoRepository.GetAllAsync();
        }

        async Task<Producto> IProductoService.GetProductoByIdAsync(int id)
        {
            return await _productoRepository.GetByIdAsync(id);
        }

        async Task IProductoService.CreateProductoAsync(Producto producto)
        {
            await _productoRepository.InsertAsync(producto);
        }

        async Task IProductoService.UpdateProductoAsync(Producto producto)
        {
            await _productoRepository.UpdateAsync(producto);
        }

        async Task IProductoService.DeleteProductoAsync(int id)
        {
            await _productoRepository.DeleteAsync(id);
        }

        async Task<(bool IsValid, List<string> Errors)> IProductoService.IsValidAsync(Producto producto)
        {
            return await _productoRepository.IsValidAsync(producto);
        }
    }
}
