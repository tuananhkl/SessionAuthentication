using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthenAuthor.Models;

public class User
{
    [Key]
    public int Id { get; set; }

    [Column(TypeName = "nvarchar(150)")]
    public string UserName { get; set; } = null!;

    [Column(TypeName = "nvarchar(250)")] 
    public string Password { get; set; } = null!;

    [Column(TypeName = "DateTime")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime? DateOfBirth { get; set; }
    
    [Column(TypeName = "nvarchar(250)")]
    public string? Address { get; set; }

    [Column(TypeName = "nvarchar(250)")]
    public string? Email { get; set; }

    public int? Age { get; set; }
    public bool Gender { get; set; }

    // Id -> int
    //     UserName -> Nvarchar(150) Not null,
    // Password -> Nvarchar(250) not null,
    // BirthDay -> DateTime (null)
    // Address -> Nvarchar(250) (null)
    //     Email -> Nvarchar(250) (null)
    //     Age -> Int (null)
    // Gender -> bit 
}