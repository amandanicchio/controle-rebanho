using ControleRebanho.Models.Enums;

namespace ControleRebanho.Models
{
    // Vacina ou vermífugo do catálogo
    public class Vacina
    {
        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public string Doenca { get; set; } = null!;
        public TipoProduto TipoProduto { get; set; }
        public PublicoAlvo PublicoAlvo { get; set; }
        public int IdadeMinimaMeses { get; set; }
        public int? IdadeMaximaMeses { get; set; }
        public bool Obrigatoria { get; set; }
        public bool Ativa { get; set; }

        public ICollection<Vacinacao> Vacinacoes { get; set; } = new List<Vacinacao>();

        // Verifica se a vacina pode ser aplicada no animal
        public bool PodeAplicar(Animal animal, DateOnly data)
        {
            if (!Ativa) return false;
            if (!AtendePublicoAlvo(animal.Sexo)) return false;

            int idade = animal.IdadeEmMeses(data);
            bool acimaDaMinima = idade >= IdadeMinimaMeses;
            bool abaixoDaMaxima = IdadeMaximaMeses == null || idade <= IdadeMaximaMeses;
            return acimaDaMinima && abaixoDaMaxima;
        }

        // Verifica o sexo permitido para a vacina
        private bool AtendePublicoAlvo(Sexo sexo) => PublicoAlvo switch
        {
            PublicoAlvo.FEMEAS => sexo == Sexo.FEMEA,
            PublicoAlvo.MACHOS => sexo == Sexo.MACHO,
            _ => true
        };
    }
}
