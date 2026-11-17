using System.Windows;
using ControleRebanho.Controllers;
using ControleRebanho.Utils;

namespace ControleRebanho.Views
{
    // Menu do pecuarista
    public partial class MenuPecuaristaWindow : Window
    {
        public MenuPecuaristaWindow()
        {
            InitializeComponent();
            TextoSaudacao.Text = $"Olá, {Sessao.Pecuarista.Nome} ({Sessao.Pecuarista.Perfil()})";
        }

        private void AbrirTela(Window tela)
        {
            tela.Owner = this;
            tela.ShowDialog();
        }

        private void MinhaFazenda_Click(object sender, RoutedEventArgs e) => AbrirTela(new FazendaWindow());
        private void Animais_Click(object sender, RoutedEventArgs e) => AbrirTela(new AnimaisWindow());
        private void Saldo_Click(object sender, RoutedEventArgs e) => AbrirTela(new SaldoRebanhoWindow());
        private void Extrato_Click(object sender, RoutedEventArgs e) => AbrirTela(new ExtratoMovimentacaoWindow());
        private void FichaSanitaria_Click(object sender, RoutedEventArgs e) => AbrirTela(new FichaSanitariaWindow());

        private void Sair_Click(object sender, RoutedEventArgs e)
        {
            FabricaControllers.Login().Sair();
            new LoginWindow().Show();
            Close();
        }
    }
}
