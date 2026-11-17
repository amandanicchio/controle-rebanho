namespace ControleRebanho.Models
{
    // Fazenda do pecuarista
    public class Fazenda
    {
        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public string CodigoPropriedade { get; set; } = null!;
        public string Municipio { get; set; } = null!;
        public string Uf { get; set; } = null!;

        public int PecuaristaId { get; set; }
        public Pecuarista Pecuarista { get; set; } = null!;
    }
}
