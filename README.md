# Order Management API

This is a .NET 6.0 application for an order management system that I developed during my internship. It uses Entity Framework for data access and ASP.NET Core for the web API layer.

## Structure

The application is divided into several projects:

1. `OrderManagement.API`: This is the web API project. It contains controllers for handling HTTP requests related to baskets, brands, orders, and products. It uses JWT for authentication.

2. `OrderManagement.Common`: This project contains common classes used across the application, such as DTOs (Data Transfer Objects), contracts (interfaces), and helper classes.

3. `OrderManagement.Data`: This project contains the data access layer of the application. It uses Entity Framework Core and contains the DbContext and entity classes.

4. `OrderManagement.BusinessEngine`: This project contains the business logic of the application.

## Features

- CRUD operations on products, brands, and baskets.
- Creating orders and updating their status.
- JWT authentication.
- Repository pattern for data access.
- DTOs for data transfer between the API and the business layer.
- Result pattern for handling operation results.
- Serilog for logging.

## Example - UserController

The `UsersController` provides the following endpoints:

- `POST /api/users/authenticate`: Authenticate a user.
- `POST /api/users/register`: Register a new user.
- `GET /api/users`: Get the current authenticated user.
- `GET /api/users/{userid}`: Get a user by their ID.
- `GET /api/users/all`: Get all users.

## Running the Application

To run the application, you will need .NET 6.0 SDK installed. You can then use the `dotnet run` command in the `OrderManagement.API` project directory.
