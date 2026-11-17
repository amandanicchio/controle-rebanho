using ControleRebanho.Models.Enums;

namespace ControleRebanho.Models
{
    // Animal do rebanho
    public class Animal
    {
        public int Id { get; set; }
        public string Brinco { get; set; } = null!;
        public Sexo Sexo { get; set; }
        public DateOnly DataNascimento { get; set; }
        public OrigemEntrada OrigemEntrada { get; set; }
        public DateOnly DataEntrada { get; set; }
        public DateOnly? DataSaida { get; set; }
        public MotivoSaida? MotivoSaida { get; set; }

        public int PecuaristaId { get; set; }
        public Pecuarista Pecuarista { get; set; } = null!;

        public ICollection<Vacinacao> Vacinacoes { get; set; } = new List<Vacinacao>();
        public ICollection<Exame> Exames { get; set; } = new List<Exame>();

        // Idade do animal em uma data
        public int IdadeEmMeses(DateOnly data) => FaixaEtaria.CalcularMeses(DataNascimento, data);

        // Categoria do animal pela idade e sexo
        public CategoriaAnimal Categoria(DateOnly data)
        {
            int idade = IdadeEmMeses(data);
            bool macho = Sexo == Sexo.MACHO;

            if (idade <= FaixaEtaria.LimiteBezerroMeses) return macho ? CategoriaAnimal.BEZERRO : CategoriaAnimal.BEZERRA;
            if (idade <= FaixaEtaria.LimiteGarroteMeses) return macho ? CategoriaAnimal.GARROTE : CategoriaAnimal.GARROTA;
            if (idade <= FaixaEtaria.LimiteNovilhoMeses) return macho ? CategoriaAnimal.NOVILHO : CategoriaAnimal.NOVILHA;
            return macho ? CategoriaAnimal.BOI : CategoriaAnimal.VACA;
        }

        // Indica se o animal ainda está no rebanho
        public bool EstaAtivo() => DataSaida == null;

        // Registra a saída do animal por morte ou venda
        public void RegistrarSaida(DateOnly data, MotivoSaida motivo)
        {
            if (!EstaAtivo())
                throw new RegraNegocioException("Este animal já saiu do rebanho.");
            if (data < DataEntrada)
                throw new RegraNegocioException("A data de saída não pode ser anterior à data de entrada.");

            DataSaida = data;
            MotivoSaida = motivo;
        }
    }
}
