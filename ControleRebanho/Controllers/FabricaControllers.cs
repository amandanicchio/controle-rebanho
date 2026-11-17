using ControleRebanho.Repositories;
using ControleRebanho.Utils;

namespace ControleRebanho.Controllers
{
    // Cria os controllers com seus repositórios
    public static class FabricaControllers
    {
        private static readonly IRelogio Relogio = new RelogioSistema();

        public static LoginController Login() => new(new PessoaRepository());
        public static FazendaController Fazenda() => new(new FazendaRepository());
        public static AnimalController Animal() => new(new AnimalRepository(), Relogio);
        public static RelatorioController Relatorio() => new(new RelatorioRepository(), new AnimalRepository());
    }
}
