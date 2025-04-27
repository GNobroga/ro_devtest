using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using RO.DevTest.Domain.Models;

namespace RO.DevTest.Application.Contracts.Persistance.Repositories;

public interface IBaseRepository<T> where T : class {

    /// <summary>
    /// Creates a new entity in the database
    /// </summary>
    /// <param name="entity"> The entity to be create </param>
    /// <param name="cancellationToken"> Cancellation token </param>
    /// <returns> The created entity </returns>
    Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Finds the first entity that matches with the <paramref name="predicate"/>
    /// </summary>
    /// <param name="predicate">
    /// The <see cref="Expression"/> to be used while
    /// looking for the entity
    /// </param>
    /// <returns>
    /// The <typeparamref name="T"/> entity, if found. Null otherwise. </returns>
    T? Get(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IQueryable<T>>? queryInterceptor = default);

    /// <summary>
    /// Updates an entity entry on the database
    /// </summary>
    /// <param name="entity"> The entity to be added </param>
    Task UpdateAsync(T entity);

    /// <summary>
    /// Deletes one entry from the database
    /// </summary>
    /// <param name="entity"> The entity to be deleted </param>
    Task DeleteAsync(T entity);

    /// <summary>
    /// Retrieves a paginated and sorted list of entities based on the specified filter criteria.
    /// </summary>
    /// <typeparam name="T">The type of the entity being queried.</typeparam>
    /// <param name="filter">The filter containing pagination, sorting, and search criteria.</param>
    /// <param name="properties">The properties to sort the results by.</param>
    /// <param name="includes">Optional related entities to include in the query.</param>
    /// <returns>A paginated and sorted list of entities.</returns>
    Task<PageResult<T>> GetPagedAndSortedResultsAsync(
        PagedFilter filter, 
        IEnumerable<string>? properties= default, 
        Func<IQueryable<T>, IQueryable<T>>? queryInterceptor = default);


    /// <summary>
    /// Asynchronously checks whether there is at least one entity that matches the specified condition.
    /// </summary>
    /// <param name="predicate">A lambda expression that defines the condition to evaluate.</param>
    /// <returns>
    /// A <see cref="Task{Boolean}"/> representing the asynchronous operation. 
    /// The result is <c>true</c> if any entity satisfies the predicate; otherwise, <c>false</c>.
    /// </returns>
    Task<bool> ExistsBy(Expression<Func<T, bool>> predicate);

    /// <summary>
    /// Retrieves an instance of <see cref="DbContext"/> for database access.
    /// </summary>
    /// <returns>
    /// An instance of <see cref="DbContext"/> that can be used to execute queries,  
    /// perform read and write operations, or manage transactions with the database.
    /// </returns>
    /// <remarks>
    /// This method is useful for obtaining a database context in scenarios  
    /// where dependency injection is not available or when a new instance is needed.
    /// </remarks>
    DbContext GetContext();

}
