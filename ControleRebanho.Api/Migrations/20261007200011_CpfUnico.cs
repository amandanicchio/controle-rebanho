using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControleRebanho.Api.Migrations
{
    /// <inheritdoc />
    public partial class CpfUnico : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Cpf",
                table: "Produtores",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Produtores_Cpf",
                table: "Produtores",
                column: "Cpf",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Produtores_Cpf",
                table: "Produtores");

            migrationBuilder.AlterColumn<string>(
                name: "Cpf",
                table: "Produtores",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
