using System.Text.Json.Serialization;

namespace ControleRebanho.Api.Models
{
    public class Propriedade
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Municipio { get; set; } = string.Empty;

        public int ProdutorId { get; set; }

        [JsonIgnore]
        public Produtor? Produtor { get; set; }
    }
}