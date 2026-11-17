using System.Windows;
using ControleRebanho.Controllers;
using ControleRebanho.Controllers.Dtos;
using ControleRebanho.Utils;

namespace ControleRebanho.Views
{
    // Tela do saldo do rebanho
    public partial class SaldoRebanhoWindow : Window
    {
        private readonly RelatorioController _controller = FabricaControllers.Relatorio();

        public SaldoRebanhoWindow()
        {
            InitializeComponent();
            CampoData.SelectedDate = DateTime.Today;
            MostrarSaldo();
        }

        private void MostrarSaldo()
        {
            Tela.Executar(() =>
            {
                DateOnly? data = Tela.Data(CampoData);
                SaldoRebanho saldo = _controller.ConsultarSaldo(data);

                TextoTitulo.Text = $"Saldo do rebanho em {Formatador.Data(data!.Value)}";
                GradeSaldo.ItemsSource = saldo.Linhas;
                TextoTotais.Text = $"Machos: {saldo.TotalMachos}    Fêmeas: {saldo.TotalFemeas}";
                TextoTotalGeral.Text = $"Total: {saldo.Total} animais";
            });
        }

        private void VerSaldo_Click(object sender, RoutedEventArgs e) => MostrarSaldo();

        private void Imprimir_Click(object sender, RoutedEventArgs e) => Tela.Imprimir(AreaRelatorio, "Saldo do rebanho");
    }
}
