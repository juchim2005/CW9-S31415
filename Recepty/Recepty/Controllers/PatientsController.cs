using Microsoft.AspNetCore.Mvc;
using Recepty.Exceptions;
using Recepty.Service;

namespace Recepty.Controllers;

[ApiController]
[Route("[controller]")]
public class PatientsController(IDbService service) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPatient([FromRoute] int id)
    {
        try
        {
            var patient = await service.GetPatientByIdAsync(id);
            return Ok(patient);
        } catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}