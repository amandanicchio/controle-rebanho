using ControleRebanho.Models.Enums;

namespace ControleRebanho.Utils
{
    // Textos mostrados nas telas
    public static class Formatador
    {
        public const string FormatoData = "dd/MM/yyyy";

        public static string Data(DateOnly data) => data.ToString(FormatoData);

        public static string Sexo(Sexo sexo) => sexo == Models.Enums.Sexo.MACHO ? "Macho" : "Fêmea";

        public static string Categoria(CategoriaAnimal categoria) => categoria switch
        {
            CategoriaAnimal.BEZERRO => "Bezerro",
            CategoriaAnimal.GARROTE => "Garrote",
            CategoriaAnimal.NOVILHO => "Novilho",
            CategoriaAnimal.BOI => "Boi",
            CategoriaAnimal.BEZERRA => "Bezerra",
            CategoriaAnimal.GARROTA => "Garrota",
            CategoriaAnimal.NOVILHA => "Novilha",
            _ => "Vaca"
        };

        public static string Origem(OrigemEntrada origem) =>
            origem == OrigemEntrada.NASCIMENTO ? "Nascimento" : "Compra";

        public static string Motivo(MotivoSaida motivo) =>
            motivo == MotivoSaida.VENDA ? "Venda" : "Morte";

        public static string Situacao(MotivoSaida? motivo) => motivo switch
        {
            null => "Ativo",
            MotivoSaida.VENDA => "Vendido",
            _ => "Morto"
        };

        public static string Resultado(ResultadoExame resultado) => resultado switch
        {
            ResultadoExame.NEGATIVO => "Negativo",
            ResultadoExame.POSITIVO => "Positivo",
            _ => "Inconclusivo"
        };

        public static string TipoExame(TipoExame tipo) =>
            tipo == Models.Enums.TipoExame.BRUCELOSE ? "Brucelose" : "Tuberculose";
    }
}
