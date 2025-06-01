using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Recepty.Models;

[PrimaryKey(nameof(IdMedicament),nameof(IdPrescription))]
public class PrescriptionMedicament
{
    public int IdMedicament { get; set; }
    [ForeignKey("IdMedicament")]
    public Medicament Medicament { get; set; }
    
    public int IdPrescription { get; set; }
    [ForeignKey("IdPrescription")]
    public Prescription prescription { get; set; }
    
    public int? Dose { get; set; }
    [MaxLength(100)]
    public string Details { get; set; }
}