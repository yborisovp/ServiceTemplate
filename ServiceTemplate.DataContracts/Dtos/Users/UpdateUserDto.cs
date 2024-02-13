using System.ComponentModel.DataAnnotations;

namespace ServiceTemplate.DataContracts.Dtos.Users;

public class UpdateUserDto
{
    [MaxLength(2083)]
    public string? AvatarPhotoUrl { get; set; } 

    [MaxLength(100)]
    public required string FirstName { get; set; } 

    [MaxLength(100)]
    public required string SecondName { get; set; }
    
    public required string UserName { get; set; } 
}