using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop.Migrations
{
    public partial class correcao_de_campo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descrica",
                table: "Produtos");

            migrationBuilder.AddColumn<string>(
                name: "Descricao",
                table: "Produtos",
                maxLength: 1024,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descricao",
                table: "Produtos");

            migrationBuilder.AddColumn<string>(
                name: "Descrica",
                table: "Produtos",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true);
        }
    }
}
