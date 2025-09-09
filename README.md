# Desafio Técnico INDT — PropostaService (Arquitetura Hexagonal)

Plataforma simples para **gerenciar propostas de seguro**. Este repo contém o **PropostaService** implementado com **Ports & Adapters (Hexagonal)**, **DDD** e **testes automatizados**.  

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
```mermaid
flowchart LR
  Client[[Swagger/HTTP]] --> API
  API[ProposalService.Api] --> APP[Application (Use Cases)]
  APP --> DOM[Domain (Entities/Rules)]
  APP --> INF[Infrastructure (Adapters)]
  INF --> DB[(Relational DB / EF Core ou InMemory)]
