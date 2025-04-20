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
    /// Obtém uma lista de resultadps paginados e ordenados com base nos filtros fornecidos.
    /// </summary>
    /// <typeparam name="T">O tipo da entidade que será retornada na consulta.</typeparam>
    /// <param name="filter">Um objeto contendo as informações de filtro, como paginação, ordenação e critérios de busca.</param>
    /// <param name="properties">Um array de strings contendo os nomes das propriedades pelas quais os resultados devem ser ordenados. A ordem das propriedades no array será a ordem de ordenação.</param>
    /// <returns>Uma tarefa assíncrona que retorna uma lista de objetos do tipo T, após aplicar os filtros, a paginação e a ordenação.</returns>
    Task<PageResult<T>> GetPagedAndSortedResultsAsync(PagedFilter filter, params string[] properties);
}
