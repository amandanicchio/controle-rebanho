using ControleRebanho.Controllers.Dtos;
using ControleRebanho.Models;
using ControleRebanho.Models.Enums;
using ControleRebanho.Repositories;
using ControleRebanho.Repositories.Interfaces;
using ControleRebanho.Utils;

namespace ControleRebanho.Controllers
{
    public class RelatorioController
    {
        private readonly IRelatorioRepository _relatorioRepository;
        private readonly IAnimalRepository _animalRepository;

        // Faixas de idade do saldo
        private static readonly (string Faixa, CategoriaAnimal Macho, CategoriaAnimal Femea)[] Faixas =
        {
            ($"0 a {FaixaEtaria.LimiteBezerroMeses} m", CategoriaAnimal.BEZERRO, CategoriaAnimal.BEZERRA),
            ($"{FaixaEtaria.LimiteBezerroMeses + 1} a {FaixaEtaria.LimiteGarroteMeses} m", CategoriaAnimal.GARROTE, CategoriaAnimal.GARROTA),
            ($"{FaixaEtaria.LimiteGarroteMeses + 1} a {FaixaEtaria.LimiteNovilhoMeses} m", CategoriaAnimal.NOVILHO, CategoriaAnimal.NOVILHA),
            ($"acima de {FaixaEtaria.LimiteNovilhoMeses} m", CategoriaAnimal.BOI, CategoriaAnimal.VACA)
        };

        public RelatorioController(IRelatorioRepository relatorioRepository, IAnimalRepository animalRepository)
        {
            _relatorioRepository = relatorioRepository;
            _animalRepository = animalRepository;
        }

        // Saldo do rebanho em uma data
        public SaldoRebanho ConsultarSaldo(DateOnly? data)
        {
            if (data == null) throw new RegraNegocioException("Informe a data do saldo.");

            List<SaldoCategoriaLinha> resultado = _relatorioRepository.ConsultarSaldoRebanho(Sessao.Pecuarista.Id, data.Value);

            int Quantidade(CategoriaAnimal categoria) =>
                resultado.Where(r => r.Categoria == categoria.ToString()).Sum(r => r.Quantidade);

            return new SaldoRebanho
            {
                Linhas = Faixas.Select(f => new SaldoFaixaLinha
                {
                    Faixa = f.Faixa,
                    Machos = $"{Formatador.Categoria(f.Macho)}: {Quantidade(f.Macho)}",
                    Femeas = $"{Formatador.Categoria(f.Femea)}: {Quantidade(f.Femea)}"
                }).ToList(),
                TotalMachos = resultado.Where(r => r.Sexo == nameof(Sexo.MACHO)).Sum(r => r.Quantidade),
                TotalFemeas = resultado.Where(r => r.Sexo == nameof(Sexo.FEMEA)).Sum(r => r.Quantidade)
            };
        }

        // Entradas e saídas no período
        public ExtratoMovimentacao ConsultarExtrato(DateOnly? de, DateOnly? ate)
        {
            if (de == null || ate == null) throw new RegraNegocioException("Informe o período.");
            if (de > ate) throw new RegraNegocioException("A data inicial não pode ser maior que a final.");

            List<Animal> animais = _animalRepository.ListarMovimentadosNoPeriodo(Sessao.Pecuarista.Id, de.Value, ate.Value);

            List<MovimentacaoLinha> entradas = animais
                .Where(a => a.DataEntrada >= de && a.DataEntrada <= ate)
                .Select(a => NovaLinha(a, a.DataEntrada, "Entrada", Formatador.Origem(a.OrigemEntrada)))
                .ToList();

            List<MovimentacaoLinha> saidas = animais
                .Where(a => a.DataSaida >= de && a.DataSaida <= ate)
                .Select(a => NovaLinha(a, a.DataSaida!.Value, "Saída", Formatador.Motivo(a.MotivoSaida!.Value)))
                .ToList();

            return new ExtratoMovimentacao
            {
                Linhas = entradas.Concat(saidas).OrderBy(l => l.DataOrdenacao).ThenBy(l => l.Brinco).ToList(),
                Entradas = entradas.Count,
                Saidas = saidas.Count
            };
        }

        // Monta uma linha do extrato
        private static MovimentacaoLinha NovaLinha(Animal animal, DateOnly data, string movimento, string motivo) =>
            new MovimentacaoLinha
            {
                DataOrdenacao = data,
                Data = Formatador.Data(data),
                Brinco = animal.Brinco,
                Movimento = movimento,
                Motivo = motivo,
                Categoria = Formatador.Categoria(animal.Categoria(data))
            };
    }
}
