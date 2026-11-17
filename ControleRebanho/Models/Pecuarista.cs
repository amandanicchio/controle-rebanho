namespace ControleRebanho.Models
{
    // Dono da fazenda e do rebanho
    public class Pecuarista : Pessoa
    {
        public string Telefone { get; set; } = null!;

        public Fazenda? Fazenda { get; set; }

        public ICollection<Animal> Animais { get; set; } = new List<Animal>();

        public override string Perfil() => "Pecuarista";
    }
}
