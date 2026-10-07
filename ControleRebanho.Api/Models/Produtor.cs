namespace ControleRebanho.Api.Models
{
    public class Produtor
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public string? Telefone { get; set; }
    }
}