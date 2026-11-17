using ControleRebanho.Controllers.Dtos;
using ControleRebanho.Models;
using ControleRebanho.Models.Enums;
using ControleRebanho.Repositories.Interfaces;
using ControleRebanho.Utils;

namespace ControleRebanho.Controllers
{
    public class AnimalController
    {
        private readonly IAnimalRepository _animalRepository;
        private readonly IRelogio _relogio;

        public AnimalController(IAnimalRepository animalRepository, IRelogio relogio)
        {
            _animalRepository = animalRepository;
            _relogio = relogio;
        }

        // Lista os animais da tela Animais
        public List<AnimalListaItem> Pesquisar(string? brinco, Sexo? sexo, FiltroSituacao situacao)
        {
            bool? somenteAtivos = situacao switch
            {
                FiltroSituacao.Ativos => true,
                FiltroSituacao.Saidos => false,
                _ => null
            };

            return _animalRepository
                .Pesquisar(Sessao.Pecuarista.Id, brinco?.Trim(), sexo, somenteAtivos)
                .Select(MontarItemDaLista)
                .ToList();
        }

        // Busca um animal pelo id
        public Animal ObterAnimal(int animalId) =>
            _animalRepository.BuscarPorId(animalId)
            ?? throw new RegraNegocioException("Animal não encontrado.");

        // Mostra idade e categoria antes de salvar
        public string PreverIdadeECategoria(Sexo sexo, DateOnly? nascimento)
        {
            if (nascimento == null || nascimento > _relogio.Hoje) return "";

            var animalTemporario = new Animal { Sexo = sexo, DataNascimento = nascimento.Value };
            DateOnly hoje = _relogio.Hoje;
            return $"Idade: {animalTemporario.IdadeEmMeses(hoje)} meses · " +
                   $"Categoria: {Formatador.Categoria(animalTemporario.Categoria(hoje))}";
        }

        // Cadastra um animal
        public void Cadastrar(AnimalFormulario formulario)
        {
            ValidarFormulario(formulario, animalIdEmEdicao: 0);

            var animal = new Animal { PecuaristaId = Sessao.Pecuarista.Id };
            PreencherAnimal(animal, formulario);
            _animalRepository.Adicionar(animal);
        }

        // Edita um animal
        public void Editar(int animalId, AnimalFormulario formulario)
        {
            ValidarFormulario(formulario, animalIdEmEdicao: animalId);

            Animal animal = ObterAnimal(animalId);
            PreencherAnimal(animal, formulario);
            _animalRepository.Atualizar(animal);
        }

        // Registra a saída do animal
        public void RegistrarSaida(int animalId, DateOnly? data, MotivoSaida motivo)
        {
            if (data == null) throw new RegraNegocioException("Informe a data de saída.");
            if (data > _relogio.Hoje) throw new RegraNegocioException("A data de saída não pode ser no futuro.");

            Animal animal = ObterAnimal(animalId);
            animal.RegistrarSaida(data.Value, motivo);
            _animalRepository.Atualizar(animal);
        }

        // Monta a ficha sanitária do animal
        public FichaSanitaria ObterFichaSanitaria(int animalId)
        {
            Animal animal = _animalRepository.BuscarComFichaSanitaria(animalId)
                ?? throw new RegraNegocioException("Animal não encontrado.");
            DateOnly hoje = _relogio.Hoje;

            return new FichaSanitaria
            {
                Titulo = $"Ficha sanitária - {animal.Brinco}",
                Resumo = $"{Formatador.Sexo(animal.Sexo)} · nasceu em {Formatador.Data(animal.DataNascimento)} · " +
                         $"{animal.IdadeEmMeses(hoje)} meses · {Formatador.Categoria(animal.Categoria(hoje))}",
                Vacinacoes = animal.Vacinacoes
                    .OrderByDescending(v => v.DataAplicacao)
                    .Select(v => new VacinacaoLinha
                    {
                        Data = Formatador.Data(v.DataAplicacao),
                        Produto = v.Vacina.Nome,
                        Doenca = v.Vacina.Doenca,
                        Dose = v.Dose,
                        Veterinario = $"{v.Veterinario.Nome} - {v.Veterinario.Crmv}"
                    }).ToList(),
                Exames = animal.Exames
                    .OrderByDescending(e => e.DataColeta)
                    .Select(e => new ExameLinha
                    {
                        Coleta = Formatador.Data(e.DataColeta),
                        Exame = Formatador.TipoExame(e.Tipo),
                        Resultado = Formatador.Resultado(e.Resultado),
                        Veterinario = $"{e.Veterinario.Nome} - {e.Veterinario.Crmv}"
                    }).ToList()
            };
        }

        // Valida os dados do animal
        private void ValidarFormulario(AnimalFormulario formulario, int animalIdEmEdicao)
        {
            if (string.IsNullOrWhiteSpace(formulario.Brinco))
                throw new RegraNegocioException("Informe o brinco do animal.");
            if (formulario.DataNascimento == null)
                throw new RegraNegocioException("Informe a data de nascimento.");
            if (formulario.DataNascimento > _relogio.Hoje)
                throw new RegraNegocioException("A data de nascimento não pode ser no futuro.");

            if (formulario.Origem == OrigemEntrada.COMPRA)
                ValidarDataDeCompra(formulario);

            if (_animalRepository.ExisteBrinco(Sessao.Pecuarista.Id, formulario.Brinco.Trim().ToUpper(), animalIdEmEdicao))
                throw new RegraNegocioException("Já existe um animal com esse brinco.");
        }

        // Valida a data de entrada do animal comprado
        private void ValidarDataDeCompra(AnimalFormulario formulario)
        {
            if (formulario.DataEntrada == null)
                throw new RegraNegocioException("Informe a data de entrada (compra).");
            if (formulario.DataEntrada < formulario.DataNascimento)
                throw new RegraNegocioException("A data de entrada não pode ser antes do nascimento.");
            if (formulario.DataEntrada > _relogio.Hoje)
                throw new RegraNegocioException("A data de entrada não pode ser no futuro.");
        }

        // Copia os dados da tela para o animal
        private static void PreencherAnimal(Animal animal, AnimalFormulario formulario)
        {
            DateOnly nascimento = formulario.DataNascimento!.Value;

            animal.Brinco = formulario.Brinco.Trim().ToUpper();
            animal.Sexo = formulario.Sexo;
            animal.OrigemEntrada = formulario.Origem;
            animal.DataNascimento = nascimento;
            animal.DataEntrada = formulario.Origem == OrigemEntrada.NASCIMENTO
                ? nascimento
                : formulario.DataEntrada!.Value;
        }

        // Monta uma linha da lista de animais
        private AnimalListaItem MontarItemDaLista(Animal animal)
        {
            DateOnly referencia = animal.DataSaida ?? _relogio.Hoje;

            return new AnimalListaItem
            {
                Id = animal.Id,
                Brinco = animal.Brinco,
                Sexo = Formatador.Sexo(animal.Sexo),
                Nascimento = Formatador.Data(animal.DataNascimento),
                Idade = $"{animal.IdadeEmMeses(referencia)} m",
                Categoria = Formatador.Categoria(animal.Categoria(referencia)),
                Entrada = Formatador.Origem(animal.OrigemEntrada),
                Situacao = Formatador.Situacao(animal.MotivoSaida),
                Ativo = animal.EstaAtivo()
            };
        }
    }
}
