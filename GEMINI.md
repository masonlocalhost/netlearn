# NETlearn Project Overview

`NETlearn` is a .NET 8.0 Web API project designed with Clean Architecture principles. It provides a foundation for a user and course management system, leveraging PostgreSQL for data persistence and Temporal for workflow orchestration.

## Main Technologies

- **Runtime:** .NET 8.0
- **Framework:** ASP.NET Core
- **Database:** PostgreSQL (via `Npgsql.EntityFrameworkCore.PostgreSQL`)
- **ORM:** Entity Framework Core
- **Temporal:** Workflow orchestration (NuGet packages `Temporalio` and `Temporalio.Extensions.Hosting` included)
- **Containerization:** Docker (via `docker-compose.yml`)

## Architecture

The project follows a multi-project Clean Architecture structure located in the `src/` directory:

- **NETlearn.Domain:** Core entities (e.g., `User`, `Course`), value objects, and domain-specific exceptions. Zero dependencies on other projects.
- **NETlearn.Application:** Business logic, interfaces (`IUserService`, `IUserRepository`), and Data Transfer Objects (DTOs). Depends on `Domain`.
- **NETlearn.Infrastructure:** Data access implementation using EF Core (`ApplicationDbContext`), repositories, and external service integrations. Depends on `Application`.
- **NETlearn.WebAPI:** Entry point, Controllers, and custom Middlewares. Depends on `Infrastructure` and `Application`.

## Building and Running

### Prerequisites

- .NET 8.0 SDK
- Docker (for PostgreSQL)

### Key Commands

- **Run Infrastructure:** `docker-compose up -d` (starts PostgreSQL)
- **Build Solution:** `dotnet build NETlearn.slnx`
- **Run Web API:** `dotnet run --project src/NETlearn.WebAPI/NETlearn.WebAPI.csproj`
- **Apply Database Migrations:** `dotnet ef database update --project src/NETlearn.Infrastructure/NETlearn.Infrastructure.csproj --startup-project src/NETlearn.WebAPI/NETlearn.WebAPI.csproj`
- **Add New Migration:** `dotnet ef migrations add <MigrationName> --project src/NETlearn.Infrastructure/NETlearn.Infrastructure.csproj --startup-project src/NETlearn.WebAPI/NETlearn.WebAPI.csproj`

## Development Conventions

- **Layer Independence:** Adhere strictly to the dependency hierarchy: `WebAPI` -> `Infrastructure` -> `Application` -> `Domain`.
- **Repository Pattern:** Abstract data access through repositories in the `Infrastructure` layer.
- **Service Pattern:** Centralize business logic in the `Application` layer.
- **DTOs:** Use POCO DTOs in the `Application` layer for data transfer, keeping them free of framework-specific attributes (like MVC attributes).
- **EF Core Migrations:** Manage schema changes via migrations in the `Infrastructure` project, using the `WebAPI` project as the startup project.
- **Nullable Reference Types:** Enabled across all projects for improved safety.
