namespace ControleRebanho.Controllers.Dtos
{
    public class ExtratoMovimentacao
    {
        public List<MovimentacaoLinha> Linhas { get; init; } = new();
        public int Entradas { get; init; }
        public int Saidas { get; init; }
    }

    public class MovimentacaoLinha
    {
        public DateOnly DataOrdenacao { get; init; }
        public string Data { get; init; } = "";
        public string Brinco { get; init; } = "";
        public string Movimento { get; init; } = "";
        public string Motivo { get; init; } = "";
        public string Categoria { get; init; } = "";
    }
}
