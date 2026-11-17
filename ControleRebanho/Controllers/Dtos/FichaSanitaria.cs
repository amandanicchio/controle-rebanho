namespace ControleRebanho.Controllers.Dtos
{
    public class FichaSanitaria
    {
        public string Titulo { get; init; } = "";
        public string Resumo { get; init; } = "";
        public List<VacinacaoLinha> Vacinacoes { get; init; } = new();
        public List<ExameLinha> Exames { get; init; } = new();
    }

    public class VacinacaoLinha
    {
        public string Data { get; init; } = "";
        public string Produto { get; init; } = "";
        public string Doenca { get; init; } = "";
        public string Dose { get; init; } = "";
        public string Veterinario { get; init; } = "";
    }

    public class ExameLinha
    {
        public string Coleta { get; init; } = "";
        public string Exame { get; init; } = "";
        public string Resultado { get; init; } = "";
        public string Veterinario { get; init; } = "";
    }
}
