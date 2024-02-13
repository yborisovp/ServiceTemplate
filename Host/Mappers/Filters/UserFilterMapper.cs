using ServiceTemplate.DataAccess.Filtering;
using ServiceTemplate.DataContracts.Filters;

namespace ServiceTemplate.Mappers.Filters;

/// <summary>
/// User filter mapping
/// </summary>
public static class UserFilterMapper
{
    /// <summary>
    /// Map dto to entity
    /// </summary>
    /// <param name="filterDto"></param>
    /// <returns></returns>
    public static UserFilters ToEntity(this UserFiltersDto filterDto)
    {
        return new UserFilters
        {
            UserName = filterDto.UserName,
            PageNumber = filterDto.PageNumber,
            PageSize = filterDto.PageSize
            
        };
    }
}