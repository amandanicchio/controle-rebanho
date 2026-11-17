using ControleRebanho.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleRebanho.Data
{
    // Contexto do banco de dados
    public class RebanhoDbContext : DbContext
    {
        private const string ConnectionString =
            "Server=(localdb)\\mssqllocaldb;Database=ControleRebanhoDB;Trusted_Connection=True;";

        public DbSet<Pessoa> Pessoas { get; set; } = null!;
        public DbSet<Pecuarista> Pecuaristas { get; set; } = null!;
        public DbSet<Veterinario> Veterinarios { get; set; } = null!;
        public DbSet<Fazenda> Fazendas { get; set; } = null!;
        public DbSet<Animal> Animais { get; set; } = null!;
        public DbSet<Vacina> Vacinas { get; set; } = null!;
        public DbSet<Vacinacao> Vacinacoes { get; set; } = null!;
        public DbSet<Exame> Exames { get; set; } = null!;

        // Conexão com o banco
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }

        // Mapeamento das tabelas
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigurarHeranca(modelBuilder);
            ConfigurarRelacionamentos(modelBuilder);
            ConfigurarColunas(modelBuilder);
            SeedData.Popular(modelBuilder);
        }

        // Herança Pessoa -> Pecuarista / Veterinario
        private static void ConfigurarHeranca(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pessoa>()
                .ToTable("Pessoas")
                .HasDiscriminator<string>("TipoPessoa")
                .HasValue<Pecuarista>("Pecuarista")
                .HasValue<Veterinario>("Veterinario");

            modelBuilder.Entity<Pessoa>()
                .HasIndex(p => p.Login)
                .IsUnique();
        }

        // Relacionamentos entre as tabelas
        private static void ConfigurarRelacionamentos(ModelBuilder modelBuilder)
        {
            // 1x1 Pecuarista -> Fazenda
            modelBuilder.Entity<Pecuarista>()
                .HasOne(p => p.Fazenda)
                .WithOne(f => f.Pecuarista)
                .HasForeignKey<Fazenda>(f => f.PecuaristaId)
                .IsRequired();

            // 1xN Pecuarista -> Animais
            modelBuilder.Entity<Pecuarista>()
                .HasMany(p => p.Animais)
                .WithOne(a => a.Pecuarista)
                .HasForeignKey(a => a.PecuaristaId)
                .OnDelete(DeleteBehavior.Restrict);

            // Composição Animal -> Exames
            modelBuilder.Entity<Animal>()
                .HasMany(a => a.Exames)
                .WithOne(e => e.Animal)
                .HasForeignKey(e => e.AnimalId)
                .OnDelete(DeleteBehavior.Cascade);

            // Composição Animal -> Vacinações
            modelBuilder.Entity<Animal>()
                .HasMany(a => a.Vacinacoes)
                .WithOne(v => v.Animal)
                .HasForeignKey(v => v.AnimalId)
                .OnDelete(DeleteBehavior.Cascade);

            // NxN Animal <-> Vacina pela Vacinacao
            modelBuilder.Entity<Vacina>()
                .HasMany(v => v.Vacinacoes)
                .WithOne(vc => vc.Vacina)
                .HasForeignKey(vc => vc.VacinaId)
                .OnDelete(DeleteBehavior.Restrict);

            // 1xN Veterinario -> Vacinações
            modelBuilder.Entity<Veterinario>()
                .HasMany(v => v.Vacinacoes)
                .WithOne(vc => vc.Veterinario)
                .HasForeignKey(vc => vc.VeterinarioId)
                .OnDelete(DeleteBehavior.Restrict);

            // 1xN Veterinario -> Exames
            modelBuilder.Entity<Veterinario>()
                .HasMany(v => v.Exames)
                .WithOne(e => e.Veterinario)
                .HasForeignKey(e => e.VeterinarioId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        // Tamanho e tipo das colunas
        private static void ConfigurarColunas(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Animal>(animal =>
            {
                animal.ToTable("Animais");
                animal.Property(a => a.Brinco).HasMaxLength(20);
                animal.Property(a => a.Sexo).HasConversion<string>().HasMaxLength(10);
                animal.Property(a => a.OrigemEntrada).HasConversion<string>().HasMaxLength(15);
                animal.Property(a => a.MotivoSaida).HasConversion<string>().HasMaxLength(10);
                animal.HasIndex(a => new { a.PecuaristaId, a.Brinco }).IsUnique();
            });

            modelBuilder.Entity<Vacina>(vacina =>
            {
                vacina.Property(v => v.TipoProduto).HasConversion<string>().HasMaxLength(15);
                vacina.Property(v => v.PublicoAlvo).HasConversion<string>().HasMaxLength(10);
            });

            modelBuilder.Entity<Exame>(exame =>
            {
                exame.Property(e => e.Tipo).HasConversion<string>().HasMaxLength(15);
                exame.Property(e => e.Resultado).HasConversion<string>().HasMaxLength(15);
            });

            modelBuilder.Entity<Fazenda>().Property(f => f.Uf).HasMaxLength(2);
        }
    }
}
