using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Geradados.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RemovendoCampoDoContato : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MyProperty",
                table: "Contatos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MyProperty",
                table: "Contatos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
