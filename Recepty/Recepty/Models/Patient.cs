using System.ComponentModel.DataAnnotations;

namespace Recepty.Models;

public class Patient
{
    [Key]
    public int IdPatient { get; set; }
    
    [MaxLength(100)]
    public string FirstName { get; set; }
    
    [MaxLength(100)]
    public string LastName { get; set; }
    
    public DateTime BirthDate { get; set; }
    public virtual ICollection<Prescription> Prescriptions { get; set; } = null!;
}