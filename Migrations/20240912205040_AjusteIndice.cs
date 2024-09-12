using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DsiVendas.Migrations
{
    /// <inheritdoc />
    public partial class AjusteIndice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdCliente",
                table: "Vendas");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdCliente",
                table: "Vendas",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
