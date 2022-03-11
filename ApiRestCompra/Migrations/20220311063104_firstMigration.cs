﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiRestCompra.Migrations
{
    public partial class firstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Compra",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClienteNombre1 = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ClienteNombre2 = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    ClienteApellido1 = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ClienteApellido2 = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    ClienteEmail = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ClienteDireccionDespacho = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CiudadDespacho = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    ClienteDireccionFacturacion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CiudadFacturacion = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    ClienteTelefono1 = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    ClienteTelefono2 = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    ValorFlete = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NumeroFactura = table.Column<int>(type: "int", nullable: false),
                    TotalArticulos = table.Column<int>(type: "int", nullable: false),
                    TotalImpuestosVenta = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalImpuestosFlete = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalImpuestosNetos = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValorTotalFactura = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compra", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Detalles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoReferencia = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    ValorUnitario = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    referencia = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    ValorTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CompraId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Detalles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Detalles_Compra_CompraId",
                        column: x => x.CompraId,
                        principalTable: "Compra",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Detalles_CompraId",
                table: "Detalles",
                column: "CompraId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Detalles");

            migrationBuilder.DropTable(
                name: "Compra");
        }
    }
}
