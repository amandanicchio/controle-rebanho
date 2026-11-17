using System.Windows;
using System.Windows.Controls;
using ControleRebanho.Controllers;
using ControleRebanho.Controllers.Dtos;

namespace ControleRebanho.Views
{
    // Tela da ficha sanitária
    public partial class FichaSanitariaWindow : Window
    {
        private readonly AnimalController _controller = FabricaControllers.Animal();

        public FichaSanitariaWindow(int? animalId = null)
        {
            InitializeComponent();

            List<AnimalListaItem> animais = _controller.Pesquisar(null, null, FiltroSituacao.Todos);
            ComboAnimal.ItemsSource = animais;
            ComboAnimal.SelectedItem = animais.FirstOrDefault(a => a.Id == animalId) ?? animais.FirstOrDefault();
        }

        private void Animal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboAnimal.SelectedItem is not AnimalListaItem animal) return;

            FichaSanitaria ficha = _controller.ObterFichaSanitaria(animal.Id);
            TextoTitulo.Text = ficha.Titulo;
            TextoResumo.Text = ficha.Resumo;
            GradeVacinacoes.ItemsSource = ficha.Vacinacoes;
            GradeExames.ItemsSource = ficha.Exames;
        }
    }
}
