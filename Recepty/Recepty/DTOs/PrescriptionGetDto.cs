namespace Recepty.DTOs;

public class PrescriptionGetDto
{
    public int IdPrescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public List<MedicamentDetailsGetDto> Medicaments { get; set; } = null!;
    public DoctorGetDto Doctor { get; set; } = null!;
}