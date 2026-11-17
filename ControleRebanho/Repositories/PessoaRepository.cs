using ControleRebanho.Data;
using ControleRebanho.Models;
using ControleRebanho.Repositories.Interfaces;

namespace ControleRebanho.Repositories
{
    public class PessoaRepository : IPessoaRepository
    {
        // Busca o usuário pelo login
        public Pessoa? BuscarPorLogin(string login)
        {
            using var context = new RebanhoDbContext();
            return context.Pessoas.FirstOrDefault(p => p.Login == login);
        }
    }
}
