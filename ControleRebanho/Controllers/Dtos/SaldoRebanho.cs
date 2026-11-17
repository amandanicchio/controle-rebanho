namespace ControleRebanho.Controllers.Dtos
{
    public class SaldoRebanho
    {
        public List<SaldoFaixaLinha> Linhas { get; init; } = new();
        public int TotalMachos { get; init; }
        public int TotalFemeas { get; init; }
        public int Total => TotalMachos + TotalFemeas;
    }

    public class SaldoFaixaLinha
    {
        public string Faixa { get; init; } = "";
        public string Machos { get; init; } = "";
        public string Femeas { get; init; } = "";
    }
}
