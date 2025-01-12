using Examen.Models;

namespace Examen.Interfaces
{
    public interface IProductoService
    {
        Task<IEnumerable<Producto>> GetAllAsync();
        Task<Producto> GetProductoByIdAsync(int id);
        Task CreateProductoAsync(Producto producto);
        Task UpdateProductoAsync(Producto producto);
        Task DeleteProductoAsync(int id);
        Task<(bool IsValid, List<string> Errors)> IsValidAsync(Producto producto);
    }
}
