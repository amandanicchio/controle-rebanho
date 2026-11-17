using System.Security.Cryptography;
using System.Text;

namespace ControleRebanho.Utils
{
    public static class SenhaHasher
    {
        // Gera o hash da senha
        public static string Gerar(string senha)
        {
            byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(senha));
            return Convert.ToHexString(bytes);
        }

        // Confere a senha digitada com o hash salvo
        public static bool Conferir(string senhaDigitada, string hashSalvo) =>
            Gerar(senhaDigitada) == hashSalvo;
    }
}
