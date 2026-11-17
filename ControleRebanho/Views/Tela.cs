using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ControleRebanho.Models;

namespace ControleRebanho.Views
{
    // Funções comuns das telas
    public static class Tela
    {
        // Executa uma ação e mostra o erro, se houver
        public static bool Executar(Action acao, TextBlock? textoErro = null)
        {
            try
            {
                acao();
                if (textoErro != null) textoErro.Text = "";
                return true;
            }
            catch (RegraNegocioException ex)
            {
                if (textoErro != null)
                    textoErro.Text = ex.Message;
                else
                    Aviso(ex.Message);
                return false;
            }
        }

        public static void Aviso(string mensagem) =>
            MessageBox.Show(mensagem, "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);

        public static void Sucesso(string mensagem) =>
            MessageBox.Show(mensagem, "Pronto", MessageBoxButton.OK, MessageBoxImage.Information);

        public static bool Confirmar(string pergunta) =>
            MessageBox.Show(pergunta, "Confirmar", MessageBoxButton.YesNo, MessageBoxImage.Question)
                == MessageBoxResult.Yes;

        // Converte a data do DatePicker
        public static DateOnly? Data(DatePicker campo) =>
            campo.SelectedDate is DateTime data ? DateOnly.FromDateTime(data) : null;

        // Imprime uma área da tela
        public static void Imprimir(Visual conteudo, string titulo)
        {
            var dialogo = new PrintDialog();
            if (dialogo.ShowDialog() == true)
                dialogo.PrintVisual(conteudo, titulo);
        }
    }
}
