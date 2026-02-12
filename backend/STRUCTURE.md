# Backend Structure - Clean Architecture

## Domain Layer (Core Business Logic)
- `Entities/` - Domain entities (User, DoctorAppointment, ShoppingItem)
- `Enums/` - UserRole enumerations
- `Common/` - Base entity classes, value objects
- `Exceptions/` - Domain-specific exceptions

## Application Layer (Use Cases & Business Rules)
- `Commands/` - CQRS Commands (Create, Update, Delete)
- `Queries/` - CQRS Queries (Get, List)
- `Validators/` - FluentValidation validators
- `DTOs/` - Data Transfer Objects
- `Interfaces/` - Repository and service interfaces
- `Behaviors/` - MediatR pipeline behaviors

## Infrastructure Layer (External Concerns)
- `Persistence/` - EF Core DbContext, configurations
- `Repositories/` - Repository implementations
- `Services/` - External services (Email, Weather API)
- `Migrations/` - EF Core migrations
- `Seeders/` - Database seeding

## API Layer (Presentation)
- `Controllers/` - REST API endpoints
- `Middleware/` - Authentication, error handling
- `Filters/` - Action filters
- `Extensions/` - Service registration extensions
