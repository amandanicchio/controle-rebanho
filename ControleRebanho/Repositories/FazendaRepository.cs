using ControleRebanho.Data;
using ControleRebanho.Models;
using ControleRebanho.Repositories.Interfaces;

namespace ControleRebanho.Repositories
{
    public class FazendaRepository : IFazendaRepository
    {
        // Busca a fazenda do pecuarista
        public Fazenda? BuscarPorPecuarista(int pecuaristaId)
        {
            using var context = new RebanhoDbContext();
            return context.Fazendas.FirstOrDefault(f => f.PecuaristaId == pecuaristaId);
        }

        // Cadastra ou atualiza a fazenda
        public void Salvar(Fazenda fazenda)
        {
            using var context = new RebanhoDbContext();

            bool fazendaNova = fazenda.Id == 0;
            if (fazendaNova)
                context.Fazendas.Add(fazenda);
            else
                context.Fazendas.Update(fazenda);

            context.SaveChanges();
        }
    }
}
