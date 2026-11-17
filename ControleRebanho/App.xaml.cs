using System.Globalization;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Threading;

namespace ControleRebanho
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            UsarCulturaBrasileira();
            DispatcherUnhandledException += MostrarErroInesperado;
            base.OnStartup(e);
        }

        // Datas no formato brasileiro
        private static void UsarCulturaBrasileira()
        {
            var cultura = new CultureInfo("pt-BR");
            CultureInfo.DefaultThreadCurrentCulture = cultura;
            CultureInfo.DefaultThreadCurrentUICulture = cultura;
            FrameworkElement.LanguageProperty.OverrideMetadata(
                typeof(FrameworkElement),
                new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(cultura.IetfLanguageTag)));
        }

        // Mostra erros inesperados sem fechar o sistema
        private void MostrarErroInesperado(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(
                $"Ocorreu um erro inesperado:\n\n{e.Exception.GetBaseException().Message}",
                "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
        }
    }
}
