# Todo List API

A modern, scalable Todo List API built with .NET using Clean Architecture principles and CQRS pattern.

## 🏗️ Architecture

The solution follows Clean Architecture and is organized into the following projects:

- **TodoList.Domain**: Contains enterprise business rules and entities
- **TodoList.Application**: Contains application business rules, interfaces, and CQRS implementations
- **TodoList.Infrastructure**: Contains external concerns and implementations
- **TodoList.Api**: API layer with controllers and configuration

## 🚀 Features

- Clean Architecture implementation
- CQRS pattern using MediatR
- API versioning (v1 and v2)
- Swagger documentation
- Docker support
- Entity Framework Core for data access

## 🛠️ API Endpoints

### V1 Endpoints

```
GET     api/v1/tasks          - Get all tasks
GET     api/v1/tasks/{id}     - Get task by ID
POST    api/v1/tasks          - Create a new task
PUT     api/v1/tasks/{id}     - Update an existing task
DELETE  api/v1/tasks/{id}     - Delete a task
```

### V2 Endpoints (Enhanced Response Format)

```
GET     api/v2/tasks          - Get all tasks (with metadata)
GET     api/v2/tasks/{id}     - Get task by ID (with metadata)
POST    api/v2/tasks          - Create a new task (with HATEOAS links)
```

## 📋 Task Model

```json
{
  "id": "guid",
  "title": "string",
  "description": "string",
  "isCompleted": "boolean"
}
```

## 🚦 Getting Started

### Prerequisites

- .NET 7.0 or later
- Docker (optional)

### Running Locally

1. Clone the repository
```bash
git clone [repository-url]
```

2. Navigate to the solution directory
```bash
cd TodoList
```

3. Run the application
```bash
dotnet run --project Api/Api.csproj
```

### Using Docker

```bash
docker-compose up
```

## 📚 API Documentation

The API documentation is available through Swagger UI when running the application:

- V1 Documentation: `/swagger/v1/swagger.json`
- V2 Documentation: `/swagger/v2/swagger.json`

Access Swagger UI at: `https://localhost:[port]/swagger`

## 🔄 Version Control

The API supports versioning through URL path versioning:
- V1: Basic CRUD operations
- V2: Enhanced response format with additional metadata and HATEOAS links

## 🛡️ Error Handling

The API uses standard HTTP status codes and includes detailed error messages:

- 200: Success
- 201: Resource created
- 400: Bad request
- 404: Resource not found
- 500: Internal server error


## 🐳 Docker Support

The application includes Docker support with:
- Multi-stage builds
- Docker Compose configuration

