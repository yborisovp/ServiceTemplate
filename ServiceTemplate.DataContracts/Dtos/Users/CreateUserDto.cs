using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ServiceTemplate.DataContracts.Dtos.Users;

public class CreateUserDto
{
    [MaxLength(2083)]
    public string? AvatarPhotoUrl { get; set; } 

    [MaxLength(100)]
    public required string FirstName { get; set; } 

    [MaxLength(100)]
    public required string SecondName { get; set; }
    [JsonIgnore]
    public string FullName => $"{FirstName} {SecondName}";

    public required string UserName { get; set; } 

    public string? Email { get; set; } 
}