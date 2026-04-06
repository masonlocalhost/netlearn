# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Commands

```bash
# Run the application (http://localhost:5195)
dotnet run

# Build
dotnet build

# Database migrations
dotnet ef migrations add <MigrationName>
dotnet ef database update

# Start PostgreSQL (required before running)
docker compose up -d
```

No test project exists yet.

## Architecture

This is a Clean Architecture ASP.NET Core 8 Web API with four layers:

```
src/
├── Domain/          # Core entities (User, Course, SafeKey, CourseUser)
├── Application/     # Services, interfaces, DTOs
├── Infrastructure/  # EF Core DbContext, repositories, migrations
└── Presentation/    # Controllers, middleware
```

**Dependency flow:** Presentation → Application → Domain ← Infrastructure

### Key patterns

- **Repository pattern**: `IUserRepository` / `UserRepository` abstracted behind an interface; registered as `Scoped`
- **Service layer**: `IUserService` / `UserService` handles business logic and DTO mapping
- **EF Core configuration**: Fluent API in `Infrastructure/Data/Configurations/` (separate file per entity)
- **Middleware registration**: Custom extension methods in `Presentation/Middlewares/Middlewares.cs`

### Domain model

- `User` ↔ `Course`: many-to-many via `CourseUser` join entity
- `User` → `SafeKey`: one-to-many (API keys per user)

### Middleware pipeline order (Program.cs)

1. `UserInjectionMiddleware` — injects `HttpContext.Items["UserId"]` (hardcoded "1234" for now)
2. `RequestCultureMiddleware` — reads `?culture=` query param and sets `CultureInfo`
3. HTTPS redirect → Authorization → Controllers

### Database

PostgreSQL via Npgsql. Connection string key: `PostgresqlConnection` in `appsettings.json`.

Docker Compose runs `postgres:18` on port `5432`, database `nitro-database`.

### Notable dependencies

- `Npgsql.EntityFrameworkCore.PostgreSQL` — PostgreSQL driver
- `Temporalio` + `Temporalio.Extensions.Hosting` — Temporal workflow engine (not yet wired up)
- OpenAPI commented out in `Program.cs` (can be re-enabled)

### TestController

`/api/v1/test` is a prototype controller demonstrating middleware context access and query-param filtering. It is not backed by a real service/repository yet.
