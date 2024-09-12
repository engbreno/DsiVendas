using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DsiVendas.Migrations
{
    /// <inheritdoc />
    public partial class AjusteIndice2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdProduto",
                table: "ItemsVenda");

            migrationBuilder.DropColumn(
                name: "IdVenda",
                table: "ItemsVenda");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdProduto",
                table: "ItemsVenda",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdVenda",
                table: "ItemsVenda",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
