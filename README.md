# TVT

## Technology Stack

- **Framework**: .NET 8
- **Application**: ASP.NET Core MVC
- **Database**: PostgreSQL
- **ORM**: Entity Framework Core
- **Architecture**: N-Tier

## Project Structure

```
TVT/
├── src/
│   ├── TVT.Core/          # Entities, Interfaces
│   ├── TVT.Data/          # DbContext, Repositories
│   ├── TVT.Business/      # Services
│   └── TVT.Web/           # ASP.NET Core MVC, Controllers, Views
├── tests/
│   └── TVT.Tests/         # xUnit Test Project
└── docs/                  # Documentation
```

## Architecture

The solution follows an **N-Tier** architecture with strict separation of concerns:

| Layer          | Responsibility                                      |
| -------------- | --------------------------------------------------- |
| **TVT.Core**   | Domain entities, base classes, interfaces           |
| **TVT.Data**   | Data access, Entity Framework DbContext, repositories |
| **TVT.Business** | Business logic, service layer                     |
| **TVT.Web**    | Presentation layer, ASP.NET Core MVC controllers & views |

Patterns applied:
- Repository Pattern
- Unit Of Work
- Dependency Injection

## How to Build

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- PostgreSQL

### Build

```bash
dotnet build
```

### Run

```bash
dotnet run --project src/TVT.Web
```

### Test

```bash
dotnet test
```
