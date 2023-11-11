namespace ServiceTemplate.DataContracts.Interfaces;

public interface IBaseService<TDto, TUniqueIdentifier, in TDtoToUpdate>
{
    /// <summary>
    /// Get all entities from repository
    /// </summary>
    /// <param name="ct">Cancellation token</param>
    /// <returns>Return all entities in repository</returns>
    Task<IEnumerable<TDto>> GetAllAsync(CancellationToken ct = default);

    /// <summary>
    /// Get dto by id
    /// </summary>
    /// <param name="id">Unique identifier</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns>Return entity by id</returns>
    Task<TDto> GetByIdAsync(TUniqueIdentifier id, CancellationToken ct = default);

    /// <summary>
    /// Update dto
    /// </summary>
    /// <param name="dtoToUpdate">Entity with all fields</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns>Updated entity</returns>
    Task<TDto> UpdateByIdAsync(TUniqueIdentifier id, TDtoToUpdate dtoToUpdate, CancellationToken ct = default);

    /// <summary>
    /// Delete entity by identifier
    /// </summary>
    /// <param name="id">Entity identifier</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns>Identifier of deleted entity</returns>
    Task<TUniqueIdentifier> DeleteByIdAsync(TUniqueIdentifier id, CancellationToken ct = default);
}