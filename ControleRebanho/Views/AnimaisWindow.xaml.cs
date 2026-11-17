using System.Windows;
using System.Windows.Controls;
using ControleRebanho.Controllers;
using ControleRebanho.Controllers.Dtos;
using ControleRebanho.Models.Enums;

namespace ControleRebanho.Views
{
    // Tela de listagem dos animais
    public partial class AnimaisWindow : Window
    {
        private readonly AnimalController _controller = FabricaControllers.Animal();

        public AnimaisWindow()
        {
            InitializeComponent();
            CarregarAnimais();
        }

        private void CarregarAnimais()
        {
            GradeAnimais.ItemsSource = _controller.Pesquisar(CampoBrinco.Text, SexoEscolhido(), SituacaoEscolhida());
        }

        private Sexo? SexoEscolhido()
        {
            string tag = (string)((ComboBoxItem)ComboSexo.SelectedItem).Tag;
            return tag == "" ? null : Enum.Parse<Sexo>(tag);
        }

        private FiltroSituacao SituacaoEscolhida()
        {
            string tag = (string)((ComboBoxItem)ComboSituacao.SelectedItem).Tag;
            return Enum.Parse<FiltroSituacao>(tag);
        }

        private AnimalListaItem? AnimalSelecionado()
        {
            if (GradeAnimais.SelectedItem is AnimalListaItem animal) return animal;

            Tela.Aviso("Selecione um animal na lista.");
            return null;
        }

        private void AbrirTela(Window tela)
        {
            tela.Owner = this;
            tela.ShowDialog();
            CarregarAnimais();
        }

        private void Buscar_Click(object sender, RoutedEventArgs e) => CarregarAnimais();

        private void Voltar_Click(object sender, RoutedEventArgs e) => Close();

        private void Novo_Click(object sender, RoutedEventArgs e) => AbrirTela(new AnimalFormWindow());

        private void Editar_Click(object sender, RoutedEventArgs e)
        {
            AnimalListaItem? animal = AnimalSelecionado();
            if (animal != null) AbrirTela(new AnimalFormWindow(animal.Id));
        }

        private void RegistrarSaida_Click(object sender, RoutedEventArgs e)
        {
            AnimalListaItem? animal = AnimalSelecionado();
            if (animal == null) return;

            if (!animal.Ativo)
            {
                Tela.Aviso("Este animal já saiu do rebanho.");
                return;
            }
            AbrirTela(new RegistrarSaidaWindow(animal));
        }

        private void FichaSanitaria_Click(object sender, RoutedEventArgs e)
        {
            AnimalListaItem? animal = AnimalSelecionado();
            if (animal != null) AbrirTela(new FichaSanitariaWindow(animal.Id));
        }
    }
}
