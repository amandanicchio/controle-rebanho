namespace ControleRebanho.Utils
{
    // Fornece a data de hoje
    public interface IRelogio
    {
        DateOnly Hoje { get; }
    }

    public class RelogioSistema : IRelogio
    {
        public DateOnly Hoje => DateOnly.FromDateTime(DateTime.Today);
    }
}
