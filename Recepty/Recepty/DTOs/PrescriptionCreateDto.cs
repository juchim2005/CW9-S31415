using System.ComponentModel.DataAnnotations;

namespace Recepty.DTOs;

public class PrescriptionCreateDto
{
    [Required]
    public PatientCreateDto Patient { get; set; } = null!;

    [Required]
    public List<MedicamentGetDto> Medicaments { get; set; } = null!;

    [Required]
    public DateTime Date { get; set; }
    
    [Required]
    public DateTime DueDate { get; set; }
    
    [Required]
    public int IdDoctor { get; set; }
    
}