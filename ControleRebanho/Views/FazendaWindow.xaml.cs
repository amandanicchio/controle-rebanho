using System.Windows;
using ControleRebanho.Controllers;
using ControleRebanho.Models;

namespace ControleRebanho.Views
{
    // Tela Minha fazenda
    public partial class FazendaWindow : Window
    {
        private readonly FazendaController _controller = FabricaControllers.Fazenda();

        public FazendaWindow()
        {
            InitializeComponent();
            CarregarFazenda();
        }

        private void CarregarFazenda()
        {
            Fazenda? fazenda = _controller.ObterFazendaDoPecuarista();
            if (fazenda == null) return;

            CampoNome.Text = fazenda.Nome;
            CampoCodigo.Text = fazenda.CodigoPropriedade;
            CampoUf.Text = fazenda.Uf;
            CampoMunicipio.Text = fazenda.Municipio;
        }

        private void Salvar_Click(object sender, RoutedEventArgs e)
        {
            bool salvou = Tela.Executar(() =>
                _controller.Salvar(CampoNome.Text, CampoCodigo.Text, CampoMunicipio.Text, CampoUf.Text), TextoErro);
            if (!salvou) return;

            Tela.Sucesso("Dados da fazenda salvos.");
            Close();
        }
    }
}
