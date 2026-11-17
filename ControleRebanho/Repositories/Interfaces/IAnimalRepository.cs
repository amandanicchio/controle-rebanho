using ControleRebanho.Models;
using ControleRebanho.Models.Enums;

namespace ControleRebanho.Repositories.Interfaces
{
    public interface IAnimalRepository
    {
        List<Animal> Pesquisar(int pecuaristaId, string? brinco = null, Sexo? sexo = null, bool? somenteAtivos = null);
        Animal? BuscarPorId(int animalId);
        Animal? BuscarComFichaSanitaria(int animalId);
        bool ExisteBrinco(int pecuaristaId, string brinco, int ignorarAnimalId = 0);
        List<Animal> ListarMovimentadosNoPeriodo(int pecuaristaId, DateOnly de, DateOnly ate);
        void Adicionar(Animal animal);
        void Atualizar(Animal animal);
    }
}
