using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ControleRebanho.Migrations
{
    /// <inheritdoc />
    public partial class VersaoInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pessoas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cpf = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Login = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SenhaHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoPessoa = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Crmv = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessoas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vacinas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Doenca = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoProduto = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    PublicoAlvo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    IdadeMinimaMeses = table.Column<int>(type: "int", nullable: false),
                    IdadeMaximaMeses = table.Column<int>(type: "int", nullable: true),
                    Obrigatoria = table.Column<bool>(type: "bit", nullable: false),
                    Ativa = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vacinas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Animais",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Brinco = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Sexo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    DataNascimento = table.Column<DateOnly>(type: "date", nullable: false),
                    OrigemEntrada = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    DataEntrada = table.Column<DateOnly>(type: "date", nullable: false),
                    DataSaida = table.Column<DateOnly>(type: "date", nullable: true),
                    MotivoSaida = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    PecuaristaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animais", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Animais_Pessoas_PecuaristaId",
                        column: x => x.PecuaristaId,
                        principalTable: "Pessoas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Fazendas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CodigoPropriedade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Municipio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Uf = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    PecuaristaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fazendas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fazendas_Pessoas_PecuaristaId",
                        column: x => x.PecuaristaId,
                        principalTable: "Pessoas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Exames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Resultado = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    DataColeta = table.Column<DateOnly>(type: "date", nullable: false),
                    AnimalId = table.Column<int>(type: "int", nullable: false),
                    VeterinarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exames_Animais_AnimalId",
                        column: x => x.AnimalId,
                        principalTable: "Animais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Exames_Pessoas_VeterinarioId",
                        column: x => x.VeterinarioId,
                        principalTable: "Pessoas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vacinacoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataAplicacao = table.Column<DateOnly>(type: "date", nullable: false),
                    Dose = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnimalId = table.Column<int>(type: "int", nullable: false),
                    VacinaId = table.Column<int>(type: "int", nullable: false),
                    VeterinarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vacinacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vacinacoes_Animais_AnimalId",
                        column: x => x.AnimalId,
                        principalTable: "Animais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vacinacoes_Pessoas_VeterinarioId",
                        column: x => x.VeterinarioId,
                        principalTable: "Pessoas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vacinacoes_Vacinas_VacinaId",
                        column: x => x.VacinaId,
                        principalTable: "Vacinas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Pessoas",
                columns: new[] { "Id", "Cpf", "Login", "Nome", "SenhaHash", "Telefone", "TipoPessoa" },
                values: new object[] { 1, "111.111.111-11", "joao.ramos", "João Ramos", "8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92", "(27) 99999-0000", "Pecuarista" });

            migrationBuilder.InsertData(
                table: "Pessoas",
                columns: new[] { "Id", "Cpf", "Crmv", "Login", "Nome", "SenhaHash", "TipoPessoa" },
                values: new object[] { 2, "222.222.222-22", "CRMV-ES 1234", "carla.souza", "Dra. Carla Souza", "8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92", "Veterinario" });

            migrationBuilder.InsertData(
                table: "Vacinas",
                columns: new[] { "Id", "Ativa", "Doenca", "IdadeMaximaMeses", "IdadeMinimaMeses", "Nome", "Obrigatoria", "PublicoAlvo", "TipoProduto" },
                values: new object[,]
                {
                    { 1, true, "Brucelose", 8, 3, "B19", true, "FEMEAS", "VACINA" },
                    { 2, true, "Brucelose", null, 8, "RB51", true, "FEMEAS", "VACINA" },
                    { 3, true, "Raiva", null, 4, "Raiva", false, "TODOS", "VACINA" },
                    { 4, true, "Clostridioses", null, 2, "Clostridioses", false, "TODOS", "VACINA" },
                    { 5, true, "Leptospirose", null, 4, "Leptospirose", false, "TODOS", "VACINA" },
                    { 6, true, "Botulismo", null, 0, "Botulismo", false, "TODOS", "VACINA" },
                    { 7, true, "Tétano", null, 0, "Tétano", false, "TODOS", "VACINA" },
                    { 8, true, "Rinotraqueíte infecciosa bovina", null, 3, "IBR", false, "TODOS", "VACINA" },
                    { 9, true, "Diarreia viral bovina", null, 3, "BVD", false, "TODOS", "VACINA" },
                    { 10, true, "Diarreia neonatal", null, 12, "Diarreia neonatal", false, "FEMEAS", "VACINA" },
                    { 11, true, "Mastite", null, 12, "Mastite", false, "FEMEAS", "VACINA" },
                    { 12, true, "Verminose", null, 0, "Vermífugo", false, "TODOS", "VERMIFUGO" }
                });

            migrationBuilder.InsertData(
                table: "Animais",
                columns: new[] { "Id", "Brinco", "DataEntrada", "DataNascimento", "DataSaida", "MotivoSaida", "OrigemEntrada", "PecuaristaId", "Sexo" },
                values: new object[,]
                {
                    { 1, "BR-0231", new DateOnly(2026, 5, 10), new DateOnly(2026, 5, 10), null, null, "NASCIMENTO", 1, "FEMEA" },
                    { 2, "BR-0244", new DateOnly(2026, 4, 2), new DateOnly(2026, 4, 2), null, null, "NASCIMENTO", 1, "FEMEA" },
                    { 3, "BR-0198", new DateOnly(2026, 1, 10), new DateOnly(2025, 9, 15), null, null, "COMPRA", 1, "MACHO" },
                    { 4, "BR-0160", new DateOnly(2025, 7, 7), new DateOnly(2025, 7, 7), null, null, "NASCIMENTO", 1, "FEMEA" },
                    { 5, "BR-0108", new DateOnly(2025, 3, 15), new DateOnly(2024, 2, 20), null, null, "COMPRA", 1, "FEMEA" },
                    { 6, "BR-0012", new DateOnly(2021, 1, 3), new DateOnly(2021, 1, 3), null, null, "NASCIMENTO", 1, "FEMEA" },
                    { 7, "BR-0077", new DateOnly(2023, 8, 20), new DateOnly(2022, 6, 11), new DateOnly(2026, 10, 1), "VENDA", "COMPRA", 1, "MACHO" },
                    { 8, "BR-0130", new DateOnly(2025, 6, 1), new DateOnly(2025, 3, 5), new DateOnly(2026, 10, 22), "MORTE", "COMPRA", 1, "MACHO" },
                    { 9, "BR-0247", new DateOnly(2026, 10, 3), new DateOnly(2026, 10, 3), null, null, "NASCIMENTO", 1, "MACHO" },
                    { 10, "BR-0248", new DateOnly(2026, 10, 5), new DateOnly(2026, 10, 5), null, null, "NASCIMENTO", 1, "FEMEA" },
                    { 11, "BR-0249", new DateOnly(2026, 10, 12), new DateOnly(2024, 1, 20), null, null, "COMPRA", 1, "FEMEA" },
                    { 12, "BR-0055", new DateOnly(2022, 9, 1), new DateOnly(2022, 9, 1), null, null, "NASCIMENTO", 1, "MACHO" }
                });

            migrationBuilder.InsertData(
                table: "Fazendas",
                columns: new[] { "Id", "CodigoPropriedade", "Municipio", "Nome", "PecuaristaId", "Uf" },
                values: new object[] { 1, "32.045.0018", "Santa Teresa", "Fazenda Boa Vista", 1, "ES" });

            migrationBuilder.InsertData(
                table: "Exames",
                columns: new[] { "Id", "AnimalId", "DataColeta", "Resultado", "Tipo", "VeterinarioId" },
                values: new object[,]
                {
                    { 1, 5, new DateOnly(2026, 10, 28), "NEGATIVO", "TUBERCULOSE", 2 },
                    { 2, 5, new DateOnly(2026, 1, 15), "NEGATIVO", "BRUCELOSE", 2 },
                    { 3, 6, new DateOnly(2026, 7, 15), "NEGATIVO", "BRUCELOSE", 2 }
                });

            migrationBuilder.InsertData(
                table: "Vacinacoes",
                columns: new[] { "Id", "AnimalId", "DataAplicacao", "Dose", "VacinaId", "VeterinarioId" },
                values: new object[,]
                {
                    { 1, 1, new DateOnly(2026, 10, 15), "Única", 1, 2 },
                    { 2, 1, new DateOnly(2026, 9, 20), "1ª dose", 4, 2 },
                    { 3, 1, new DateOnly(2026, 9, 20), "-", 12, 2 },
                    { 4, 2, new DateOnly(2026, 9, 20), "1ª dose", 4, 2 },
                    { 5, 4, new DateOnly(2026, 8, 5), "Única", 2, 2 },
                    { 6, 5, new DateOnly(2026, 3, 10), "Única", 3, 2 },
                    { 7, 6, new DateOnly(2026, 3, 10), "Reforço anual", 3, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Animais_PecuaristaId_Brinco",
                table: "Animais",
                columns: new[] { "PecuaristaId", "Brinco" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Exames_AnimalId",
                table: "Exames",
                column: "AnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_Exames_VeterinarioId",
                table: "Exames",
                column: "VeterinarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Fazendas_PecuaristaId",
                table: "Fazendas",
                column: "PecuaristaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pessoas_Login",
                table: "Pessoas",
                column: "Login",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vacinacoes_AnimalId",
                table: "Vacinacoes",
                column: "AnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_Vacinacoes_VacinaId",
                table: "Vacinacoes",
                column: "VacinaId");

            migrationBuilder.CreateIndex(
                name: "IX_Vacinacoes_VeterinarioId",
                table: "Vacinacoes",
                column: "VeterinarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Exames");

            migrationBuilder.DropTable(
                name: "Fazendas");

            migrationBuilder.DropTable(
                name: "Vacinacoes");

            migrationBuilder.DropTable(
                name: "Animais");

            migrationBuilder.DropTable(
                name: "Vacinas");

            migrationBuilder.DropTable(
                name: "Pessoas");
        }
    }
}
