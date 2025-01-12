using Examen.Models;

namespace Examen.Interfaces
{
    public interface IProductosRepository
    {
        Task<IEnumerable<Producto>> GetAllAsync();
        Task<Producto?> GetByIdAsync(int id);
        Task InsertAsync(Producto entity);
        Task UpdateAsync(Producto entity);
        Task DeleteAsync(int id);
        Task<(bool IsValid, List<string> ValidationErrors)> IsValidAsync(Producto entity);
    }
}
