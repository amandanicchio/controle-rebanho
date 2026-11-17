namespace ControleRebanho.Models
{
    // Vacina aplicada em um animal
    public class Vacinacao
    {
        public int Id { get; set; }
        public DateOnly DataAplicacao { get; set; }
        public string Dose { get; set; } = null!;

        public int AnimalId { get; set; }
        public Animal Animal { get; set; } = null!;

        public int VacinaId { get; set; }
        public Vacina Vacina { get; set; } = null!;

        public int VeterinarioId { get; set; }
        public Veterinario Veterinario { get; set; } = null!;
    }
}
