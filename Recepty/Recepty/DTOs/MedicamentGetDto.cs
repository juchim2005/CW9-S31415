﻿namespace Recepty.DTOs;

public class MedicamentGetDto
{
    public int IdMedicament { get; set; }
    public int? Dose { get; set; }
    public string Description { get; set; } = null!;
}