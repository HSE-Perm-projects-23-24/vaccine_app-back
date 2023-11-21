using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyVaccinesWeb.My_Classes;
using MyVaccinesWeb.Services.VaccineMakersService;
using MyVaccinesWeb.Services.VaccineService;

namespace MyVaccinesWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaccineMakersController : ControllerBase
    {
        private readonly IVaccineMakersService _vaccineMakersService;

        public VaccineMakersController(IVaccineMakersService vaccineMakersService)
        {
            _vaccineMakersService = vaccineMakersService;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteVaccineMakerAsync(int id)
        {
            bool result = await _vaccineMakersService.DeleteVaccineMakerAsync(id);
            if (result is false)
                return BadRequest();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> AddVaccineMakerAsync(MyVaccineMakers vaccineMaker)
        {
            bool result = await _vaccineMakersService.AddVaccineMakerAsync(vaccineMaker);
            if (result)
                return Ok(result);
            return BadRequest();
        }

        [HttpGet]
        public async Task<List<VaccinesMaker>?> GetAllVaccineMakersAsync()
        {
            return await _vaccineMakersService.GetAllVaccineMakersAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VaccinesMaker>?> GetSingleVaccineMakerAsync(int id)
        {
            var result = await _vaccineMakersService.GetSingleVaccineMakerAsync(id);
            if (result is null)
                return BadRequest();
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> UpdateVaccineMakerAsync(int id, MyVaccineMakers vaccineMaker)
        {
            bool result = await _vaccineMakersService.UpdateVaccineMakerAsync(id, vaccineMaker);
            if (result)
                return Ok(result);
            return BadRequest();
        }
    }
}
