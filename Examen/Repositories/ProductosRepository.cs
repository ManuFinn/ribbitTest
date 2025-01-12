using Examen.Interfaces;
using Examen.Models;
using Microsoft.EntityFrameworkCore;

namespace Examen.Repositories
{
    public class ProductosRepository : IProductosRepository
    {
        private readonly TiendaDbContext Context;

        public ProductosRepository(TiendaDbContext context)
        {
            Context = context;
        }

        //public ProductosRepository(DbContext context) : base(context) { }

        public async Task<IEnumerable<Producto>> GetAllAsync()
        {
            return await Context
                .Set<Producto>()
                .OrderBy(x => x.Id)
                .ToListAsync();
        }

        public async Task<Producto?> GetByIdAsync(int id)
        {
            return await Context
                .Set<Producto>()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task InsertAsync(Producto entity)
        {
            await Context
                .Set<Producto>()
                .AddAsync(entity);
            await Context
                .SaveChangesAsync();
        }

        public async Task UpdateAsync(Producto entity)
        {
            Context
                .Entry(entity).State = EntityState.Modified;
            await Context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await Context.Set<Producto>().FirstOrDefaultAsync(x => x.Id == id);
            if (entity != null)
            {
                Context
                    .Set<Producto>()
                    .Remove(entity);
                await Context.SaveChangesAsync();
            }
        }

        public async Task<(bool IsValid, List<string> ValidationErrors)> IsValidAsync(Producto entity)
        {
            var validationErrors = new List<string>();

            // Validaciones en memoria
            if (string.IsNullOrEmpty(entity.Nombre))
            {
                validationErrors.Add("El nombre no debe estar vacío.");
            }
            if (entity.Nombre.Length < 3 || entity.Nombre.Length > 100)
            {
                validationErrors.Add("El nombre debe contar entre 3 y 100 caracteres.");
            }
            if (!string.IsNullOrEmpty(entity.Descripcion) && entity.Descripcion.Length > 500)
            {
                validationErrors.Add("La descripción tiene un límite de 500 caracteres.");
            }
            if (entity.Precio <= 0)
            {
                validationErrors.Add("El precio debe ser mayor que 0.");
            }
            if (entity.Stock < 0)
            {
                validationErrors.Add("El stock no puede ser negativo.");
            }

            // Validaciones en base de datos
            var nombreExiste = await Context
                .Set<Producto>()
                .AnyAsync(p => p.Nombre == entity.Nombre && p.Id != entity.Id);

            return (validationErrors.Count == 0, validationErrors);
        }

        public bool IsValid(Producto entity, out List<string> validationErrors)
        {
            var result = IsValidAsync(entity).Result; 
            validationErrors = result.ValidationErrors; 
            return result.IsValid;
        }
    }
}
