namespace ControleRebanho.Models
{
    // Classe base dos usuários do sistema
    public abstract class Pessoa
    {
        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public string Cpf { get; set; } = null!;
        public string Login { get; set; } = null!;
        public string SenhaHash { get; set; } = null!;

        public abstract string Perfil();
    }
}
