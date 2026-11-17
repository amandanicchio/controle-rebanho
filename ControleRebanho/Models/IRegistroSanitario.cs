using ControleRebanho.Models.Enums;

namespace ControleRebanho.Models
{
    // Ações sanitárias feitas no animal
    public interface IRegistroSanitario
    {
        Vacinacao Vacinar(Animal animal, Vacina vacina, DateOnly data, string dose);
        Exame RegistrarExame(Animal animal, TipoExame tipo, ResultadoExame resultado, DateOnly dataColeta);
    }
}
