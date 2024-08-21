# Clean Architecture .NET 8 API

## Project Overview

- This API is designed for managing tournaments, providing functionalities to create and manage tournaments, matches, teams, and players. It allows users to organize and track all aspects of a tournament, from team and player registrations to match scheduling and results tracking. The system is built to be flexible, supporting various tournament formats and ensuring a smooth management experience for organizers and participants.


- This project is a .NET 8 API developed using Clean Architecture principles, leveraging Domain-Driven Design (DDD) concepts, Repository Pattern, and Service Layer. It integrates Entity Framework Core with PostgreSQL as the database, and it is fully tested using unit tests.


## Features

- Clean Architecture implementation
- Domain-Driven Design (DDD) patterns
- Repository Pattern for data access
- Service Layer to handle business logic
- Entity Framework Core for ORM
- PostgreSQL as the database
- Unit testing for services and repositories

## Architecture Overview

This API is built using Clean Architecture, which ensures that the core logic and domain rules are isolated from external concerns. The layers are organized as follows:

- **Domain Layer**: Contains the core business logic and domain entities.
- **Application Layer**: Contains the business rules and the implementation of the use cases.
- **Infrastructure Layer**: Contains the implementation of external concerns like data access (using Entity Framework Core) and repositories.
- **Presentation Layer**: Contains the API controllers and any other external interfaces.

## Technologies Used

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- PostgreSQL
- xUnit (for unit testing)
- Moq (for mocking dependencies in tests)
