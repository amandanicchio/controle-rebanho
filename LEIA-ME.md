# Sistema de Controle de Rebanho — WPF + EF Core (Code First)

## Como rodar (Visual Studio, .NET 10)

1. Abra `ControleRebanho.slnx` (ou o `ControleRebanho.csproj`). O Visual Studio restaura os pacotes NuGet sozinho:
   - `Microsoft.EntityFrameworkCore.SqlServer`
   - `Microsoft.EntityFrameworkCore.Design`
   - `Microsoft.EntityFrameworkCore.Tools`
2. **Ferramentas → Gerenciador de Pacotes NuGet → Console do Gerenciador de Pacotes**:
   ```
   Add-Migration VersaoInicial
   Update-Database
   ```
   O EF cria o banco `ControleRebanhoDB` no LocalDB `(localdb)\mssqllocaldb`, com tabelas,
   relacionamentos e os dados iniciais (seed).
3. `Ctrl + F5`. Login: **joao.ramos** / senha **123456**.

Para ver/editar os dados: **Exibir → Pesquisador de Objetos do SQL Server → (localdb)\MSSQLLocalDB →
Bancos de Dados → ControleRebanhoDB → Tabelas → botão direito → Exibir Dados**.

> Se mudar alguma classe de `Models`, rode de novo `Add-Migration NomeDaMudanca` e `Update-Database`.

## Estrutura em camadas (MVCR)

| Pasta | Camada | O que tem |
|---|---|---|
| `Models` | Model | Entidades, enums, regras do domínio (`Animal.Categoria`, `Vacina.PodeAplicar`...) |
| `Views` | View | Janelas WPF. Só leem campos e chamam o controller |
| `Controllers` | Controller | Validações e regras de cada caso de uso; DTOs para as telas |
| `Repositories` | Repository | Acesso a dados com EF/LINQ (e o SQL puro do saldo) |
| `Data` | — | `RebanhoDbContext` (mapeamento Fluent API) e `SeedData` |
| `Utils` | — | Sessão, hash de senha, formatação, relógio |

## Relacionamentos exigidos

| Exigência | Onde |
|---|---|
| Herança | `Pessoa` (abstrata) → `Pecuarista` / `Veterinario`, tabela única `Pessoas` (TPH, coluna `TipoPessoa`) |
| 1x1 | `Pecuarista` ↔ `Fazenda` (FK única `Fazenda.PecuaristaId`) |
| 1xN | `Pecuarista` → `Animais`; `Veterinario` → `Vacinacoes` / `Exames` |
| NxN | `Animal` ↔ `Vacina` pela classe de associação `Vacinacao` (data, dose, veterinário) |
| Composição | `Animal` ◆ `Exame` e `Animal` ◆ `Vacinacao` (delete em cascata) |

Configurados em `Data/RebanhoDbContext.cs` → `ConfigurarHeranca` e `ConfigurarRelacionamentos`.

## Funcionalidade com SQL puro

**Saldo do rebanho** → `Repositories/RelatorioRepository.cs`, `ConsultarSaldoRebanho`.
Usa `Database.SqlQuery<T>` com `GROUP BY` sexo/categoria; a categoria é calculada no próprio SQL.
Os valores interpolados viram parâmetros (sem SQL injection). O restante do sistema usa EF/LINQ.

## Dados do veterinário (combinado com o professor)

Só a parte do pecuarista tem telas. Veterinária, catálogo de vacinas, vacinações e exames entram pelo
seed (`Data/SeedData.cs`) para a **Ficha sanitária** ter o que mostrar.

## Onde aplicamos SOLID

| Princípio | Onde |
|---|---|
| **S** — Responsabilidade única | Cada camada tem um papel; um repository por agregado; `Formatador` só formata; `SenhaHasher` só faz hash |
| **O** — Aberto/fechado | Vacina nova é só um registro novo no catálogo (`Vacina`), sem mudar código |
| **L** — Substituição de Liskov | `Pecuarista` e `Veterinario` podem ser usados como `Pessoa` (`LoginController` trabalha com `Pessoa` e `Perfil()`) |
| **I** — Segregação de interfaces | `IRegistroSanitario` só tem vacinar e registrar exame; interfaces de repository pequenas |
| **D** — Inversão de dependência | Controllers recebem `IAnimalRepository`, `IRelogio` etc. no construtor; só `FabricaControllers` conhece as classes concretas |

## Onde aplicamos Clean Code

| Prática | Exemplo |
|---|---|
| Nomes significativos | `ListarMovimentadosNoPeriodo`, `ValidarDataDeCompra`, `nasceuNaFazenda` |
| Funções pequenas / um nível de abstração | `OnModelCreating` só chama `ConfigurarHeranca`, `ConfigurarRelacionamentos`... |
| Sem números mágicos | `FaixaEtaria.LimiteBezerroMeses` (12), usado no C# e como parâmetro do SQL |
| Guard clauses + exceção clara | `RegraNegocioException` em `AnimalController.ValidarFormulario` e `Animal.RegistrarSaida` |
| Tell, don't ask | `animal.RegistrarSaida(data, motivo)`, `vacina.PodeAplicar(animal, data)` |
| DRY | `Tela.Executar` (tratamento de erro das telas), `Formatador` (textos dos enums) |
| Código testável | `IRelogio` permite testar com uma data fixa |
| KISS / YAGNI | Sem camadas extras nem telas do veterinário fora do escopo |
