namespace ControleRebanho.Models
{
    // Erro de regra de negócio mostrado ao usuário
    public class RegraNegocioException : Exception
    {
        public RegraNegocioException(string mensagem) : base(mensagem) { }
    }
}
