using Microsoft.EntityFrameworkCore;
using Recepty.Models;

namespace Recepty.Data;

public class AppDbContext : DbContext
{
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<PrescriptionMedicament> Prescription_Medicaments { get; set; }
    
    protected AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Doctor>().HasData(
            new Doctor
            {
                IdDoctor = 1,
                FirstName = "Michał",
                LastName = "Tomaszewski",
                Email = "tomaszew@gmail.com",
            },
            new Doctor
            {
                IdDoctor = 2,
                FirstName = "Kacper",
                LastName = "Otowski",
                Email = "otowski@gmail.com",
            }
        );
        modelBuilder.Entity<Patient>().HasData(
            new Patient
            {
                IdPatient = 1,
                FirstName = "Robert",
                LastName = "Juchimiuk",
                BirthDate = new DateTime(1410, 1, 1),
            },
            new Patient
            {
                IdPatient = 2,
                FirstName = "Piotr",
                LastName = "Gruba",
                BirthDate = new DateTime(1939, 8, 1),
            }
        );
        modelBuilder.Entity<Medicament>().HasData(
            new Medicament
            {
                IdMedicament = 1,
                Name = "Apap Dzień",
                Description = "Nie brać w nocy",
                Type = "Medicament",
            },
            new Medicament
            {
                IdMedicament = 2,
                Name = "Apap Noc",
                Description = "Nie brać w dzień",
                Type = "Medicament",
            }
        );
        modelBuilder.Entity<Prescription>().HasData(
            new Prescription()
            {
                IdPrescription = 1,
                Date = new DateTime(2025, 6, 01),
                DueDate = new DateTime(2025, 11, 01),
                IdPatient = 1,
                IdDoctor = 1,
            }
        );
        modelBuilder.Entity<PrescriptionMedicament>().HasData(
            new PrescriptionMedicament()
            {
                IdPrescription = 1,
                IdMedicament = 1,
                Dose = 10,
                Details = "Once a day"
            }
        );
    }
}   