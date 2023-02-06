using System.Reflection;

namespace Vivarni.Example.Infrastructure;

public static class PersistenceServicesRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
             options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

        var executingAssembly = Assembly.GetExecutingAssembly(); services.AddDbContext<DbContext, ApplicationDbContext>();
        services.AddVivarniInfrastructure();
        services.Scan(scan => scan
        .FromAssemblies(executingAssembly)
        .AddClasses(classes => classes.AssignableTo(typeof(IGenericRepository<>)))
        .AsImplementedInterfaces()
        .WithScopedLifetime());
        services.Scan(scan => scan
        .FromAssemblies(Assembly.GetExecutingAssembly())
        .AddClasses(classes => classes.AssignableTo(typeof(IDomainEventHandler<>)))
        .AsImplementedInterfaces()
        .WithScopedLifetime());
        services.Scan(scan => scan
        .FromAssemblies(executingAssembly)
        .AddClasses(b => b.Where(type => type.Name.EndsWith("Repository")))
        .AsImplementedInterfaces()
        .WithScopedLifetime());


        return services;
    }
}