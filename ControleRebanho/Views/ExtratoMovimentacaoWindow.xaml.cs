using System.Windows;
using ControleRebanho.Controllers;
using ControleRebanho.Controllers.Dtos;
using ControleRebanho.Utils;

namespace ControleRebanho.Views
{
    // Tela do extrato de movimentação
    public partial class ExtratoMovimentacaoWindow : Window
    {
        private readonly RelatorioController _controller = FabricaControllers.Relatorio();

        public ExtratoMovimentacaoWindow()
        {
            InitializeComponent();

            DateTime hoje = DateTime.Today;
            CampoDe.SelectedDate = new DateTime(hoje.Year, hoje.Month, 1).AddMonths(-1);
            CampoAte.SelectedDate = hoje;
            MostrarExtrato();
        }

        private void MostrarExtrato()
        {
            Tela.Executar(() =>
            {
                DateOnly? de = Tela.Data(CampoDe);
                DateOnly? ate = Tela.Data(CampoAte);
                ExtratoMovimentacao extrato = _controller.ConsultarExtrato(de, ate);

                TextoTitulo.Text = $"Extrato de {Formatador.Data(de!.Value)} a {Formatador.Data(ate!.Value)}";
                GradeExtrato.ItemsSource = extrato.Linhas;
                TextoResumo.Text = $"Entradas: {extrato.Entradas}    Saídas: {extrato.Saidas}";
            });
        }

        private void Filtrar_Click(object sender, RoutedEventArgs e) => MostrarExtrato();

        private void Imprimir_Click(object sender, RoutedEventArgs e) =>
            Tela.Imprimir(AreaRelatorio, "Extrato de movimentação");
    }
}
