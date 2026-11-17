using ControleRebanho.Models;
using ControleRebanho.Repositories.Interfaces;
using ControleRebanho.Utils;

namespace ControleRebanho.Controllers
{
    public class LoginController
    {
        private readonly IPessoaRepository _pessoaRepository;

        public LoginController(IPessoaRepository pessoaRepository)
        {
            _pessoaRepository = pessoaRepository;
        }

        // Faz o login do pecuarista
        public Pecuarista Entrar(string login, string senha)
        {
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(senha))
                throw new RegraNegocioException("Informe o login e a senha.");

            Pessoa? pessoa = _pessoaRepository.BuscarPorLogin(login.Trim());
            if (pessoa == null || !SenhaHasher.Conferir(senha, pessoa.SenhaHash))
                throw new RegraNegocioException("Login ou senha inválidos.");

            if (pessoa is not Pecuarista pecuarista)
                throw new RegraNegocioException($"O perfil {pessoa.Perfil()} não tem telas nesta versão do sistema.");

            Sessao.Iniciar(pecuarista);
            return pecuarista;
        }

        // Faz o logout
        public void Sair() => Sessao.Encerrar();
    }
}
