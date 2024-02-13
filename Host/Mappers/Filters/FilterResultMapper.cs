using ServiceTemplate.DataAccess.Filtering;
using ServiceTemplate.DataAccess.Models.Users;
using ServiceTemplate.DataContracts.Dtos.Users;
using ServiceTemplate.DataContracts.Filters;

namespace ServiceTemplate.Mappers.Filters;

/// <summary>
/// Filtering result mapppers
/// </summary>
public static class FilterResultMapper
{
    /// <summary>
    /// Map filtered users
    /// </summary>
    /// <param name="filterDto"></param>
    /// <returns></returns>
    public static FilterResultDto<UserDto> ToDto(this FilterResult<User> filterDto)
    {
        return new FilterResultDto<UserDto>
        {
            Results = filterDto.Results.Select(UserMapper.ToDto),
            Count = filterDto.Count
        };
    }
}