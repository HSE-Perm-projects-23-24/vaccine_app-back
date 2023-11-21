using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyVaccinesWeb.Services.CountriesService;
using MyVaccinesWeb.Services.VaccineService;

namespace MyVaccinesWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ICountriesService _countriesService;

        public CountriesController(ICountriesService countriesService)
        {
            _countriesService = countriesService;
        }

        [HttpGet]
        public async Task<List<Country>?> GetAllCountriesAsync()
        {
            return await _countriesService.GetAllCountriesAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Country>?> GetSingleCountryAsync(int id)
        {
            var result = await _countriesService.GetSingleCountryAsync(id);
            if (result is null)
                return BadRequest();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> AddCountryAsync(Country country)
        {
            bool result = await _countriesService.AddCountryAsync(country);
            if (result is false)
                return BadRequest();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteCountryAsync(int id)
        {
            bool result = await _countriesService.DeleteCountryAsync(id);
            if (result is false)
                return BadRequest();
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> UpdateCountryAsync(int id, Country country)
        {
            bool result = await _countriesService.UpdateCountryAsync(id, country);
            if (result is false)
                return BadRequest();
            return Ok(result);
        }
    }
}
