using ControleRebanho.Data;
using ControleRebanho.Models;
using ControleRebanho.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ControleRebanho.Repositories
{
    public class RelatorioRepository : IRelatorioRepository
    {
        // Saldo do rebanho por sexo e categoria (SQL puro)
        public List<SaldoCategoriaLinha> ConsultarSaldoRebanho(int pecuaristaId, DateOnly data)
        {
            using var context = new RebanhoDbContext();

            int limiteBezerro = FaixaEtaria.LimiteBezerroMeses;
            int limiteGarrote = FaixaEtaria.LimiteGarroteMeses;
            int limiteNovilho = FaixaEtaria.LimiteNovilhoMeses;

            return context.Database.SqlQuery<SaldoCategoriaLinha>($@"
                SELECT t.Sexo, t.Categoria, COUNT(*) AS Quantidade
                FROM (
                    SELECT a.Sexo,
                        CASE
                            WHEN i.Meses <= {limiteBezerro} THEN CASE WHEN a.Sexo = 'MACHO' THEN 'BEZERRO' ELSE 'BEZERRA' END
                            WHEN i.Meses <= {limiteGarrote} THEN CASE WHEN a.Sexo = 'MACHO' THEN 'GARROTE' ELSE 'GARROTA' END
                            WHEN i.Meses <= {limiteNovilho} THEN CASE WHEN a.Sexo = 'MACHO' THEN 'NOVILHO' ELSE 'NOVILHA' END
                            ELSE CASE WHEN a.Sexo = 'MACHO' THEN 'BOI' ELSE 'VACA' END
                        END AS Categoria
                    FROM Animais a
                    CROSS APPLY (
                        SELECT DATEDIFF(MONTH, a.DataNascimento, {data})
                             - CASE WHEN DAY({data}) < DAY(a.DataNascimento) THEN 1 ELSE 0 END AS Meses
                    ) i
                    WHERE a.PecuaristaId = {pecuaristaId}
                      AND a.DataNascimento <= {data}
                      AND a.DataEntrada <= {data}
                      AND (a.DataSaida IS NULL OR a.DataSaida > {data})
                ) t
                GROUP BY t.Sexo, t.Categoria")
                .ToList();
        }
    }
}
