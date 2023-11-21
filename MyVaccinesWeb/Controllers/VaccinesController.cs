using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using MyVaccinesWeb.My_Classes;
using MyVaccinesWeb.Services.VaccineService;

namespace MyVaccinesWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaccinesController : ControllerBase
    {
        private readonly IVaccineService _vaccineService;

        public VaccinesController(IVaccineService vaccineService)
        {
            _vaccineService = vaccineService;
        }

        [HttpGet]
        public async Task<List<Vaccine>?> GetAllVaccinesAsync()
        {
            return await _vaccineService.GetAllVaccinesAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Vaccine>?> GetSingleVaccineAsync(int id)
        {
            var result = await _vaccineService.GetSingleVaccineAsync(id);
            if (result is null)
                return BadRequest();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteVaccineAsync(int id)
        {
            bool result = await _vaccineService.DeleteVaccineAsync(id);
            if (result is false)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> AddVaccineAsync(MyVaccineClass vaccine)
        {
            bool result = await _vaccineService.AddVaccineAsync(vaccine);
            if (result is false)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> UpdateVaccineAsync(int id, MyVaccineClass vaccine)
        {
            bool result = await _vaccineService.UpdateVaccineAsync(id, vaccine);
            if (!result)
                return BadRequest();
            return Ok(result);
        }
    }
}
