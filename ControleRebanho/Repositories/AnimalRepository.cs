using ControleRebanho.Data;
using ControleRebanho.Models;
using ControleRebanho.Models.Enums;
using ControleRebanho.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ControleRebanho.Repositories
{
    public class AnimalRepository : IAnimalRepository
    {
        // Pesquisa animais com filtros
        public List<Animal> Pesquisar(int pecuaristaId, string? brinco = null, Sexo? sexo = null, bool? somenteAtivos = null)
        {
            using var context = new RebanhoDbContext();
            IQueryable<Animal> consulta = context.Animais
                .AsNoTracking()
                .Where(a => a.PecuaristaId == pecuaristaId);

            if (!string.IsNullOrWhiteSpace(brinco))
                consulta = consulta.Where(a => a.Brinco.Contains(brinco));

            if (sexo != null)
                consulta = consulta.Where(a => a.Sexo == sexo);

            if (somenteAtivos == true)
                consulta = consulta.Where(a => a.DataSaida == null);
            else if (somenteAtivos == false)
                consulta = consulta.Where(a => a.DataSaida != null);

            return consulta.OrderBy(a => a.Brinco).ToList();
        }

        // Busca um animal pelo id
        public Animal? BuscarPorId(int animalId)
        {
            using var context = new RebanhoDbContext();
            return context.Animais.AsNoTracking().FirstOrDefault(a => a.Id == animalId);
        }

        // Busca o animal com vacinas e exames
        public Animal? BuscarComFichaSanitaria(int animalId)
        {
            using var context = new RebanhoDbContext();
            return context.Animais
                .AsNoTracking()
                .Include(a => a.Vacinacoes).ThenInclude(v => v.Vacina)
                .Include(a => a.Vacinacoes).ThenInclude(v => v.Veterinario)
                .Include(a => a.Exames).ThenInclude(e => e.Veterinario)
                .AsSplitQuery()
                .FirstOrDefault(a => a.Id == animalId);
        }

        // Verifica se o brinco já existe
        public bool ExisteBrinco(int pecuaristaId, string brinco, int ignorarAnimalId = 0)
        {
            using var context = new RebanhoDbContext();
            return context.Animais.Any(a =>
                a.PecuaristaId == pecuaristaId &&
                a.Brinco == brinco &&
                a.Id != ignorarAnimalId);
        }

        // Animais que entraram ou saíram no período
        public List<Animal> ListarMovimentadosNoPeriodo(int pecuaristaId, DateOnly de, DateOnly ate)
        {
            using var context = new RebanhoDbContext();
            return context.Animais
                .AsNoTracking()
                .Where(a => a.PecuaristaId == pecuaristaId)
                .Where(a => (a.DataEntrada >= de && a.DataEntrada <= ate) ||
                            (a.DataSaida >= de && a.DataSaida <= ate))
                .ToList();
        }

        // Cadastra um animal
        public void Adicionar(Animal animal)
        {
            using var context = new RebanhoDbContext();
            context.Animais.Add(animal);
            context.SaveChanges();
        }

        // Atualiza um animal
        public void Atualizar(Animal animal)
        {
            using var context = new RebanhoDbContext();
            context.Animais.Update(animal);
            context.SaveChanges();
        }
    }
}
