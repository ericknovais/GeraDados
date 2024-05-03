using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Geradados.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class CriaTabelaAtivos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ativos",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoDeAtivoID = table.Column<int>(type: "int", nullable: true),
                    Ticker = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UltimaNegociacao = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ativos", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Ativos_TipoDeAtivos_TipoDeAtivoID",
                        column: x => x.TipoDeAtivoID,
                        principalTable: "TipoDeAtivos",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ativos_TipoDeAtivoID",
                table: "Ativos",
                column: "TipoDeAtivoID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ativos");
        }
    }
}
