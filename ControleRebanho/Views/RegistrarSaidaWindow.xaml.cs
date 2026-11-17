using System.Windows;
using System.Windows.Controls;
using ControleRebanho.Controllers;
using ControleRebanho.Controllers.Dtos;
using ControleRebanho.Models.Enums;

namespace ControleRebanho.Views
{
    // Tela de saída do animal
    public partial class RegistrarSaidaWindow : Window
    {
        private readonly AnimalController _controller = FabricaControllers.Animal();
        private readonly AnimalListaItem _animal;

        public RegistrarSaidaWindow(AnimalListaItem animal)
        {
            InitializeComponent();
            _animal = animal;

            TextoTitulo.Text = $"Saída do animal {animal.Brinco}";
            TextoResumo.Text = $"{animal.Sexo} · {animal.Idade} · {animal.Categoria}";
            CampoData.SelectedDate = DateTime.Today;
        }

        private MotivoSaida MotivoEscolhido() =>
            Enum.Parse<MotivoSaida>((string)((ComboBoxItem)ComboMotivo.SelectedItem).Tag);

        private void Confirmar_Click(object sender, RoutedEventArgs e)
        {
            if (!Tela.Confirmar($"Confirmar a saída do animal {_animal.Brinco}?")) return;

            bool registrou = Tela.Executar(() =>
                _controller.RegistrarSaida(_animal.Id, Tela.Data(CampoData), MotivoEscolhido()), TextoErro);

            if (registrou) Close();
        }
    }
}
