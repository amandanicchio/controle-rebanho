namespace ControleRebanho.Controllers.Dtos
{
    public class AnimalListaItem
    {
        public int Id { get; init; }
        public string Brinco { get; init; } = "";
        public string Sexo { get; init; } = "";
        public string Nascimento { get; init; } = "";
        public string Idade { get; init; } = "";
        public string Categoria { get; init; } = "";
        public string Entrada { get; init; } = "";
        public string Situacao { get; init; } = "";
        public bool Ativo { get; init; }
    }
}
