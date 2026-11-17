using ControleRebanho.Models;

namespace ControleRebanho.Utils
{
    // Usuário logado no sistema
    public static class Sessao
    {
        public static Pecuarista? PecuaristaLogado { get; private set; }

        public static Pecuarista Pecuarista =>
            PecuaristaLogado ?? throw new InvalidOperationException("Nenhum pecuarista logado.");

        // Inicia a sessão do pecuarista
        public static void Iniciar(Pecuarista pecuarista) => PecuaristaLogado = pecuarista;

        // Encerra a sessão
        public static void Encerrar() => PecuaristaLogado = null;
    }
}
