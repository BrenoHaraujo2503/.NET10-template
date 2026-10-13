# Clean Architecture Boilerplate

Boilerplate de API REST em **.NET 10 LTS**, organizado por domínio/feature e com uma feature `Products` funcional de ponta a ponta.

## Tecnologias

- ASP.NET Core Minimal APIs
- Clean Architecture (`Domain`, `Application`, `Infrastructure`, `Api`)
- PostgreSQL + Entity Framework Core
- Redis com cache-aside e invalidação após escrita
- FluentValidation
- Swagger/OpenAPI
- Serilog com logs estruturados
- Problem Details para erros não tratados
- Docker e Docker Compose
- xUnit, FluentAssertions e NSubstitute

## Dependências entre camadas

```text
Api ───────────────> Application <──────── Infrastructure
                         │                       │
                         └──────> Domain <───────┘
```

- `Domain`: entidades e regras de negócio; não depende das outras camadas.
- `Application`: casos de uso, contratos, DTOs e validações.
- `Infrastructure`: EF Core, PostgreSQL, Redis e implementações dos contratos.
- `Api`: composição, endpoints, Swagger, tratamento de erros e observabilidade.

## Executar com Docker

Pré-requisito: Docker Desktop ou Docker Engine com Compose.

```bash
docker compose up --build
```

Depois acesse:

- Swagger: <http://localhost:8080/swagger>
- Health check: <http://localhost:8080/health>
- API: <http://localhost:8080/api/v1/products>

O Compose sobe a API, PostgreSQL e Redis. As migrations são aplicadas automaticamente apenas quando `Database__ApplyMigrations=true`, já configurado no ambiente local do Compose.

## Executar sem Docker

Suba PostgreSQL e Redis, ajuste as connection strings em `src/Boilerplate.Api/appsettings.json` e execute:

```bash
dotnet restore
dotnet run --project src/Boilerplate.Api
```

No ambiente `Development`, a migration inicial é aplicada automaticamente. Em produção, mantenha `Database:ApplyMigrations=false` e aplique migrations no processo de implantação.

## Testes

```bash
dotnet test
```

Para coletar cobertura:

```bash
dotnet test --collect:"XPlat Code Coverage"
```

## Exemplo de requisição

```http
POST /api/v1/products
Content-Type: application/json

{
  "name": "Mechanical Keyboard",
  "description": "Hot-swappable switches",
  "price": 399.90
}
```

## Criar uma nova feature

1. Modele a entidade em `Domain/<Feature>`.
2. Crie contratos e casos de uso em `Application/<Feature>/<UseCase>`.
3. Implemente persistência/cache em `Infrastructure`.
4. Exponha endpoints em `Api/Endpoints/<Feature>`.
5. Cubra regras e casos de uso em `tests/Boilerplate.UnitTests/<Feature>`.

Evite colocar regra de negócio nos endpoints ou na infraestrutura. Os endpoints devem apenas traduzir HTTP para casos de uso e respostas HTTP.
