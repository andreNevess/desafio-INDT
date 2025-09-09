# Desafio Técnico INDT — PropostaService (Arquitetura Hexagonal)

Plataforma simples para **gerenciar propostas de seguro** e **contratos**.  
Este repositório contém dois microserviços:

- **ProposalService** → responsável pela gestão de propostas.
- **ContractService** → responsável pela contratação, que consome o ProposalService.

Arquitetura baseada em **Ports & Adapters (Hexagonal)**, **DDD** e **testes automatizados**.

---

## 🚀 Tecnologias
- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server ou PostgreSQL (opcionalmente InMemory)
- xUnit + FluentAssertions
- Arquitetura Hexagonal (Ports & Adapters)

---

## 🧭 Arquitetura

  -  Client[[Swagger/HTTP]] --> API[ProposalService.Api] && API[ContractService.Api]
  -  API --> APP[Application (Use Cases)]
  -  APP --> DOM[Domain (Entities/Rules)]
  -  APP --> INF[Infrastructure (Adapters)]
  -  INF --> DB[(Relational DB / EF Core ou InMemory)]

  -  ### 💾 Persistência e Migrations

Por padrão, este projeto sobe usando **InMemory** (apenas em memória).  
Você pode usar um banco de dados relacional como **SQL Server** ou **PostgreSQL**.  
Para isso, basta alterar o `appsettings.json`: e executar a migration


"Persistence": {
  "Provider": "SqlServer" // opções: InMemory | SqlServer | PostgreSql
},
"ConnectionStrings": {
  "ProposalDb": "Server=localhost;Database=desafio;User Id=sa;Password=SuaSenhaSegura;Encrypt=False;"
}
