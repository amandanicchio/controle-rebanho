using ControleRebanho.Models;

namespace ControleRebanho.Repositories.Interfaces
{
    public interface IPessoaRepository
    {
        Pessoa? BuscarPorLogin(string login);
    }
}
