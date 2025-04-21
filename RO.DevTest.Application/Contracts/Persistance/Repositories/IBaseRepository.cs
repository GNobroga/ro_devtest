using System.Linq.Expressions;
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
    T? Get(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);

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
    /// Retrieves a paginated and sorted list of results based on the provided filters.
    /// </summary>
    /// <typeparam name="T">The type of the entity that will be returned in the query.</typeparam>
    /// <param name="filter">An object containing filtering information such as pagination, sorting, and search criteria.</param>
    /// <param name="properties">An array of strings containing the property names to sort the results by. The order of the array determines the sorting priority.</param>
    /// <returns>
    /// An asynchronous task that returns a <see cref="PageResult{T}"/> containing the filtered, paginated, and sorted list of entities.
    /// </returns>
    Task<PageResult<T>> GetPagedAndSortedResultsAsync(PagedFilter filter, params string[] properties);



    /// <summary>
    /// Asynchronously checks whether there is at least one entity that matches the specified condition.
    /// </summary>
    /// <param name="predicate">A lambda expression that defines the condition to evaluate.</param>
    /// <returns>
    /// A <see cref="Task{Boolean}"/> representing the asynchronous operation. 
    /// The result is <c>true</c> if any entity satisfies the predicate; otherwise, <c>false</c>.
    /// </returns>
    Task<bool> ExistsBy(Expression<Func<T, bool>> predicate);
}
