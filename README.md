# Guardian

The **Guardian API** is a secure and scalable solution built with **ASP.NET Core** for the backend and **Blazor** in the front end. It provides user authentication using **JWT tokens**, role-based access control, and CRUD operations for managing resources. Designed with **Clean Architecture**, it ensures separation of concerns, testability, and maintainability. Ideal for modern web and mobile applications.
This repository will serve as a guide to build future projects as well as a knowledge reference.

---

## Features

- **Backend**: RESTful API for user registration, login, and authentication.  
- **Frontend**: Responsive forms and user-friendly navigation.  
- **Security**: Encrypted passwords, JWT for session management, and role-based access control.  
- **Tech Stack**: ASP.NET Core, Entity Framework Core, SQL Server, JWT (JSON Web Tokens), ASP.NET Core Identity. 
- **Tools**: Visual Studio, Git, Swager/OpenAPI 

---

## Clean Architecture

The Guardian API follows **Clean Architecture**, a design pattern that emphasizes separation of concerns and maintainability. It organizes the project into layers:
- **Domain**: Contains entities.
- **Application**: Contains the contracts for the repositories and services.
- **Infrastructure**: Handles data access, external services and service implementation.
- **API**: Exposes endpoints for client interaction.

This structure ensures that the core business logic remains independent of frameworks, databases, and UI, making the application more testable, scalable, and adaptable to change.

---

## Required Packages

### 1. `Microsoft.AspNetCore.Identity.EntityFrameworkCore`
- Provides integration between ASP.NET Core Identity and Entity Framework Core. Enables user and role management with a database-backed store.

### 2. `Microsoft.AspNetCore.Authentication.JwtBearer`
- Adds JWT Bearer authentication to ASP.NET Core. Validates JWT tokens for securing API endpoints.

### 3. `Microsoft.IdentityModel.Tokens`
- A library for creating and validating security tokens, including JWTs. Essential for token signing and validation.

### 4. `Microsoft.EntityFrameworkCore.SqlServer`
- Enables Entity Framework Core to work with SQL Server. Provides database connectivity and operations.

### 5. `Microsoft.EntityFrameworkCore.Design`
- Supports design-time tools for Entity Framework Core, such as migrations and scaffolding.

### 6. `Microsoft.EntityFrameworkCore.Tools`
- Provides CLI tools for managing Entity Framework Core migrations and database updates.