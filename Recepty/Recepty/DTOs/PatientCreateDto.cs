using System.ComponentModel.DataAnnotations;

namespace Recepty.DTOs;

public class PatientCreateDto
{
    [Required]
    public int IdPatient { get; set; }
    
    [MaxLength(100)] 
    [Required] 
    public string FirstName { get; set; } = null!;
    
    [MaxLength(100)]
    [Required]
    public string LastName { get; set; } = null!;
    
    [Required]
    public DateTime Birthdate { get; set; }
}