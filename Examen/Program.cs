using Examen.Interfaces;
using Examen.Models;
using Examen.Repositories;
using Examen.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<TiendaDbContext>
    (options => options.UseSqlite("Data Source=.\\DB\\TiendaDB.db"));

builder.Services.AddControllers();

builder.Services.AddTransient<IProductoService, ProductoService>();
builder.Services.AddTransient<IProductosRepository, ProductosRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseCors(x => x
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetIsOriginAllowed(origin => true)
                    .AllowCredentials()
                    );

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();