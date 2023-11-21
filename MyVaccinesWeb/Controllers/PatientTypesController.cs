using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyVaccinesWeb.Services.PatientTypesService;

namespace MyVaccinesWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientTypesController : ControllerBase
    {
        private readonly IPatientTypesService _patientTypesService;

        public PatientTypesController(IPatientTypesService patientTypesService)
        {
            _patientTypesService = patientTypesService;
        }

        [HttpGet]
        public async Task<List<PatientsType>?> GetAllPatientTypesAsync()
        {
            return await _patientTypesService.GetAllPatientTypesAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PatientsType>?> GetSinglePatientTypeAsync(int id)
        {
            var result = await _patientTypesService.GetSinglePatientTypeAsync(id);
            if (result is null)
                return BadRequest();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> AddPatientTypeAsync(PatientsType patientType)
        {
            bool result = await _patientTypesService.AddPatientTypeAsync(patientType);
            if (result is false)
                return BadRequest();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeletePatientTypeAsync(int id)
        {
            bool result = await _patientTypesService.DeletePatientTypeAsync(id);
            if (result is false)
                return BadRequest();
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> UpdatePatientTypeAsync(int id, PatientsType patientsType)
        {
            bool result = await _patientTypesService.UpdatePatientTypeAsync(id, patientsType);
            if (!result)
                return BadRequest();
            return Ok(result);
        }
    }
}
