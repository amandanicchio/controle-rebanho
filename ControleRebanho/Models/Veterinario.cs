using ControleRebanho.Models.Enums;

namespace ControleRebanho.Models
{
    // Responsável pelas vacinas e exames
    public class Veterinario : Pessoa, IRegistroSanitario
    {
        public string Crmv { get; set; } = null!;

        public ICollection<Vacinacao> Vacinacoes { get; set; } = new List<Vacinacao>();
        public ICollection<Exame> Exames { get; set; } = new List<Exame>();

        public override string Perfil() => "Veterinário";

        // Registra a aplicação de uma vacina no animal
        public Vacinacao Vacinar(Animal animal, Vacina vacina, DateOnly data, string dose)
        {
            if (!vacina.PodeAplicar(animal, data))
                throw new RegraNegocioException($"A vacina {vacina.Nome} não pode ser aplicada neste animal.");

            return new Vacinacao
            {
                AnimalId = animal.Id,
                VacinaId = vacina.Id,
                VeterinarioId = Id,
                DataAplicacao = data,
                Dose = dose
            };
        }

        // Registra um exame feito no animal
        public Exame RegistrarExame(Animal animal, TipoExame tipo, ResultadoExame resultado, DateOnly dataColeta)
        {
            return new Exame
            {
                AnimalId = animal.Id,
                VeterinarioId = Id,
                Tipo = tipo,
                Resultado = resultado,
                DataColeta = dataColeta
            };
        }
    }
}
