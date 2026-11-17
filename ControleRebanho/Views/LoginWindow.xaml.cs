using System.Windows;
using ControleRebanho.Controllers;

namespace ControleRebanho.Views
{
    // Tela de login
    public partial class LoginWindow : Window
    {
        private readonly LoginController _controller = FabricaControllers.Login();

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Entrar_Click(object sender, RoutedEventArgs e)
        {
            bool entrou = Tela.Executar(() => _controller.Entrar(CampoLogin.Text, CampoSenha.Password), TextoErro);
            if (!entrou) return;

            new MenuPecuaristaWindow().Show();
            Close();
        }
    }
}
