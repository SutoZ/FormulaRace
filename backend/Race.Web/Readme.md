# FormulaRace Solution

## Overview

FormulaRace is a modern, containerized ASP.NET Core Web API solution for managing racing teams and pilots. The project is designed with clean architecture principles, leverages Entity Framework Core, supports SQL Server (and can be easily switched to PostgreSQL), and is ready for deployment in Docker and Kubernetes environments.

---

## Technologies Used

- **.NET 9** (C# 13)
- **ASP.NET Core Web API**
- **Entity Framework Core** (with SQL Server, PostgreSQL-ready)
- **MediatR** (CQRS and Mediator patterns)
- **Serilog** (structured logging)
- **AutoMapper** (object mapping)
- **Swagger/OpenAPI** (API documentation)
- **Docker** (multi-stage builds, containerization)
- **Docker Compose** (multi-container orchestration)
- **Kubernetes-ready** (manifests can be provided)
- **Health Checks** (for app and database)
- **Identity** (ASP.NET Core Identity for authentication)
- **API Versioning**
- **SPA Static Files** (for frontend integration)

---

## Solution Structure


---

## Key Design Patterns

- **Dependency Injection** (built-in)
- **Repository Pattern** (data access abstraction)
- **Unit of Work** (via DbContext)
- **CQRS** (MediatR for commands/queries)
- **Mediator Pattern** (MediatR)
- **Builder Pattern** (for DTOs, e.g., `PilotDetailsDtoBuilder`)
- **Singleton Pattern** (for stateless services, e.g., logger)
- **Specification/Strategy/Decorator** (ready for extension)

---

## Running Locally with Docker Compose

1. **Set environment variables** (e.g., `DB_PASSWORD`) in your shell or `.env` file.
2. **Build and start the services:**

3. **API available at:** [http://localhost:8080/swagger](http://localhost:8080/swagger)
4. **SQL Server available at:** `localhost:1433` (for management tools)

---

## Health Checks

- **App health endpoint:** [http://localhost:8080/healthz](http://localhost:8080/healthz)
- Checks both application liveness and database connectivity.

---

## Database

- **Default:** SQL Server (Dockerized, see `docker-compose.yaml`)
- **Switch to PostgreSQL:**  
- Change EF Core provider to Npgsql in `Program.cs`
- Update connection string and Docker Compose service

---

## Migrations & Seeding

- **EF Core migrations** are used for schema management.
- **Seed data** for Teams and Pilots is provided via `HasData` in `RaceContext`.

---

## Logging

- **Serilog** is configured for console and file logging.
- Log files are written to `Logs/app-*.log`.

---

## API Documentation

- **Swagger UI:** [http://localhost:8080/swagger](http://localhost:8080/swagger)
- **OpenAPI JSON:** [http://localhost:8080/swagger/v1/swagger.json](http://localhost:8080/swagger/v1/swagger.json)

---

## Extending or Customizing

- Add new entities and DTOs in `/Race.Model` and `/Race.Repo/Dtos`.
- Add new business logic in `/Race.Service`.
- Add new API endpoints in `/Race.Web/Controllers`.
- Add new health checks in `Program.cs`.

---

## Production & Kubernetes

- The solution is ready for deployment in Kubernetes.
- Use the provided Dockerfile and create Kubernetes manifests for your environment.
- For HTTPS, configure Kestrel with a valid certificate and expose port 443.

---

## License

MIT License (or your chosen license)

---

## Contributors

- [Zoltán Sütõ]

---