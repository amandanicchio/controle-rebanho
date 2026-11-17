namespace ControleRebanho.Models
{
    // Limites de idade de cada categoria
    public static class FaixaEtaria
    {
        public const int LimiteBezerroMeses = 12;
        public const int LimiteGarroteMeses = 24;
        public const int LimiteNovilhoMeses = 36;

        // Calcula a idade em meses completos
        public static int CalcularMeses(DateOnly nascimento, DateOnly data)
        {
            int meses = (data.Year - nascimento.Year) * 12 + data.Month - nascimento.Month;
            bool aindaNaoCompletouOMes = data.Day < nascimento.Day;
            return aindaNaoCompletouOMes ? meses - 1 : meses;
        }
    }
}
