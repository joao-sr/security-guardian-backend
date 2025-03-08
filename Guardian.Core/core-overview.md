## Core Layer

The **Core** folder is the heart of the Guardian API, containing the **Domain** and **Application** layers:

### Domain
- Houses all entities.

### Application
- Contains contracts (interfaces) that define the application's behavior and interactions.
- Ensures loose coupling and testability.

This structure ensures a clean separation of concerns, making the application more maintainable and scalable.
The actual implementation of the CLEAN architecture will follow a generic repository pattern.

# Generic Repository Pattern

The **Generic Repository Pattern** is a design pattern that abstracts data access logic, making it easier to manage and maintain database operations in your application. It provides a common interface for performing CRUD (Create, Read, Update, Delete) operations on entities.

## Why Use the Generic Repository Pattern?
- **Code Reusability**: Reduces boilerplate code by providing a common implementation for all entities.
- **Separation of Concerns**: Keeps data access logic separate from business logic.
- **Testability**: Makes it easier to mock data access logic in unit tests.

## Implementation

### 1. **Generic Repository Interface**
The `IGenericRepository<T>` interface defines the contract for CRUD operations.
All database operations performed in the platform will do the basics:

```csharp
public interface IGenericRepository<T> where T : class
{
    Task<T> GetByIdAsync(int id);
    Task<IReadOnlyList<T>> GetAllAsync();
    Task<int> AddAsync(T entity);
    Task<int> UpdateAsync(T entity);
    Task<int> DeleteAsync(int id);
}
```
Any other class that extends the Generic Repository can therefore use the methods

The actual implementation of the Generic Repository interface will happen in the `Infrastructure Layer`. Please refer to it.