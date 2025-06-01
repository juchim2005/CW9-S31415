using Microsoft.AspNetCore.Mvc;
using Recepty.DTOs;
using Recepty.Exceptions;
using Recepty.Service;

namespace Recepty.Controllers;

[ApiController]
[Route("[controller]")]
public class PrescriptionsController(IDbService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AddPrescription([FromBody] PrescriptionCreateDto prescriptionData)
    {
        try
        {
            var prescription = await service.CreatePrescriptionAsync(prescriptionData);
            return Created($"prescriptions/{prescription.IdPrescription}",prescription);
        } catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}