﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Domain.Models;
using RO.DevTest.Persistence.Extensions;
using System.Linq.Expressions;

namespace RO.DevTest.Persistence.Repositories;

public class BaseRepository<T>(DefaultContext defaultContext) : IBaseRepository<T> where T: class {
    private readonly DefaultContext _defaultContext = defaultContext;

    protected DefaultContext Context { get => _defaultContext; }

    protected DbSet<T> Entities => Context.Set<T>();

    public async Task<PageResult<T>> GetPagedAndSortedResultsAsync(
        PagedFilter filter, 
        IEnumerable<string>? properties = default, 
        Func<IQueryable<T>, IQueryable<T>>? queryInterceptor = default
    ) {
        properties ??= [];


        var baseQuery = Entities.WhereContainsIgnoreCaseMultiple(filter.Keyword, properties)
            .AsNoTracking();

        baseQuery = queryInterceptor?.Invoke(baseQuery) ?? baseQuery;

        var page = filter.Page;
        var pageSize = filter.PageSize;
        var totalItems = await baseQuery.CountAsync();

        var results = await baseQuery
            .OrderByProperty(filter.SortBy, filter.IsSortOrderAscending)
            .Paginate(page, pageSize)
            .ToListAsync();

        return new PageResult<T>(page, pageSize, totalItems, results);
    }

    public async Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default) {
        await Entities.AddAsync(entity, cancellationToken);
        await Context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(T entity) {
        Entities.Update(entity);
        await Context.SaveChangesAsync();
    }

    public Task<bool> ExistsBy(Expression<Func<T, bool>> predicate) => Task.FromResult(Get(predicate) is not null);

    public async Task DeleteAsync(T entity) {
        Entities.Remove(entity);
        await Context.SaveChangesAsync();
    }

    public T? Get(Expression<Func<T, bool>> predicate,  Func<IQueryable<T>, IQueryable<T>>? queryInterceptor = default)
    => GetQueryWithIncludes(predicate, queryInterceptor).FirstOrDefault();

    /// <summary>
    /// Generates a filtered <see cref="IQueryable{T}"/>, based on its
    /// <paramref name="predicate"/> and <paramref name="includes"/>, including
    /// the data requested
    /// </summary>
    /// <param name="predicate">
    /// The <see cref="Expression"/> to use as filter
    /// </param>
    /// <param name="includes">
    /// The <see cref="Expression"/> to use as include
    /// </param>
    /// <returns>
    /// The generated <see cref="IQueryable{T}"/>
    /// </returns>
    private IQueryable<T> GetQueryWithIncludes(
        Expression<Func<T, bool>> predicate,
        Func<IQueryable<T>, IQueryable<T>>? queryInterceptor = default
    ) {
        IQueryable<T> baseQuery = GetWhereQuery(predicate);

        baseQuery = queryInterceptor?.Invoke(baseQuery) ?? baseQuery;
        
        return baseQuery;
    }

    /// <summary>
    /// Generates an <see cref="IQueryable"/> based on
    /// the <paramref name="predicate"/>
    /// </summary>
    /// <param name="predicate">
    /// An <see cref="Expression"/> representing a filter
    /// of it
    /// </param>
    /// <returns>S
    /// The <see cref="IQueryable{T}"/>
    /// </returns>
    private IQueryable<T> GetWhereQuery(Expression<Func<T, bool>> predicate) {
        IQueryable<T> baseQuery = Entities;

        if(predicate is not null) {
            baseQuery = baseQuery.Where(predicate);
        }

        return baseQuery;
    }

    public DbContext GetContext() {
        return Context;
    }
}
