using ServiceTemplate.DataAccess.Models.Users;
using ServiceTemplate.DataContracts.Dtos.Users;

namespace ServiceTemplate.Mappers;

/// <summary>
/// User mapper
/// </summary>
public static class UserMapper
{
    /// <summary>
    /// Mao entity to dto
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public static UserDto ToDto(this User user)
    {
        return new UserDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            SecondName = user.SecondName,
            AvatarPhotoUrl = user.AvatarPhotoUrl,
            UserName = user.UserName!
        };
    }
    
    /// <summary>
    /// Map dto to entity
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public static User ToEntity(this UserDto user)
    {
        return new User
        {
            Id = user.Id,
            FirstName = user.FirstName,
            SecondName = user.SecondName,
            UserName = user.UserName,
            AvatarPhotoUrl = user.AvatarPhotoUrl,
            Email = user.Email
        };
    }
    
    /// <summary>
    /// Map new user for creation proposes
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public static User ToEntity(this CreateUserDto user)
    {
        return new User
        {
            FirstName = user.FirstName,
            SecondName = user.SecondName,
            UserName = user.UserName,
            AvatarPhotoUrl = user.AvatarPhotoUrl,
            Email = user.Email
        };
    }
    
    /// <summary>
    /// Map user for updating propose
    /// </summary>
    /// <param name="user"></param>
    /// <param name="actualUser"></param>
    /// <returns></returns>
    public static User ToEntity(this UpdateUserDto user, User actualUser)
    {
        actualUser.AvatarPhotoUrl = user.AvatarPhotoUrl;
        actualUser.FirstName = user.FirstName;
        actualUser.SecondName = user.SecondName;
        actualUser.UserName = user.UserName;
        return actualUser;
    }
}