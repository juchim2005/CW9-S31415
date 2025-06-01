using Microsoft.EntityFrameworkCore;
using Recepty.Data;
using Recepty.DTOs;
using Recepty.Exceptions;
using Recepty.Models;

namespace Recepty.Service;
public interface IDbService
{
    public Task<PrescriptionGetDto> CreatePrescriptionAsync(PrescriptionCreateDto prescriptionData);
    public Task<PatientGetDto> GetPatientByIdAsync(int id);
}

public class DbService(AppDbContext data) : IDbService
{
    public async Task<PrescriptionGetDto> CreatePrescriptionAsync(PrescriptionCreateDto prescriptionData)
    {
        if (prescriptionData.Medicaments.Count > 10)
        {
            throw new NotFoundException("Prescription can't have more than 10 medicaments");
        }

        if (prescriptionData.Date >= prescriptionData.DueDate)
        {
            throw new NotFoundException("Expired date");
        }

        var doctor = await data.Doctors.FirstOrDefaultAsync(d => d.IdDoctor == prescriptionData.IdDoctor);
        if (doctor == null)
        {
            throw new NotFoundException($"Doctor of id {prescriptionData.IdDoctor} not found");
        }
        
        var patient = await data.Patients.FirstOrDefaultAsync(p=>p.IdPatient == prescriptionData.Patient.IdPatient);
        if (patient == null)
        {
            patient = new Patient
            {
                FirstName = prescriptionData.Patient.FirstName,
                LastName = prescriptionData.Patient.LastName,
                BirthDate = prescriptionData.Patient.Birthdate
            };
            await data.Patients.AddAsync(patient);
            await data.SaveChangesAsync();
        }

        var medicamentIds = prescriptionData.Medicaments.Select(m => m.IdMedicament).ToList();
        var medicaments = await data.Medicaments
            .Where(m => medicamentIds.Contains(m.IdMedicament))
            .ToListAsync();

        if (medicaments.Count != prescriptionData.Medicaments.Count)
            throw new NotFoundException("One or more medicaments not found");
        
        var prescriptionMedicaments = prescriptionData.Medicaments.Select(m => new PrescriptionMedicament
        {
            IdMedicament = m.IdMedicament,
            Dose = m.Dose,
            Details = m.Description
        }).ToList();
        
        var prescription = new Prescription
        {
            Date = prescriptionData.Date,
            DueDate = prescriptionData.DueDate,
            IdPatient = patient.IdPatient,
            IdDoctor = doctor.IdDoctor,
            PRE = prescriptionMedicaments
        };
        await data.Prescriptions.AddAsync(prescription);
        await data.SaveChangesAsync();
            
        return new PrescriptionGetDto
        {
            IdPrescription = prescription.IdPrescription,
            Date = prescription.Date,
            DueDate = prescription.DueDate,
            Doctor = new DoctorGetDto
            {
                IdDoctor = doctor.IdDoctor,
                FirstName = doctor.FirstName
            },
            Medicaments = prescriptionData.Medicaments.Select(d=>
            {
                var name = medicaments.FirstOrDefault(m => m.IdMedicament == d.IdMedicament)?.Name;
                if (name != null)
                    return new MedicamentDetailsGetDto()
                    {
                        IdMedicament = d.IdMedicament,
                        Dose = d.Dose,
                        Description = d.Description,
                        Name = name
                    };
                throw new InvalidOperationException();
            }).ToList()
        };
    }

    public async Task<PatientGetDto> GetPatientByIdAsync(int id)
    {
        var result = await data.Patients.Select(st => new PatientGetDto
        {
            IdPatient = st.IdPatient,
            FirstName = st.FirstName,
            LastName = st.LastName,
            Birthdate = st.BirthDate,
            Prescriptions = st.Prescriptions.Select(p => new PrescriptionGetDto
                {
                    IdPrescription = p.IdPrescription,
                    Date = p.Date,
                    DueDate = p.DueDate,
                    Doctor = new DoctorGetDto
                    {
                        IdDoctor = p.IdDoctor,
                        FirstName = p.Doctor.FirstName
                    },
                    Medicaments = p.PRE.Select(pm => new MedicamentDetailsGetDto
                    {
                        IdMedicament = pm.IdMedicament,
                        Dose = pm.Dose,
                        Description = pm.Details,
                        Name = pm.Medicament.Name
                    }).ToList()
                }
            ).ToList()
        }).FirstOrDefaultAsync(p => p.IdPatient == id);
        if (result == null)
        {
            throw new NotFoundException($"Patient of id {id} not found");
        }
        return result;
    }
}