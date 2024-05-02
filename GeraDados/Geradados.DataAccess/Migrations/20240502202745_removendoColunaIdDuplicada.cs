using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Geradados.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class removendoColunaIdDuplicada : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdPessoa",
                table: "Enderecos");

            migrationBuilder.DropColumn(
                name: "IdPessoa",
                table: "Contatos");

            migrationBuilder.DropColumn(
                name: "IdTipoContato",
                table: "Contatos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdPessoa",
                table: "Enderecos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdPessoa",
                table: "Contatos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdTipoContato",
                table: "Contatos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
