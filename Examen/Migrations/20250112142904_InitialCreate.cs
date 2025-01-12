using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Examen.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    nombre = table.Column<string>(type: "TEXT", nullable: false),
                    descripcion = table.Column<string>(type: "TEXT", nullable: true),
                    precio = table.Column<double>(type: "REAL", nullable: false),
                    stock = table.Column<int>(type: "INTEGER", nullable: false),
                    fechaCreacion = table.Column<DateTime>(type: "DATETIME", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "Productos",
                columns: new[] { "id", "descripcion", "nombre", "precio", "stock" },
                values: new object[,]
                {
                    { 1, "generico 1", "Producto 1", 10.0, 2 },
                    { 2, "generico 2", "Producto 2", 20.0, 18 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Productos_id",
                table: "Productos",
                column: "id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Productos");
        }
    }
}
