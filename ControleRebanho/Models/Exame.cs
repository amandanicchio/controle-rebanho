using ControleRebanho.Models.Enums;

namespace ControleRebanho.Models
{
    // Exame feito em um animal
    public class Exame
    {
        public int Id { get; set; }
        public TipoExame Tipo { get; set; }
        public ResultadoExame Resultado { get; set; }
        public DateOnly DataColeta { get; set; }

        public int AnimalId { get; set; }
        public Animal Animal { get; set; } = null!;

        public int VeterinarioId { get; set; }
        public Veterinario Veterinario { get; set; } = null!;
    }
}
