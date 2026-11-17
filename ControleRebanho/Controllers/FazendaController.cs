using ControleRebanho.Models;
using ControleRebanho.Repositories.Interfaces;
using ControleRebanho.Utils;

namespace ControleRebanho.Controllers
{
    public class FazendaController
    {
        private const int TamanhoUf = 2;
        private readonly IFazendaRepository _fazendaRepository;

        public FazendaController(IFazendaRepository fazendaRepository)
        {
            _fazendaRepository = fazendaRepository;
        }

        // Busca a fazenda do usuário logado
        public Fazenda? ObterFazendaDoPecuarista() =>
            _fazendaRepository.BuscarPorPecuarista(Sessao.Pecuarista.Id);

        // Salva os dados da fazenda
        public void Salvar(string nome, string codigoPropriedade, string municipio, string uf)
        {
            ValidarCampos(nome, codigoPropriedade, municipio, uf);

            Fazenda fazenda = ObterFazendaDoPecuarista() ?? new Fazenda { PecuaristaId = Sessao.Pecuarista.Id };
            fazenda.Nome = nome.Trim();
            fazenda.CodigoPropriedade = codigoPropriedade.Trim();
            fazenda.Municipio = municipio.Trim();
            fazenda.Uf = uf.Trim().ToUpper();

            _fazendaRepository.Salvar(fazenda);
        }

        // Valida os campos da fazenda
        private static void ValidarCampos(string nome, string codigo, string municipio, string uf)
        {
            if (string.IsNullOrWhiteSpace(nome)) throw new RegraNegocioException("Informe o nome da fazenda.");
            if (string.IsNullOrWhiteSpace(codigo)) throw new RegraNegocioException("Informe o código da propriedade.");
            if (string.IsNullOrWhiteSpace(municipio)) throw new RegraNegocioException("Informe o município.");
            if (uf.Trim().Length != TamanhoUf) throw new RegraNegocioException("A UF deve ter 2 letras.");
        }
    }
}
