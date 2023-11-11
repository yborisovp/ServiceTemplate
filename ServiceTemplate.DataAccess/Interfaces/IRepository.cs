namespace ServiceTemplate.DataAccess.Interfaces;

public interface IRepository<T, TUniqueIdentifier>
{
    /// <summary>
    /// Get all rows from table
    /// </summary>
    /// <param name="ct">Cancellation token</param>
    /// <returns>Return all rows in table</returns>
    Task<IEnumerable<T>> GetAllAsync(CancellationToken ct = default);

    /// <summary>
    /// Get row by id
    /// </summary>
    /// <param name="id">Unique identifier</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns></returns>
    Task<T> GetByIdAsync(TUniqueIdentifier id, CancellationToken ct = default);

    /// <summary>
    /// Update entity
    /// </summary>
    /// <param name="entityToUpdate">Entity with all fields</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns>Updated entity</returns>
    Task<T> UpdateAsync(T entityToUpdate, CancellationToken ct = default);

    /// <summary>
    /// Delete entity by identifier
    /// </summary>
    /// <param name="id">Entity identifier</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns>Identifier of deleted entity</returns>
    Task<TUniqueIdentifier> DeleteByIdAsync(TUniqueIdentifier id, CancellationToken ct = default);
}