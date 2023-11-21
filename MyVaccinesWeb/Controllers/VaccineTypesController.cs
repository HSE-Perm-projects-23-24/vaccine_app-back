using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyVaccinesWeb.Services.AdminsService;
using MyVaccinesWeb.Services.VaccineTypesService;

namespace MyVaccinesWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaccineTypesController : ControllerBase
    {
        private readonly IVaccineTypesService _vaccineTypesService;

        public VaccineTypesController(IVaccineTypesService vaccineTypesService)
        {
            _vaccineTypesService = vaccineTypesService;
        }

        [HttpGet]
        public async Task<List<VaccinesType>?> GetAllVaccineTypesAsync()
        {
            return await _vaccineTypesService.GetAllVaccineTypesAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<KeyWord>?> GetSingleVaccineTypeAsync(int id)
        {
            var result = await _vaccineTypesService.GetSingleVaccineTypeAsync(id);
            if (result is null)
                return BadRequest();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> AddVaccineTypeAsync(VaccinesType vaccineType)
        {
            bool result = await _vaccineTypesService.AddVaccineTypeAsync(vaccineType);
            if (result is false)
                return BadRequest();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteVaccineTypeAsync(int id)
        {
            bool result = await _vaccineTypesService.DeleteVaccineTypeAsync(id);
            if (result is false)
                return BadRequest();
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> UpdateVaccineTypeAsync(int id, VaccinesType vaccinesType)
        {
            bool result = await _vaccineTypesService.UpdateVaccineTypeAsync(id, vaccinesType);
            if (!result)
                return BadRequest();
            return Ok(result);
        }
    }
}
