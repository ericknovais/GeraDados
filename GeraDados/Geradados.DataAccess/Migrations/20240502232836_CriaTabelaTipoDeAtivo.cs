using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Geradados.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class CriaTabelaTipoDeAtivo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contatos_TipoContatos_TipoContatoID",
                table: "Contatos");

            migrationBuilder.AlterColumn<int>(
                name: "TipoContatoID",
                table: "Contatos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "TipoDeAtivos",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoDeAtivos", x => x.ID);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Contatos_TipoContatos_TipoContatoID",
                table: "Contatos",
                column: "TipoContatoID",
                principalTable: "TipoContatos",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contatos_TipoContatos_TipoContatoID",
                table: "Contatos");

            migrationBuilder.DropTable(
                name: "TipoDeAtivos");

            migrationBuilder.AlterColumn<int>(
                name: "TipoContatoID",
                table: "Contatos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Contatos_TipoContatos_TipoContatoID",
                table: "Contatos",
                column: "TipoContatoID",
                principalTable: "TipoContatos",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
