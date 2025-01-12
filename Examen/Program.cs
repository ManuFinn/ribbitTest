using Examen.Interfaces;
using Examen.Models;
using Examen.Repositories;
using Examen.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TiendaDbContext>
    (options => options.UseSqlite("Data Source=.\\DB\\TiendaDB.db"));

builder.Services.AddControllers();

builder.Services.AddTransient<IProductoService, ProductoService>();
builder.Services.AddTransient<IProductosRepository, ProductosRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var info = new OpenApiInfo()
{
    Title = "Documentacion de API Examen",
    Version = "v1",
    Description = "Esta es la documentacion para la API del Examen",
    Contact = new OpenApiContact()
    {
        Name = "Jean Munoz",
        Email = "manufinn117@gmail.com",
    }

};

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", info);
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();
app.UseCors(x => x
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetIsOriginAllowed(origin => true)
                    .AllowCredentials()
                    );

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(u =>
    {
        u.RouteTemplate = "swagger/{documentName}/swagger.json";
    });

    app.UseSwaggerUI(c =>
    {
        c.RoutePrefix = "swagger";
        c.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "ExamenAPI");
    });

}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();