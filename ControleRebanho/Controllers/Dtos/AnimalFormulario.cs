using ControleRebanho.Models.Enums;

namespace ControleRebanho.Controllers.Dtos
{
    public class AnimalFormulario
    {
        public string Brinco { get; init; } = "";
        public Sexo Sexo { get; init; }
        public OrigemEntrada Origem { get; init; }
        public DateOnly? DataNascimento { get; init; }
        public DateOnly? DataEntrada { get; init; }
    }
}
