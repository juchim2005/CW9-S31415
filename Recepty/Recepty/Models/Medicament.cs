using System.ComponentModel.DataAnnotations;

namespace Recepty.Models;

public class Medicament
{
    [Key]
    public int IdMedicament { get; set; }
    
    [MaxLength(100)]
    public string Name { get; set; }
    
    [MaxLength(100)]
    public string Description { get; set; }
    
    [MaxLength(100)]
    public string Type { get; set; }
    
    public virtual ICollection<PrescriptionMedicament> PRE { get; set; } = null!;
}