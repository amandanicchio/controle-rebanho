namespace ControleRebanho.Repositories.Interfaces
{
    public interface IRelatorioRepository
    {
        List<SaldoCategoriaLinha> ConsultarSaldoRebanho(int pecuaristaId, DateOnly data);
    }
}
