using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace ServiceTemplate.DataAccess.Models.Users;

public class User: IdentityUser<long>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public override long Id
    {
        get => base.Id;
        set => base.Id = value;
    }
    [MaxLength(100)]
    public required string FirstName { get; set; } 
    [MaxLength(100)]
    public required string SecondName { get; set; }
    
    [MaxLength(2083)]
    public string? AvatarPhotoUrl { get; set; }
}