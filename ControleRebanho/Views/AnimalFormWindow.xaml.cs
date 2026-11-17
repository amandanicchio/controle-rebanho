using System.Windows;
using System.Windows.Controls;
using ControleRebanho.Controllers;
using ControleRebanho.Controllers.Dtos;
using ControleRebanho.Models;
using ControleRebanho.Models.Enums;

namespace ControleRebanho.Views
{
    // Tela de cadastro e edição de animal
    public partial class AnimalFormWindow : Window
    {
        private readonly AnimalController _controller = FabricaControllers.Animal();
        private readonly int? _animalIdEmEdicao;

        public AnimalFormWindow(int? animalIdEmEdicao = null)
        {
            InitializeComponent();
            _animalIdEmEdicao = animalIdEmEdicao;

            if (_animalIdEmEdicao != null)
                CarregarAnimal(_animalIdEmEdicao.Value);

            AtualizarTela();
        }

        private void CarregarAnimal(int animalId)
        {
            Animal animal = _controller.ObterAnimal(animalId);

            Title = TextoTitulo.Text = $"Editar animal {animal.Brinco}";
            CampoBrinco.Text = animal.Brinco;
            ComboSexo.SelectedIndex = animal.Sexo == Sexo.MACHO ? 0 : 1;
            OpcaoNascimento.IsChecked = animal.OrigemEntrada == OrigemEntrada.NASCIMENTO;
            OpcaoCompra.IsChecked = animal.OrigemEntrada == OrigemEntrada.COMPRA;
            CampoNascimento.SelectedDate = animal.DataNascimento.ToDateTime(TimeOnly.MinValue);
            CampoEntrada.SelectedDate = animal.DataEntrada.ToDateTime(TimeOnly.MinValue);
        }

        private Sexo SexoEscolhido() =>
            Enum.Parse<Sexo>((string)((ComboBoxItem)ComboSexo.SelectedItem).Tag);

        private OrigemEntrada OrigemEscolhida() =>
            OpcaoCompra.IsChecked == true ? OrigemEntrada.COMPRA : OrigemEntrada.NASCIMENTO;

        private void DadosAlterados(object sender, RoutedEventArgs e)
        {
            if (IsInitialized) AtualizarTela();
        }

        private void AtualizarTela()
        {
            bool nasceuNaFazenda = OrigemEscolhida() == OrigemEntrada.NASCIMENTO;
            CampoEntrada.IsEnabled = !nasceuNaFazenda;
            if (nasceuNaFazenda)
                CampoEntrada.SelectedDate = CampoNascimento.SelectedDate;

            string previa = _controller.PreverIdadeECategoria(SexoEscolhido(), Tela.Data(CampoNascimento));
            if (previa != "") TextoPrevia.Text = previa;
        }

        private AnimalFormulario MontarFormulario() => new()
        {
            Brinco = CampoBrinco.Text,
            Sexo = SexoEscolhido(),
            Origem = OrigemEscolhida(),
            DataNascimento = Tela.Data(CampoNascimento),
            DataEntrada = Tela.Data(CampoEntrada)
        };

        private void Salvar_Click(object sender, RoutedEventArgs e)
        {
            AnimalFormulario formulario = MontarFormulario();

            bool salvou = Tela.Executar(() =>
            {
                if (_animalIdEmEdicao == null)
                    _controller.Cadastrar(formulario);
                else
                    _controller.Editar(_animalIdEmEdicao.Value, formulario);
            }, TextoErro);

            if (salvou) Close();
        }
    }
}
