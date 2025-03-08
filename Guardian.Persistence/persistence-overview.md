# Persistence Layer

The **Persistence Layer** is responsible for data access and database operations in the Guardian API. It acts as the bridge between the application and the database, ensuring that data is stored, retrieved, and managed efficiently. This layer is organized into the following folders:

---

## 1. **Configurations**
This folder contains configuration files for Entity Framework Core, including:
- **Entity Configurations**: Define relationships, constraints, and table mappings for entities.
- **Seeding Data**: Initial data population for the database (e.g., default roles, admin users).

### Example: User Configuration
```csharp
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Email).IsRequired();
        builder.HasIndex(u => u.Email).IsUnique();
    }
}
```

## 2. **Database Context**

The `DatabaseContext` file is the heart of the Persistence Layer. It:
- Manages database connections and transactions
- Defined DbSet<T> properties for each entity.
- Applies configurations and migrations.

### Example: User Configuration

```csharp
public class GuardianDatabaseContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }

    public GuardianDatabaseContext(DbContextOptions<GuardianDatabaseContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(GuardianDatabaseContext).Assembly);
    }
}
```

The line `public GuardianDatabaseContext(DbContextOptions<GuardianDatabaseContext> options) : base(options){}`
- DbContextOptions<TContext> is a generic class provided by Entity Framework Core (EF Core).
- It is used to configure the behavior of the DbContext (in this case, GuardianDatabaseContext).
- The `DbContextOptions<GuardianDatabaseContext>` is typically configured in the `Program.cs` or `Startup.cs` file when setting up dependency injection.

### Example Configuration in `Program.cs`
```csharp
var builder = WebApplication.CreateBuilder(args);

// Configure the DbContext with SQL Server
builder.Services.AddDbContext<GuardianDatabaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();
```

- The `AddDbContext` method registers the GuardianDatabaseContext with the dependency injection (DI) container.
- The `options` parameter is configured to use SQL Server as the database provider and to retrieve the connection string from the application's configuration (`appsettings.json`).

The line `modelBuilder.ApplyConfigurationsFromAssembly(typeof(GuardianDatabaseContext).Assembly);` tells the compiler to look in the assembly tree (all files in the project) to look for any configuration file
we know that it is a configuration file if it inherits from `IEntityTypeConfiguration<T>`

## 3. **Migrations**
This folder contains Entity Framework Core migrations, which:
- Track changes to the database schema over time.
- Generate SQL scripts to update the database.

In Visual Studio you can use the `Package Manage Console` to write commands.
Just search in the general search bar on VS for it and it should show up.
To add a migration simply write
`add-migration <SomeDescriptionForTheMigration>`
The description usually comes with no spaces and in camel-case (and withou the angle brackets).
When the command is executed a new file should be generated in your migrations folder.
If you are happy with it run the command 
`update-database`
All the tables registered in the `DbContext` file should now be reflected in the database 

## 4. **Repositories**
This folder contains the implementation of repository interfaces defined in the Application Layer. Repositories:
- Encapsulate data access logic.
- Provide a clean API for performing CRUD operations.

### Example User Repository
```csharp
public class UserRepository : IUserRepository
{
    private readonly GuardianDatabaseContext _context;

    public UserRepository(GuardianDatabaseContext context)
    {
        _context = context;
    }

    public async Task<User> GetByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }
}
```

## 5. **Services**
This folder contains the implementation of service interfaces defined in the Application Layer. Services:
- Handle business logic related to data access.
- Coordinate between repositories and other application components.

### Example User Service
```csharp
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> GetUserByIdAsync(int id)
    {
        return await _userRepository.GetByIdAsync(id);
    }
}
```

### **Generic Repository Implementation**

The implementation of any contract happens in the Persistance layer since this is the point at which we communicate with the database
an example implementation of the generic repository is as follows

```csharp
public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly DbContext _context;

    public GenericRepository(DbContext context)
    {
        _context = context;
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<int> AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> UpdateAsync(T entity)
    {
        _context.Set<T>().Update(entity);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        _context.Set<T>().Remove(entity);
        return await _context.SaveChangesAsync();
    }
}```