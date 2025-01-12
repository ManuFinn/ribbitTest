using Examen.Models;
using Examen.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Linq;


namespace PruebasExamen.Tests
{
    public class TestDbContextOptions
    {
        public static DbContextOptions<TiendaDbContext> GetInMemoryDbContextOptions()
        {
            return new DbContextOptionsBuilder<TiendaDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
        }
    }

    public class ProductosRepositoryTests
    {
        [Fact]
        public async Task GetAllAsync()
        {
            var options = TestDbContextOptions.GetInMemoryDbContextOptions();
            using (var context = new TiendaDbContext(options))
            {
                context.Productos.Add(new Producto { Id = 1, Nombre = "Producto 1", Precio = 10.00, Stock = 2 });
                context.Productos.Add(new Producto { Id = 2, Nombre = "Producto 2", Precio = 20.00, Stock = 18 });
                await context.SaveChangesAsync();
            }

            using (var context = new TiendaDbContext(options))
            {
                var repository = new ProductosRepository(context);

                var products = await repository.GetAllAsync();

                Assert.Equal(2, products.Count());
            }
        }


        [Fact]
        public async Task sinNegativo()
        {
            var options = TestDbContextOptions.GetInMemoryDbContextOptions();
            var producto = new Producto { Id = 1, Nombre = "Producto Válido", Precio = -10.00, Stock = 5 };

            using (var context = new TiendaDbContext(options))
            {
                var repository = new ProductosRepository(context);
                var result = await repository.IsValid(producto);

                Assert.False(result.IsValid, "El producto debería ser inválido debido al precio negativo.");
                Assert.Contains("El precio debe ser mayor que 0.", result.ValidationErrors);
            }
        }


        [Fact]
        public async Task nombreValido()
        {
            var options = TestDbContextOptions.GetInMemoryDbContextOptions();
            var productoConNombreVacio = new Producto { Id = 2, Nombre = "", Precio = 10.00, Stock = 5 };
            var productoConNombreCorto = new Producto { Id = 3, Nombre = "AB", Precio = 10.00, Stock = 5 };

            using (var context = new TiendaDbContext(options))
            {
                var repository = new ProductosRepository(context);

                var resultVacio = await repository.IsValidAsync(productoConNombreVacio);
                Assert.False(resultVacio.IsValid, "El producto debería ser inválido por nombre vacío.");
                Assert.Contains("El nombre no debe estar vacío.", resultVacio.ValidationErrors);

                var resultCorto = await repository.IsValidAsync(productoConNombreCorto);
                Assert.False(resultCorto.IsValid, "El producto debería ser inválido por nombre corto.");
                Assert.Contains("El nombre debe contar entre 3 y 100 caracteres.", resultCorto.ValidationErrors);
            }
        }
    }
}

