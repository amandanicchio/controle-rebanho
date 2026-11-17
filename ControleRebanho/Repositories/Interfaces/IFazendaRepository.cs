using ControleRebanho.Models;

namespace ControleRebanho.Repositories.Interfaces
{
    public interface IFazendaRepository
    {
        Fazenda? BuscarPorPecuarista(int pecuaristaId);
        void Salvar(Fazenda fazenda);
    }
}
