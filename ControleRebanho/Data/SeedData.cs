using ControleRebanho.Models;
using ControleRebanho.Models.Enums;
using ControleRebanho.Utils;
using Microsoft.EntityFrameworkCore;

namespace ControleRebanho.Data
{
    // Dados iniciais do banco
    public static class SeedData
    {
        private const int PecuaristaId = 1;
        private const int VeterinarioId = 2;
        private const string SenhaPadrao = "123456";

        public static void Popular(ModelBuilder modelBuilder)
        {
            PopularPessoas(modelBuilder);
            PopularFazenda(modelBuilder);
            PopularVacinas(modelBuilder);
            PopularAnimais(modelBuilder);
            PopularVacinacoes(modelBuilder);
            PopularExames(modelBuilder);
        }

        // Pecuarista e veterinário
        private static void PopularPessoas(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pecuarista>().HasData(new Pecuarista
            {
                Id = PecuaristaId,
                Nome = "João Ramos",
                Cpf = "111.111.111-11",
                Login = "joao.ramos",
                SenhaHash = SenhaHasher.Gerar(SenhaPadrao),
                Telefone = "(27) 99999-0000"
            });

            modelBuilder.Entity<Veterinario>().HasData(new Veterinario
            {
                Id = VeterinarioId,
                Nome = "Dra. Carla Souza",
                Cpf = "222.222.222-22",
                Login = "carla.souza",
                SenhaHash = SenhaHasher.Gerar(SenhaPadrao),
                Crmv = "CRMV-ES 1234"
            });
        }

        // Fazenda do pecuarista
        private static void PopularFazenda(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Fazenda>().HasData(new Fazenda
            {
                Id = 1,
                Nome = "Fazenda Boa Vista",
                CodigoPropriedade = "32.045.0018",
                Municipio = "Santa Teresa",
                Uf = "ES",
                PecuaristaId = PecuaristaId
            });
        }

        // Catálogo de vacinas
        private static void PopularVacinas(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vacina>().HasData(
                NovaVacina(1, "B19", "Brucelose", PublicoAlvo.FEMEAS, 3, 8, obrigatoria: true),
                NovaVacina(2, "RB51", "Brucelose", PublicoAlvo.FEMEAS, 8, null, obrigatoria: true),
                NovaVacina(3, "Raiva", "Raiva", PublicoAlvo.TODOS, 4, null),
                NovaVacina(4, "Clostridioses", "Clostridioses", PublicoAlvo.TODOS, 2, null),
                NovaVacina(5, "Leptospirose", "Leptospirose", PublicoAlvo.TODOS, 4, null),
                NovaVacina(6, "Botulismo", "Botulismo", PublicoAlvo.TODOS, 0, null),
                NovaVacina(7, "Tétano", "Tétano", PublicoAlvo.TODOS, 0, null),
                NovaVacina(8, "IBR", "Rinotraqueíte infecciosa bovina", PublicoAlvo.TODOS, 3, null),
                NovaVacina(9, "BVD", "Diarreia viral bovina", PublicoAlvo.TODOS, 3, null),
                NovaVacina(10, "Diarreia neonatal", "Diarreia neonatal", PublicoAlvo.FEMEAS, 12, null),
                NovaVacina(11, "Mastite", "Mastite", PublicoAlvo.FEMEAS, 12, null),
                NovaVacina(12, "Vermífugo", "Verminose", PublicoAlvo.TODOS, 0, null, tipo: TipoProduto.VERMIFUGO));
        }

        private static Vacina NovaVacina(int id, string nome, string doenca, PublicoAlvo publico,
            int idadeMinima, int? idadeMaxima, bool obrigatoria = false, TipoProduto tipo = TipoProduto.VACINA)
        {
            return new Vacina
            {
                Id = id,
                Nome = nome,
                Doenca = doenca,
                TipoProduto = tipo,
                PublicoAlvo = publico,
                IdadeMinimaMeses = idadeMinima,
                IdadeMaximaMeses = idadeMaxima,
                Obrigatoria = obrigatoria,
                Ativa = true
            };
        }

        // Animais do rebanho
        private static void PopularAnimais(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Animal>().HasData(
                NovoAnimal(1, "BR-0231", Sexo.FEMEA, new DateOnly(2026, 5, 10)),
                NovoAnimal(2, "BR-0244", Sexo.FEMEA, new DateOnly(2026, 4, 2)),
                NovoAnimal(3, "BR-0198", Sexo.MACHO, new DateOnly(2025, 9, 15), compradoEm: new DateOnly(2026, 1, 10)),
                NovoAnimal(4, "BR-0160", Sexo.FEMEA, new DateOnly(2025, 7, 7)),
                NovoAnimal(5, "BR-0108", Sexo.FEMEA, new DateOnly(2024, 2, 20), compradoEm: new DateOnly(2025, 3, 15)),
                NovoAnimal(6, "BR-0012", Sexo.FEMEA, new DateOnly(2021, 1, 3)),
                NovoAnimal(7, "BR-0077", Sexo.MACHO, new DateOnly(2022, 6, 11), compradoEm: new DateOnly(2023, 8, 20),
                    saida: new DateOnly(2026, 10, 1), motivo: MotivoSaida.VENDA),
                NovoAnimal(8, "BR-0130", Sexo.MACHO, new DateOnly(2025, 3, 5), compradoEm: new DateOnly(2025, 6, 1),
                    saida: new DateOnly(2026, 10, 22), motivo: MotivoSaida.MORTE),
                NovoAnimal(9, "BR-0247", Sexo.MACHO, new DateOnly(2026, 10, 3)),
                NovoAnimal(10, "BR-0248", Sexo.FEMEA, new DateOnly(2026, 10, 5)),
                NovoAnimal(11, "BR-0249", Sexo.FEMEA, new DateOnly(2024, 1, 20), compradoEm: new DateOnly(2026, 10, 12)),
                NovoAnimal(12, "BR-0055", Sexo.MACHO, new DateOnly(2022, 9, 1)));
        }

        private static Animal NovoAnimal(int id, string brinco, Sexo sexo, DateOnly nascimento,
            DateOnly? compradoEm = null, DateOnly? saida = null, MotivoSaida? motivo = null)
        {
            bool foiComprado = compradoEm != null;
            return new Animal
            {
                Id = id,
                Brinco = brinco,
                Sexo = sexo,
                DataNascimento = nascimento,
                OrigemEntrada = foiComprado ? OrigemEntrada.COMPRA : OrigemEntrada.NASCIMENTO,
                DataEntrada = compradoEm ?? nascimento,
                DataSaida = saida,
                MotivoSaida = motivo,
                PecuaristaId = PecuaristaId
            };
        }

        // Vacinas aplicadas
        private static void PopularVacinacoes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vacinacao>().HasData(
                NovaVacinacao(1, animalId: 1, vacinaId: 1, new DateOnly(2026, 10, 15), "Única"),
                NovaVacinacao(2, animalId: 1, vacinaId: 4, new DateOnly(2026, 9, 20), "1ª dose"),
                NovaVacinacao(3, animalId: 1, vacinaId: 12, new DateOnly(2026, 9, 20), "-"),
                NovaVacinacao(4, animalId: 2, vacinaId: 4, new DateOnly(2026, 9, 20), "1ª dose"),
                NovaVacinacao(5, animalId: 4, vacinaId: 2, new DateOnly(2026, 8, 5), "Única"),
                NovaVacinacao(6, animalId: 5, vacinaId: 3, new DateOnly(2026, 3, 10), "Única"),
                NovaVacinacao(7, animalId: 6, vacinaId: 3, new DateOnly(2026, 3, 10), "Reforço anual"));
        }

        private static Vacinacao NovaVacinacao(int id, int animalId, int vacinaId, DateOnly data, string dose) =>
            new Vacinacao
            {
                Id = id,
                AnimalId = animalId,
                VacinaId = vacinaId,
                VeterinarioId = VeterinarioId,
                DataAplicacao = data,
                Dose = dose
            };

        // Exames realizados
        private static void PopularExames(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Exame>().HasData(
                new Exame { Id = 1, AnimalId = 5, VeterinarioId = VeterinarioId, Tipo = TipoExame.TUBERCULOSE,
                    Resultado = ResultadoExame.NEGATIVO, DataColeta = new DateOnly(2026, 10, 28) },
                new Exame { Id = 2, AnimalId = 5, VeterinarioId = VeterinarioId, Tipo = TipoExame.BRUCELOSE,
                    Resultado = ResultadoExame.NEGATIVO, DataColeta = new DateOnly(2026, 1, 15) },
                new Exame { Id = 3, AnimalId = 6, VeterinarioId = VeterinarioId, Tipo = TipoExame.BRUCELOSE,
                    Resultado = ResultadoExame.NEGATIVO, DataColeta = new DateOnly(2026, 7, 15) });
        }
    }
}
